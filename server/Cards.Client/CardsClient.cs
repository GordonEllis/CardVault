using Cards.Client.Models;
using QueueHandler.Queues;
using QueueHandler.Messages;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace Cards.Client
{
    public class CardsClient : QueueWriterService
    {
        public CardsClient(ConnectionFactory factory) : base(factory, Queueing.Exchange) { }

        public Task<MessageResponse<Card[]>> GetCards(GetCardsRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAndReply<GetCardsRequest, Card[]>(Queueing.Queues.GetCards, request, null, millisecondsTimeout, cancellationToken);
        }
    }
}
