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

        public Task<MessageResponse<(CollectionData[], LocalCardData[])>> LoadDataFromSpreadsheet(LoadCardDataRequest request, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
		{
			return WriteAndReply<LoadCardDataRequest, (CollectionData[], LocalCardData[])>(Queueing.Queues.GetCollectionData, request, null, millisecondsTimeout, cancellationToken);
		}
	}
}
