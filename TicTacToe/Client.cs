using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Client
    {
        Socket client;
        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 2023);

        public Client()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(remoteEndPoint);
        }

        public bool AuthtorisingPlayer(string playerName, string password)
        {

        }
    }
}
