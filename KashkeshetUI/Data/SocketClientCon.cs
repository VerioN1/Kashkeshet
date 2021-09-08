using KashkeshetCommon.Communication;
using KashkeshetCommon.MessageProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace KashkeshetUI.Data
{
    public static class SocketClientCon
    {
        private static int rndNumber = new Random().Next(1, 90);
        public static string userName = $"alon-{rndNumber}";
        private static TcpClient Client = new TcpClient("127.0.0.1", 13000);
        public static NetworkStream Stream = Client.GetStream();
        public static SocketProtocol CommunicationProtocol =  new SocketProtocol();
        public static void SendMessage()
        {
           
        }
        private static async Task<SocketMessageProtocol> Reciever()
        {
            var message = await CommunicationProtocol.ReceiveAsync<SocketMessageProtocol>(Stream);
            return message;
        }
    }
}
