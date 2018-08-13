using QueueHandler.Exchanges;
using QueueHandler.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace QueueHandler.Queues
{
    public abstract class QueueService : IDisposable
    {
        #region -  Constructor  -

        public QueueService(ConnectionFactory connectionFactory, ExchangeConfiguration exchange) : this(connectionFactory, exchange, null) { }
        public QueueService(ConnectionFactory connectionFactory, ExchangeConfiguration exchange, IMessageHandler messageHandler)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory), "The supplied connection factory is invalid.");
            Exchange = exchange ?? throw new ArgumentNullException(nameof(exchange), "The supplied exchange configuration is invalid.");

            Connection = connectionFactory.CreateConnection();
            MessageHandler = messageHandler ?? new DefaultMessageHandler();
            Channel = Connection.CreateModel();
            Channel.BasicQos(0, 1, false);

            EnsureExchangeExists(exchange);
        }

        #endregion

        #region -  Properties  -

        protected ConnectionFactory ConnectionFactory { get; private set; }
        protected IConnection Connection { get; private set; }
        protected IModel Channel { get; private set; }
        protected ExchangeConfiguration Exchange { get; private set; }
        protected IMessageHandler MessageHandler { get; private set; }

        #endregion

        #region -  Utility Methods  -

        protected void EnsureQueueExists(QueueConfiguration config)
        {
            Channel.QueueDeclare(config.Name, config.Durable, config.Exclusive, config.AutoDelete);
            Channel.QueueBind(config.Name, Exchange.Name, config.Name);
            Console.WriteLine($"Ensured that queue {Exchange.Name}.{config.Name} exists and is bound.");
        }

        protected void EnsureExchangeExists(ExchangeConfiguration config)
        {
            Channel.ExchangeDeclare(config.Name, config.Type, config.Durable, config.AutoDelete);
            Console.WriteLine($"Ensured that exchange {config.Name} exists.");
        }

        protected EventingBasicConsumer ConsumeQueue(string queueName, EventHandler<BasicDeliverEventArgs> handler)
        {
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += handler;
            Channel.BasicConsume(queueName, false, consumer);
            Console.WriteLine($"Listening on {Exchange.Name}.{queueName}...");
            return consumer;
        }

        #endregion

        #region -  IDisposable Support  -

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Channel.Close();
                    Channel.Dispose();
                    Connection.Close();
                    Connection.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}