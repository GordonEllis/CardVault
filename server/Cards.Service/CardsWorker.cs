using Cards.Models;
using Newtonsoft.Json.Linq;
using QueueHandler.Queues;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cards
{
    class CardsWorker : QueueReaderService
    {
        private static readonly HttpClient client = new HttpClient();

        #region -  Constructor  -

        public CardsWorker(ConnectionFactory factory) : base(factory, Queueing.Exchange)
        {
            ReadAndReply<GetCardsRequest, Card[]>(Queueing.Queues.GetCards, true, GetCardsAsync);
        }

        #endregion

        #region -  Event Handlers  -

        private static async Task<Card[]> GetCardsAsync(object sender, ReceiveEventArgs<GetCardsRequest> e)
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