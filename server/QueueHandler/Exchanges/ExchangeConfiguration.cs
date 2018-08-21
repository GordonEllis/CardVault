using RabbitMQ.Client;

namespace QueueHandler.Exchanges
{
    public class ExchangeConfiguration
    {
        public ExchangeConfiguration() { }
        public ExchangeConfiguration(string name, bool durable) : this(name, durable, ExchangeType.Direct) { }
        public ExchangeConfiguration(string name, bool durable, string type)
        {
            Name = name;
            Durable = durable;
            Type = type;
        }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = ExchangeType.Direct;
        public bool Durable { get; set; } = true;
        public bool AutoDelete { get; set; } = false;

        public override string ToString()
        {
            return Name;
        }
    }
}
