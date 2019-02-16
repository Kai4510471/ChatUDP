using System;
using System.Net;
using System.Text;
using System.Threading;
using ChatUDP.Event.Network;
using ChatUDP.Network;

namespace ChatUDP
{
    public class ChatUDP
    {
        private UDPSocket _socket;
        private Thread _networkThread;
        private bool _isKilled;
        
        public ChatUDP()
        {
            _socket = new UDPSocket(12345);
            _socket.Download += Socket_OnDownload;
            _socket.Upload += Socket_OnUpload;
        }

        public void Start()
        {
            _networkThread = new Thread(OnReceive);
            _networkThread.Name = "NetworkThread";
            _networkThread.Start();
            IPAddress ip = null;
            while(ip == null)
            {
                Console.WriteLine("IPアドレスを入力");
                string address = Console.ReadLine();
                try
                {
                    ip = IPAddress.Parse(address);
                }
                catch(FormatException e)
                {
                    Console.WriteLine(e);
                }

            }
            Console.WriteLine("<Chat>");
            InputLoop(ip);
            
        }

        private void InputLoop(IPAddress ip)
        {
            while (true)
            {
                _socket.Send(new IPEndPoint(ip, 12345), Encoding.UTF8.GetBytes(Console.ReadLine()));
            }
        }

        public void OnReceive()
        {
            while (!_isKilled)
            {
                _socket.Receive();
            }
        }

        private void Socket_OnUpload(object sender, UploadEventArgs e)
        {
        }

        private void Socket_OnDownload(object sender, DownloadEventArgs e)
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.Result.Buffer));
        }
    }
}