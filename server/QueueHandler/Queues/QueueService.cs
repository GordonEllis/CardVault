using QueueHandler.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace QueueHandler.Queues
{
    public abstract class QueueService : IDisposable
    {
        #region -  Constructor  -

        protected QueueService(ConnectionFactory connectionFactory) : this(connectionFactory, null) { }
        protected QueueService(ConnectionFactory connectionFactory, IMessageHandler messageHandler)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory), "The supplied connection factory is invalid.");
            Connection = connectionFactory.CreateConnection();
            MessageHandler = messageHandler ?? new DefaultMessageHandler();
            Channel = Connection.CreateModel();
            Channel.BasicQos(0, 1, false);
        }

        #endregion

        #region -  Properties  -

        protected ConnectionFactory ConnectionFactory { get; }
        protected IConnection Connection { get; }
        protected IModel Channel { get; }
        protected IMessageHandler MessageHandler { get; }

        #endregion

        #region -  Utility Methods  -

        protected void EnsureQueueExists(QueueConfiguration config)
        {
            Channel.QueueDeclare(config.Name, config.Durable, config.Exclusive, config.AutoDelete);
            foreach (var exchange in config.Exchanges)
            {
                Channel.ExchangeDeclare(exchange.Name, exchange.Type, exchange.Durable, exchange.AutoDelete);
                Channel.QueueBind(config.Name, exchange.Name, config.BindingKey ?? config.Name);
            }
        }

        protected EventingBasicConsumer ConsumeQueue(string queueName, EventHandler<BasicDeliverEventArgs> handler)
        {
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += handler;
            Channel.BasicConsume(queueName, false, consumer);
            return consumer;
        }

        #endregion

        #region -  IDisposable Support  -

        private bool _disposed;

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