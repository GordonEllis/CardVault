namespace QueueHandler.Queues
{
    public class ReceiveEventArgs<TMessage>
    {
        public ReceiveEventArgs(TMessage message, bool acknowledge)
        {
            Message = message;
            Acknowledge = acknowledge;
        }

        public TMessage Message { get; private set; }
        public bool Acknowledge { get; set; }
    }
}
