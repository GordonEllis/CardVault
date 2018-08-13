using Cards.Models;
using QueueHandler.Queues;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace Cards
{
    public class CardsClient : QueueWriterService
    {
        #region -  Constructors  -

        public CardsClient(ConnectionFactory factory) : base(factory, Queueing.Exchange) { }

        #endregion

        #region -  Methods  -

        public Task<MessageResponse<Card[]>> GetCards(GetCardsRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAndReply<GetCardsRequest, Card[]>(Queueing.Queues.GetCards, request, millisecondsTimeout, cancellationToken);
        }

        public Task<MessageResponse<Card>> CreateCard(Card card, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAndReply<Card, Card>(Queueing.Queues.CreateCard, card, millisecondsTimeout, cancellationToken);
        }

        public Task<MessageResponse<Card>> UpdateCard(Card card, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAndReply<Card, Card>(Queueing.Queues.UpdateCard, card, millisecondsTimeout, cancellationToken);
        }

        public void DeleteCard(DeleteCardRequest request, int millisecondsTimeout = -1)
        {
            Write(Queueing.Queues.DeleteCard, request);
        }
        #endregion
    }
}
