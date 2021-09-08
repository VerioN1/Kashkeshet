using KashkeshetCommon.MessageProtocol;
using SocketServer.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Rooms
{
    class GlobalRoom
    {
        public IReadOnlyDictionary<string, SocketClient> Clients { get; private set; }
        private IDictionary<string, SocketClient> _clients;

        public GlobalRoom()
        {
            _clients = new Dictionary<string, SocketClient>();
        }
        public void AddClient(TcpClient client, SocketMessageProtocol message)
        {
            var socketClient = new SocketClient(message.Origin, client);
            _clients.Add(message.Origin, socketClient);
            _clients.SkipWhile(x => x.Key == message.Origin).ToList().ForEach(x => x.Value.SendMessage(new SocketMessageProtocol(message.Origin, Encoding.UTF8.GetBytes($"{message.Origin} just joined the Global Chat Room "))));
            StartUserChat(socketClient);
        }

        private void StartUserChat(SocketClient socketClient)
        {
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    var msg = await socketClient.ReceiveMessage<SocketMessageProtocol>();
                    _clients.Keys.ToList().Where(x => x != msg.Origin).ToList().ForEach(x => _clients[x].SendMessage(msg));
                }
            });
        }
    }
}
