using QueueHandler.Exchanges;
using QueueHandler.Queues;

namespace Decks.Client
{
    public class Queueing
    {
        public static readonly ExchangeConfiguration Exchange = new ExchangeConfiguration("decks_data", true);
        public static class Queues
        {
			public static readonly QueueConfiguration SaveDeck = new QueueConfiguration("decks_data_save", false, Exchange);
			//public static readonly QueueConfiguration DeleteDecks = new QueueConfiguration("decks_data_delete", false, Exchange);
			public static readonly QueueConfiguration GetDecks = new QueueConfiguration("decks_data_get", false, Exchange);
        }
    }
}
