using System;
using System.Collections.Generic;
using System.Text;

namespace KashkeshetCommon.MessageProtocol
{
    public class SocketMessageProtocol
    {
        public string Origin { get; set; }
        public byte[] Data { get; set; }
        public MessageType messageType { get; set; }
        
        public SocketMessageProtocol()
        {

        }

        public SocketMessageProtocol(string origin, byte[] data)
        {
            Origin = origin;
            Data = data;
        }

        public override string ToString()
        {
            return $"From : {Origin} \nMessage : {Encoding.UTF8.GetString(Data)}";
        }
    }
}
