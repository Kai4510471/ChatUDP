using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace ChatUDP.Network
{
    public class UDPSocket
    {
        private UdpClient _client;

        public IPAddress Address { get; }
        public ushort Port { get; }

        public event EventHandler<EventArgs> Download;
        public event EventHandler<EventArgs> Upload;

        public UDPSocket(ushort port) : this(IPAddress.Any, port)
        {
        }

        public UDPSocket(IPAddress address, ushort port)
        {
            Address = address;
            Port = port;
            
            _client = new UdpClient(new IPEndPoint(Address, Port));
        }

        public async Task<UdpReceiveResult> Receive()
        {
            return await _client.ReceiveAsync();
        }

        public async Task<int> Send(IPEndPoint endPoint, byte[] buffer)
        {
            return await _client.SendAsync(buffer, buffer.Length, endPoint);
        }
    }
}