using QueueHandler.Exchanges;
using QueueHandler.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Threading.Tasks;

namespace QueueHandler.Queues
{
    public abstract class QueueReaderService : QueueService
    {
        #region -  Constructor  -

        protected QueueReaderService(ConnectionFactory connectionFactory, ExchangeConfiguration exchange) : base(connectionFactory, exchange) { }
        protected QueueReaderService(ConnectionFactory connectionFactory, ExchangeConfiguration exchange, IMessageHandler messageHandler) : base(connectionFactory, exchange, messageHandler) { }

        #endregion

        #region -  Methods  -

        protected Task<EventingBasicConsumer> Read<TMessage>(QueueConfiguration queue, bool autoAck, Func<object, ReceiveEventArgs<TMessage>, Task> handler)
        {
            EnsureQueueExists(queue);
            async void messageHandlerAsync(object sender, BasicDeliverEventArgs args)
            {
                TMessage message = MessageHandler.Decode<TMessage>(args.Body);
                var receivedArgs = new ReceiveEventArgs<TMessage>(message, autoAck);
                try
                {
                    await handler(sender, receivedArgs);
                }
                finally
                {
                    if (receivedArgs.Acknowledge)
                    {
                        Console.WriteLine($"Acknowledged message for {Exchange.Name}.{queue.Name}.");
                        Channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                    }
                }
            }
            return Task.FromResult(ConsumeQueue(queue.Name, messageHandlerAsync));
        }

        protected Task<EventingBasicConsumer> ReadAndReply<TMessage, TResponse>(QueueConfiguration queue, bool autoAck, Func<object, ReceiveEventArgs<TMessage>, Task<TResponse>> handler)
        {
            EnsureQueueExists(queue);
            async void messageHandlerAsync(object sender, BasicDeliverEventArgs args)
            {
                Console.WriteLine($"Message received on {Exchange.Name}.{queue.Name}.");
                var props = args.BasicProperties;
                var replyProps = Channel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                TMessage message = MessageHandler.Decode<TMessage>(args.Body);
                var receivedArgs = new ReceiveEventArgs<TMessage>(message, autoAck);

                try
                {
                    var result = await handler(sender, receivedArgs);

                    // If there's a queue to reply to.
                    if (!String.IsNullOrEmpty(props.ReplyTo))
                    {
                        var response = MessageHandler.Encode(result);
                        Channel.BasicPublish(Exchange.Name, props.ReplyTo, basicProperties: replyProps, body: response);
                        Console.WriteLine($"Reply sent to {Exchange.Name}.{queue.Name}.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error encountered handling message on {Exchange.Name}.{queue.Name}.");
                    // Throw or return the error.
                    if (!String.IsNullOrEmpty(props.ReplyTo))
                    {
                        var response = MessageHandler.Encode(new ErrorMessage(ex));
                        replyProps.Type = nameof(Exception);
                        Channel.BasicPublish(Exchange.Name, props.ReplyTo, basicProperties: replyProps, body: response);
                        Console.WriteLine($"Reply address found. Responding with message.");
                    }
                    throw;
                }
                finally
                {
                    if (receivedArgs.Acknowledge)
                    {
                        Console.WriteLine($"Acknowledged message for {Exchange.Name}.{queue.Name}.");
                        Channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                    }
                    else
                    {
                        Console.WriteLine($"Refusing to acknowledge message for {Exchange}.{queue.Name}.");
                    }
                }
            }
            return Task.FromResult(ConsumeQueue(queue.Name, messageHandlerAsync));
        }

        #endregion
    }
}