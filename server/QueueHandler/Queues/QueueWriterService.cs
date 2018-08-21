using QueueHandler.Collections;
using QueueHandler.Exchanges;
using QueueHandler.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QueueHandler.Queues
{
    public abstract class QueueWriterService : QueueService
    {
        #region -  Constructor  -

        protected QueueWriterService(ConnectionFactory connectionFactory, ExchangeConfiguration exchange) : this(connectionFactory, exchange, null) { }

        protected QueueWriterService(
            ConnectionFactory connectionFactory,
            ExchangeConfiguration exchange,
            IMessageHandler messageHandler
        ) : base(connectionFactory, messageHandler)
        {
            Exchange = exchange;
        }

        #endregion

        #region -  Properties  -

        protected ExchangeConfiguration Exchange { get; }

        #endregion

        #region -  Methods  -

        protected void Write<TMessage>(QueueConfiguration queue, TMessage value, string routingKey = null)
        {
            routingKey = routingKey ?? queue.Name;
            EnsureQueueExists(queue);
            var sendProps = Channel.CreateBasicProperties();
            sendProps.Persistent = true;
            var messageBody = MessageHandler.Encode(value);
            Channel.BasicPublish(Exchange.Name, routingKey, sendProps, messageBody);
        }

        protected async Task<MessageResponse<TResponse>> WriteAndReply<TMessage, TResponse>(QueueConfiguration queue, TMessage value, string routingKey = null, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            routingKey = routingKey ?? queue.Name;
            EnsureQueueExists(queue);

            // Set up the queue that the reply will be written to.
            var replyQueue = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(replyQueue, Exchange.Name, replyQueue);

            var sendProps = Channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            sendProps.CorrelationId = correlationId;
            sendProps.ReplyTo = replyQueue;

            // Post our message.
            Channel.BasicPublish(Exchange.Name, routingKey, sendProps, MessageHandler.Encode(value));
            
            // Wait for a response.
            var replyCollection = new AsyncQueue<MessageResponse<TResponse>>();
            void Consume(object sender, BasicDeliverEventArgs ea)
            {
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    var reply = new MessageResponse<TResponse>();
                    if (ea.BasicProperties.Type == nameof(Exception))
                    {
                        reply.Success = false;
                        reply.Error = MessageHandler.Decode<ErrorMessage>(ea.Body);
                    }
                    else
                    {
                        reply.Success = true;
                        reply.Response = MessageHandler.Decode<TResponse>(ea.Body);
                    }
                    replyCollection.Enqueue(reply);
                    }
            }
            ConsumeQueue(replyQueue, Consume);

            // Wait for something to be added to the collection, then return it.
            return await replyCollection.DequeueAsync(millisecondsTimeout, cancellationToken);
        }

        #endregion
    }
}