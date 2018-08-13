using QueueHandler.Exchanges;
using QueueHandler.Queues;
using System.Collections.Generic;

namespace QueueHandler
{
    public class QueueingConfiguration
    {
        public string HostName { get; set; } = "rabbitmq://localhost";
        public Dictionary<string, ExchangeConfiguration> Exchanges { get; set; } = new Dictionary<string, ExchangeConfiguration>();
        public Dictionary<string, QueueConfiguration> Queues { get; set; } = new Dictionary<string, QueueConfiguration>();
    }
}
