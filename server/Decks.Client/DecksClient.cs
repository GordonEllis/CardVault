using QueueHandler.Queues;
using QueueHandler.Messages;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;
using Decks.Client.Models;
using System;

namespace Decks.Client
{
    public class DecksClient : QueueWriterService
    {
        public DecksClient(ConnectionFactory factory) : base(factory, Queueing.Exchange) { }

		public Task<MessageResponse<Boolean>> DeleteDeck(DeleteDeckRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
		{
			return WriteAndReply<DeleteDeckRequest, Boolean>(Queueing.Queues.DeleteDecks, request, null, millisecondsTimeout, cancellationToken);
		}

		public Task<MessageResponse<Deck[]>> GetDecks(GetDecksRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAndReply<GetDecksRequest, Deck[]>(Queueing.Queues.GetDecks, request, null, millisecondsTimeout, cancellationToken);
        }

        public Task<MessageResponse<Deck>> SaveDeck(SaveDeckRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAndReply<SaveDeckRequest, Deck>(Queueing.Queues.SaveDeck, request, null, millisecondsTimeout, cancellationToken);
        }
	}
}
