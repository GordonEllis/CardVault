﻿namespace QueueHandler.Messages
{
    public class MessageResponse<TResponse>
    {
        public bool Success { get; set; }
        public TResponse Response { get; set; }
        public ErrorMessage Error { get; set; }
    }
}
