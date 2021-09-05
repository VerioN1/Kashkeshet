using KashkeshetCommon.MessageProtocol;
using KashkeshetCommon.Serializer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace KashkeshetCommon.Encoder
{
    public class NStreamEncoder
    {
        public Encoding _encoding { get; private set; }

        public NStreamEncoder(Encoding encoding)
        {
            _encoding = encoding;
        }
        public (byte[] header, byte[] body) Encode<T>(T message)
            where T : SocketMessageProtocol
        {
            string stringfyMessage = JsonSerialization.Serialize<T>(message);
            var bodyBytes = _encoding.GetBytes(stringfyMessage);
            var headerBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(bodyBytes.Length));

            return (headerBytes, bodyBytes);
        }
        public T Decode<T>(byte[] body)
            where T : SocketMessageProtocol
        {
            var str = Encoding.UTF8.GetString(body);
            return JsonSerialization.Desrialize<T>(str);
        }
    }
}
