using LocalData.Client.Models;
using QueueHandler.Messages;
using QueueHandler.Queues;
using RabbitMQ.Client;
using System.Threading;
using System.Threading.Tasks;

namespace LocalData.Client
{
    public class LocalDataClient : QueueWriterService
    {
        public LocalDataClient(ConnectionFactory factory) : base(factory, Queueing.Exchange) { }

        public Task<MessageResponse<LocalCardData[]>> GetLocalData(LoadCardDataRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAndReply<LoadCardDataRequest, LocalCardData[]>(Queueing.Queues.GetLocalCardData, request, null, millisecondsTimeout, cancellationToken);
        }

        public Task<MessageResponse<LocalScryfallData[]>> GetLocalScryfallData(LoadScryfallDataRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            return WriteAndReply<LoadScryfallDataRequest, LocalScryfallData[]>(Queueing.Queues.GetLocalScryfallData, request, null, millisecondsTimeout, cancellationToken);
        }

		public Task<MessageResponse<CollectionData[]>> GetCollectionData(LoadCardDataRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
		{
			return WriteAndReply<LoadCardDataRequest, CollectionData[]>(Queueing.Queues.GetCollectionData, request, null, millisecondsTimeout, cancellationToken);
		}
	}
}
