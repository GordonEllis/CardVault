using QueueHandler.Exchanges;
using QueueHandler.Queues;

namespace Cards.Client
{
    public class Queueing
    {
        public static readonly ExchangeConfiguration Exchange = new ExchangeConfiguration("cards_data", true);
        public static class Queues
        {
            public static readonly QueueConfiguration GetCards = new QueueConfiguration("cards_data_get", false, Exchange);
            public static readonly QueueConfiguration SaveCards = new QueueConfiguration("cards_data_save", false, Exchange);
        }
    }
}
