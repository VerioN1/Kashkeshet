using KashkeshetCommon.Communication;
using KashkeshetCommon.MessageProtocol;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Rooms
{
    internal class RoomManager
    {
        public GlobalRoom DefaulRoom { get; set; }
        public SocketProtocol _socketProtocol { get; set; }
        private Encoding _encoder;

        public RoomManager( SocketProtocol socketProtocol )
        {
            DefaulRoom = new GlobalRoom();
            _socketProtocol = socketProtocol;
            _encoder = Encoding.UTF8;
        }
        public async Task AddUserToDefualtRoom(TcpClient client)
        {
            SocketMessageProtocol a = await _socketProtocol.ReceiveAsync<SocketMessageProtocol>(client.GetStream());
            DefaulRoom.AddClient(client, a);
        }
    }
}
