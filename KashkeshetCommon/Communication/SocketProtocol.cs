using KashkeshetCommon.Encoder;
using KashkeshetCommon.MessageProtocol;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KashkeshetCommon.Communication
{
    public class SocketProtocol
    {
        public NStreamEncoder nStreamEncoder { get; set; }

        public SocketProtocol(NStreamEncoder nStreamEncoder)
        {
            this.nStreamEncoder = nStreamEncoder;
        }

        public SocketProtocol()
        {
            nStreamEncoder = new NStreamEncoder(Encoding.UTF8);
        }
        public async Task SendAsync<T>(NetworkStream networkStream, T message)
            where T: SocketMessageProtocol
        {
            var (header, body) = nStreamEncoder.Encode(message);
            await networkStream.WriteAsync(header, 0, header.Length).ConfigureAwait(false);
            await networkStream.WriteAsync(body, 0, body.Length).ConfigureAwait(false);
        }
        public async Task<T> ReceiveAsync<T>(NetworkStream networkStream)
            where T : SocketMessageProtocol
        {
            var headerBytes = await ReadAsync(networkStream, 4);
            var bodyLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(headerBytes, 0));

            var bodyBytes = await ReadAsync(networkStream, bodyLength);

            return nStreamEncoder.Decode<T>(bodyBytes);
        }
        public async Task<byte[]> ReadAsync(NetworkStream networkStream, int bytesToRead)
        {
            var buffer = new byte[bytesToRead];
            var bytesRead = 0;
            while (bytesRead < bytesToRead)
            {
                var bytesReceived = await networkStream.ReadAsync(buffer, bytesRead, (bytesToRead - bytesRead)).ConfigureAwait(false);
                if (bytesReceived == 0)
                    throw new Exception("Socket Closed");
                bytesRead += bytesReceived;
            }
            return buffer;
        }
    }
}
