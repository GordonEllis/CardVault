using Cards.Client.Models;
using Cards.Client;
using System.Threading.Tasks;
using QueueHandler.Queues;
using RabbitMQ.Client;
using System.Collections.Generic;
using System;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Cards.Service.Context;
using System.Linq;

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
            var databaseCards = await Task.FromResult(_context.Cards.ToArray());
            var itemsToAdd = e.Message.CardData.Where(d => !databaseCards.Any(c => c.Id == d.Id));
            var itemsToUpdate = databaseCards.Where(d => e.Message.CardData.Any(c => c.Id == d.Id));
            var itemsToRemove = databaseCards.Where(d => !e.Message.CardData.Any(c => c.Id == d.Id));

            foreach (var update in itemsToUpdate)
                _context.Entry(update).CurrentValues.SetValues(e.Message.CardData.Where(d => d.Id == update.Id).SingleOrDefault());
            _context.Cards.RemoveRange(itemsToRemove);
            _context.Cards.AddRange(itemsToAdd);
            _context.SaveChanges();
            return true;
        }

        private async Task<Card[]> GetCards(object sender, ReceiveEventArgs<GetCardsRequest> e)
        {
            IEnumerable<Card> cards = _context.Cards;
            Card[] resulting = await Task.FromResult(cards.ToArray());
            return resulting;
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