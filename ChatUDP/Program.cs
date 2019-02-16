using System;
using System.Net.Sockets;
using System.Text;
using ChatUDP.Network;

namespace ChatUDP
{
    class Program
    {
        static void Main(string[] args)
        {
            ChatUDP chatUdp = new ChatUDP();
            chatUdp.Start();
        }
    }
}