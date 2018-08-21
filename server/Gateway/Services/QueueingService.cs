using QueueHandler;
using RabbitMQ.Client;

namespace Gateway.Services
{
    public class QueueingService
    {
        
        public QueueingService(QueueingConfiguration configuration)
        {
            ConnectionFactory = new ConnectionFactory() { HostName = configuration.HostName };
        }

        public ConnectionFactory ConnectionFactory { get; }
    }
}