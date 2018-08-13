namespace QueueHandler.Queues
{
    public class QueueConfiguration
    {
        public QueueConfiguration() { }
        public QueueConfiguration(string name, bool durable)
        {
            Name = name;
            Durable = durable;
        }

        public string Name { get; set; }
        public bool Durable { get; set; } = true;
        public bool Exclusive { get; set; } = false;
        public bool AutoDelete { get; set; } = false;

        public override string ToString()
        {
            return Name;
        }
    }
}
