using System.Net.Sockets;

namespace ChatUDP.Event.Network
{
    public class DownloadEventArgs : NetworkEventArgs
    {
        public UdpReceiveResult Result { get; }

        public DownloadEventArgs(UdpReceiveResult result)
        {
            Result = result;
        }
    }
}