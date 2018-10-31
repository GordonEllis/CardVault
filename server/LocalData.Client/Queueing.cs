using QueueHandler.Exchanges;
using QueueHandler.Queues;

namespace LocalData.Client
{
    public class Queueing
    {
        public static readonly ExchangeConfiguration Exchange = new ExchangeConfiguration("local_data", true);
        public static class Queues
        {
            public static readonly QueueConfiguration GetLocalCardData = new QueueConfiguration("local_card_data_get", false, Exchange);
            public static readonly QueueConfiguration GetLocalScryfallData = new QueueConfiguration("local_scryfall_data_get", false, Exchange);
			public static readonly QueueConfiguration GetCollectionData = new QueueConfiguration("collection_data_get", false, Exchange);
		}
    }
}
