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

        protected QueueReaderService(ConnectionFactory connectionFactory) : base(connectionFactory) { }
        protected QueueReaderService(ConnectionFactory connectionFactory, IMessageHandler messageHandler) : base(connectionFactory, messageHandler) { }

        #endregion

        #region -  Methods  -

        protected Task<EventingBasicConsumer> Read<TMessage>(QueueConfiguration queue, bool autoAck, Func<object, ReceiveEventArgs<TMessage>, Task> handler)
        {
            EnsureQueueExists(queue);
            async void MessageHandlerAsync(object sender, BasicDeliverEventArgs args)
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
                        Channel.BasicAck(args.DeliveryTag, false);
                    }
                }
            }
            return Task.FromResult(ConsumeQueue(queue.Name, MessageHandlerAsync));
        }

        protected Task<EventingBasicConsumer> ReadAndReply<TMessage, TResponse>(QueueConfiguration queue, bool autoAck, Func<object, ReceiveEventArgs<TMessage>, Task<TResponse>> handler)
        {
            EnsureQueueExists(queue);
            async void MessageHandlerAsync(object sender, BasicDeliverEventArgs args)
            {
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
                        Channel.BasicPublish(args.Exchange, props.ReplyTo, replyProps, response);
                    }
                }
                catch (Exception ex)
                {
                    // Throw or return the error.
                    if (!string.IsNullOrEmpty(props.ReplyTo))
                    {
                        var response = MessageHandler.Encode(new ErrorMessage(ex));
                        replyProps.Type = nameof(Exception);
                        Channel.BasicPublish(args.Exchange, props.ReplyTo, replyProps, response);
                    }
                    throw;
                }
                finally
                {
                    if (receivedArgs.Acknowledge)
                    {
                        Channel.BasicAck(args.DeliveryTag, false);
                    }
                }
            }
            return Task.FromResult(ConsumeQueue(queue.Name, MessageHandlerAsync));
        }

        #endregion
    }
}