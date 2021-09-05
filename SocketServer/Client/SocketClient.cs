using KashkeshetCommon.Communication;
using KashkeshetCommon.MessageProtocol;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Client
{
    internal class SocketClient
    {
        public string ID { get; private set; }
        private TcpClient _tcpClient;
        private SocketProtocol _socketProtocol;

        public SocketClient(string iD, TcpClient tcpClient)
        {
            ID = iD;
            _tcpClient = tcpClient;
            _socketProtocol = new SocketProtocol();
        }

        public void SendMessage(SocketMessageProtocol socketMessageProtocol)
        {
            _socketProtocol.SendAsync(_tcpClient.GetStream(), socketMessageProtocol);
        }
        public async Task<T> ReceiveMessage<T>()
            where T: SocketMessageProtocol
        {
            return await _socketProtocol.ReceiveAsync<T>(_tcpClient.GetStream());
        }
    }
}
