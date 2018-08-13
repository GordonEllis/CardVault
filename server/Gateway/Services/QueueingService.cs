using QueueHandler;
using RabbitMQ.Client;

namespace Gateway.Services
{
    public class QueueingService
    {
        #region -  Fields  -

        private readonly ConnectionFactory _connectionFactory;

        #endregion

        #region -  Constructor  -

        public QueueingService(QueueingConfiguration configuration)
        {
            _connectionFactory = new ConnectionFactory() { HostName = configuration.HostName };
        }

        #endregion

        public ConnectionFactory ConnectionFactory
        {
            get { return _connectionFactory; }
        }
    }
}