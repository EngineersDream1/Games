using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server
{
    public class Server
    {
        private Socket listener;
        private const string serverIP = "127.0.0.1";
        private const int serverPort = 2023;
        private ApplicationContext applicationContext;

        public Server()
        {
            applicationContext = new ApplicationContext();
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(new IPEndPoint(IPAddress.Parse(serverIP), serverPort));
        }
        
        public void Start()
        {
            listener.Listen(10);

            while (true)
            {
                Socket clientSocket = listener.Accept();
                HandleClient(clientSocket);
            }
        }

        private async void HandleClient(Socket clientSocket)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = await Task.Factory.FromAsync(clientSocket.BeginReceive, clientSocket.EndReceive, buffer, 0, buffer.Length, TaskCreationOptions.None);

            if (bytesRead > 0)
            {
                string reqest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                string[] reqestData = reqest.Split(',');

                if (reqestData.Length == 2)
                {
                    string username = reqestData[0];
                    string password = reqestData[1];

                    bool isAuthentificete = AuthentificatePlayer(username, password);
                }
            }
        }

        private bool AuthentificatePlayer(string name, string pass)
        {
            applicationContext = new ApplicationContext();
            var user = applicationContext.Players.FirstOrDefault(u => u.Name == name && u.Password == pass);

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
