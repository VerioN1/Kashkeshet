using KashkeshetCommon.Communication;
using SocketServer.Rooms;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");

            // TcpListener server = new TcpListener(port);
            var server = new TcpListener(localAddr, port);
            RoomManager roomManager = new RoomManager(new SocketProtocol());
            server.Start();

            while (true)
            {
                Console.Write("Waiting for a connection... ");
                TcpClient client = server.AcceptTcpClient();
                _ = Task.Run(async() =>
                {
                    await roomManager.AddUserToDefualtRoom(client);
                });
            }
        }
    }
}
