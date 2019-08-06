using Cards.Client;
using Cards.Client.Models;
using Cards.Service.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using QueueHandler.Queues;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cards.Service
{
    class CardsWorker : QueueReaderService
    {
		#region - Fields - 

		private readonly CardContext _context = new CardContext();

        #endregion

        #region -  Constructor  -
        private static readonly HttpClient client = new HttpClient();

        public CardsWorker(ConnectionFactory factory) : base(factory)
        {
            ReadAndReply<GetCardsRequest, Card[]>(Queueing.Queues.GetCards, true, GetCards);
            ReadAndReply<SaveCardsRequest, Boolean>(Queueing.Queues.SaveCards, true, SaveCards);
        }

        #endregion

        #region -  Event Handlers  -
        private async Task<Boolean> SaveCards(object sender, ReceiveEventArgs<SaveCardsRequest> e)
        {
			var storedCards = await Task.FromResult(_context.Cards.ToList());
			var storedUris = await Task.FromResult(_context.ImageUris.ToList());
			var incomingCards = MergeDuplicates(e.Message.CardData.ToList());
			var incomingUris = e.Message.CardData.Select(i => i.ImageUris).Where(u => u.Small != "").ToList();

			SaveCardDetails(_context, storedCards, incomingCards);
			SaveImageUris(_context, storedUris, incomingUris);
			_context.SaveChanges();
			return true;
		}

		private List<Card> MergeDuplicates(List<Card> incomingCards)
		{
			incomingCards.ForEach(c =>
			{
				var a = incomingCards.FindAll(i => i.Id == c.Id);
				if (a.Count > 1)
				{
					var b = "here!";
				}
				var d = "stop";
			});

			return incomingCards;
		}

		private static void SaveCardDetails(CardContext context, List<Card> storedCards, List<Card> incomingCards)
        {
			var itemsToAdd = incomingCards.ToArray().Where(d => !storedCards.Any(c => c.Id == d.Id));
			var itemsToUpdate = storedCards.Where(d => incomingCards.ToArray().Any(c => c.Id == d.Id));
			var itemsToRemove = storedCards.Where(d => !incomingCards.ToArray().Any(c => c.Id == d.Id));

			foreach (var update in itemsToUpdate)
				context.Entry(update).CurrentValues.SetValues(incomingCards.ToArray().Where(d => d.Id == update.Id).SingleOrDefault());
			context.Cards.RemoveRange(itemsToRemove);
			context.Cards.AddRange(itemsToAdd);
		}

		private static void SaveImageUris(CardContext context, List<ImageUri> storedUris, List<ImageUri> incomingUris)
		{
			
			var itemsToAdd = incomingUris.ToArray().Where(d => !storedUris.Any(c => c.Id == d.Id));
			var itemsToUpdate = storedUris.Where(d => incomingUris.ToArray().Any(c => c.Id == d.Id));
			var itemsToRemove = storedUris.Where(d => !incomingUris.ToArray().Any(c => c.Id == d.Id));

			foreach (var update in itemsToUpdate)
				context.Entry(update).CurrentValues.SetValues(incomingUris.ToArray().Where(d => d.Id == update.Id).SingleOrDefault());
			context.ImageUris.RemoveRange(itemsToRemove);
			context.ImageUris.AddRange(itemsToAdd);
		}

		private async Task<Card[]> GetCards(object sender, ReceiveEventArgs<GetCardsRequest> e)
        {
			return await Task.FromResult(_context.Cards.Include(c => c.ImageUris).ToArray());
        }

        private async Task<Card[]> GetSpecificCards(object sender, ReceiveEventArgs<GetCardsRequest> e)
        {
            List<Card> cardDetails = new List<Card>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            JObject parsed = JObject.Parse(await client.GetStringAsync("https://api.scryfall.com/cards/" + e.Message.CardIds[0]));
            cardDetails.Add(ModelMapper.MapCardData(parsed));
            return cardDetails.ToArray();
        }
    }
    #endregion
}