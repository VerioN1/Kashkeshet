using KashkeshetCommon.Communication;
using KashkeshetCommon.MessageProtocol;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UIConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter UserName: ");
            var userName = Console.ReadLine();
            Int32 port = 13000;
            TcpClient client = new TcpClient("127.0.0.1", port);
            NetworkStream stream = client.GetStream();

            SocketProtocol a = new SocketProtocol();
            await Sender(userName, stream, a);

            while (true)
            {
                await SendToServer(userName, stream, a);
                await Reciever(stream, a);
            }


        }

        private static async Task Sender(string userName, NetworkStream stream, SocketProtocol a)
        {
            await a.SendAsync(stream, new SocketMessageProtocol(userName, Encoding.UTF8.GetBytes($" hello from me {userName}")));
        }

        private static async Task Reciever(NetworkStream stream, SocketProtocol a)
        {
            var message = await a.ReceiveAsync<SocketMessageProtocol>(stream);
            Console.WriteLine(message);
        }
        private static async Task SendToServer(string userName, NetworkStream stream, SocketProtocol a)
        {
            Console.WriteLine("Enter Message: ");
            var msg = Console.ReadLine();
            await a.SendAsync(stream, new SocketMessageProtocol(userName, Encoding.UTF8.GetBytes(msg)));
        }
    }
}
