using Newtonsoft.Json;
using System;
using System.Text;

namespace QueueHandler.Messages
{
    class DefaultMessageHandler : IMessageHandler
    {
        #region -  Fields  -

        private readonly Encoding _encoding;

        #endregion

        #region -  Constructors  -

        public DefaultMessageHandler() : this(Encoding.UTF8) { }

        public DefaultMessageHandler(Encoding encoding)
        {
            _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding), "Supplied encoding cannot be null.");
        }

        #endregion

        #region -  Methods  -

        public T Decode<T>(byte[] body)
        {
            var json = _encoding.GetString(body);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public byte[] Encode<T>(T value)
        {
            if (value == null) { return new byte[] { }; }
            var json = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return _encoding.GetBytes(json);
        }

        #endregion
    }
}
