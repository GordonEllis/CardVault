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
        }

        #endregion

        #region -  Event Handlers  -

        private async Task<Card[]> GetCards(object sender, ReceiveEventArgs<GetCardsRequest> e)
        {
            IEnumerable<Card> cards = _context.Cards;
            Card[] resulting = await Task.FromResult(cards.ToArray());
            return resulting;
        }

        private async Task<Card[]> GetSpecificCards(object sender, ReceiveEventArgs<GetCardsRequest> e)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var stringTask = client.GetStringAsync("https://api.scryfall.com/cards/" + e.Message.CardIds[0]);
            var cardDetails = new List<Card>();

            var msg = await stringTask;
            var mapper = new ModelMapper();
            JObject parsed = JObject.Parse(msg);
            cardDetails.Add(ModelMapper.MapCardData(parsed));
            return cardDetails.ToArray();
        }
    }
    #endregion
}