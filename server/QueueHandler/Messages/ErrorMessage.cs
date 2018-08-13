using System;

namespace QueueHandler.Messages
{
    public class ErrorMessage
    {
        public ErrorMessage() { }

        public ErrorMessage(Exception ex)
        {
            Message = ex.Message;
            Type = ex.GetType().Name;
            Source = ex.Source;
        }

        public string Message { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
    }
}
