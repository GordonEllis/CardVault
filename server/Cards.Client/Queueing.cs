using QueueHandler.Exchanges;
using QueueHandler.Queues;

namespace Cards
{
    public class Queueing
    {
        public static readonly ExchangeConfiguration Exchange = new ExchangeConfiguration("cards", true);
        public static class Queues
        {
            public static readonly QueueConfiguration CreateCard = new QueueConfiguration("cards_create", true);
            public static readonly QueueConfiguration UpdateCard = new QueueConfiguration("cards_update", true);
            public static readonly QueueConfiguration DeleteCard = new QueueConfiguration("cards_delete", true);
            public static readonly QueueConfiguration PurchaseCard = new QueueConfiguration("cards_purchase", true);

            public static readonly QueueConfiguration GetCards = new QueueConfiguration("cards_get", false);
        }
    }
}
