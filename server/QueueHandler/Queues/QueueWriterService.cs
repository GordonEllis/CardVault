using QueueHandler.Collections;
using QueueHandler.Exchanges;
using QueueHandler.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace QueueHandler.Queues
{
    public abstract class QueueWriterService : QueueService
    {
        #region -  Constructor  -

        protected QueueWriterService(ConnectionFactory connectionFactory, ExchangeConfiguration exchange) : base(connectionFactory, exchange) { }
        protected QueueWriterService(ConnectionFactory connectionFactory, ExchangeConfiguration exchange, IMessageHandler messageHandler) : base(connectionFactory, exchange, messageHandler) { }

        #endregion

        #region -  Methods  -

        protected void Write<TMessage>(QueueConfiguration queue, TMessage value)
        {
            EnsureQueueExists(queue);
            var sendProps = Channel.CreateBasicProperties();
            sendProps.Persistent = true;
            var messageBody = MessageHandler.Encode(value);
            Channel.BasicPublish(Exchange.Name, queue.Name, sendProps, messageBody);
            Console.WriteLine($"Message written to {Exchange.Name}.{queue.Name}");
        }

        protected async Task<MessageResponse<TResponse>> WriteAndReply<TMessage, TResponse>(QueueConfiguration queue, TMessage value, int millisecondsTimeout = -1, CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureQueueExists(queue);

            // Set up the queue that the reply will be written to.
            var replyQueue = Channel.QueueDeclare().QueueName;
            Channel.QueueBind(replyQueue, Exchange.Name, replyQueue);

            var sendProps = Channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            sendProps.CorrelationId = correlationId;
            sendProps.ReplyTo = replyQueue;

            // Post our message.
            Channel.BasicPublish(Exchange.Name, queue.Name, sendProps, MessageHandler.Encode(value));
            Console.WriteLine($"Message written to {Exchange.Name}.{queue.Name}. Waiting on response...");

            // Wait for a response.
            var replyCollection = new AsyncQueue<MessageResponse<TResponse>>();
            void consume(object sender, BasicDeliverEventArgs ea)
            {
                if (ea.BasicProperties.CorrelationId == correlationId)
                {
                    Console.WriteLine($"Reply received for {Exchange.Name}.{queue.Name}.");
                    var reply = new MessageResponse<TResponse>();
                    if (ea.BasicProperties.Type == nameof(Exception))
                    {
                        Console.WriteLine($"Reply for {Exchange.Name}.{queue.Name} contains an error.");
                        reply.Success = false;
                        reply.Error = MessageHandler.Decode<ErrorMessage>(ea.Body);
                    }
                    else
                    {
                        Console.WriteLine($"Reply for {Exchange.Name}.{queue.Name} was successful.");
                        reply.Success = true;
                        reply.Response = MessageHandler.Decode<TResponse>(ea.Body);
                    }
                    replyCollection.Enqueue(reply);
                    Console.WriteLine($"Reply for {Exchange.Name}.{queue.Name} added to async queue.");
                }
            };
            ConsumeQueue(replyQueue, consume);

            // Wait for something to be added to the collection, then return it.
            return await replyCollection.DequeueAsync(millisecondsTimeout, cancellationToken);
        }

        #endregion
    }
}