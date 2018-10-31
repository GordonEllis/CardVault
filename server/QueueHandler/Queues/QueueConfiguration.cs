using QueueHandler.Exchanges;

namespace QueueHandler.Queues
{
    public class QueueConfiguration
    {
        public QueueConfiguration() { }

        public QueueConfiguration(string name, bool durable, params ExchangeConfiguration[] exchanges) : this(name, durable, name, exchanges) { }

        public QueueConfiguration(string name, bool durable, string bindingKey, params ExchangeConfiguration[] exchanges)
        {
            Name = name;
            Durable = durable;
            BindingKey = bindingKey;
            Exchanges = exchanges;
        }

        public string Name { get; set; }
        public string BindingKey { get; set; }
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;
        public ExchangeConfiguration[] Exchanges { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
