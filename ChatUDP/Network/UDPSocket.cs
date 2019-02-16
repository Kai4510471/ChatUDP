using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Threading.Tasks;
using ChatUDP.Event.Network;

namespace ChatUDP.Network
{
    public class UDPSocket
    {
        private UdpClient _client;

        public IPAddress Address { get; }
        public ushort Port { get; }
        
        public long DownloadLengths { get; private set; }
        public long UploadLengths { get; private set; }

        public event EventHandler<DownloadEventArgs> Download;
        public event EventHandler<UploadEventArgs> Upload;

        public UDPSocket(ushort port) : this(IPAddress.Any, port)
        {
        }

        public UDPSocket(IPAddress address, ushort port)
        {
            Address = address;
            Port = port;
            
            _client = new UdpClient(new IPEndPoint(Address, Port));
        }

        public async void ReceiveAsync()
        {
            UdpReceiveResult result = await _client.ReceiveAsync();
            DownloadLengths += result.Buffer.Length;
            OnDownload(this, new DownloadEventArgs(result));
        }

        public void Receive()
        {
            IPEndPoint endPoint = null;
            byte[] buffer = _client.Receive(ref endPoint);
            DownloadLengths += buffer.Length;
            OnDownload(this, new DownloadEventArgs(new UdpReceiveResult(buffer, endPoint)));
        }

        public async void SendAsync(IPEndPoint endPoint, byte[] buffer)
        {
            int len = await _client.SendAsync(buffer, buffer.Length, endPoint);
            UploadLengths += len;
            OnUpload(this, new UploadEventArgs(len));
        }

        public void Send(IPEndPoint endPoint, byte[] buffer)
        {
            int len = _client.Send(buffer, buffer.Length, endPoint);
            UploadLengths += len;
            OnUpload(this, new UploadEventArgs(len));
        }

        private void OnDownload(object sender, DownloadEventArgs args)
        {
            Download?.Invoke(sender, args);
        }

        private void OnUpload(object sender, UploadEventArgs args)
        {
            Upload?.Invoke(sender, args);
        }
    }
}