namespace QueueHandler.Messages
{
    public interface IMessageHandler
    {
        T Decode<T>(byte[] obj);
        byte[] Encode<T>(T obj);
    }
}
