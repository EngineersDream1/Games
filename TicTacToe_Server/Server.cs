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
        private TcpListener listener;
        private const int port = 2023;
        private const string host = "127.0.0.1";

        public void Start()
        {
            listener = new TcpListener(new IPEndPoint(IPAddress.Parse(host), port));
            listener.Start();
            Console.WriteLine("Сервер запущен и ожидает подключений.");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine($"Подключился новый клиент: {client.Client.RemoteEndPoint}");

                ClientHandler clientHandler = new ClientHandler(client, this);
                Thread thread = new Thread(clientHandler.HandleClient);
                thread.Start();
            }
        }
        //1я версия
        #region

        //private Socket listener;
        //private const string serverIP = "127.0.0.1";
        //private const int serverPort = 2023;
        //private ApplicationContext applicationContext;

        //public Server()
        //{
        //    applicationContext = new ApplicationContext();
        //    listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    listener.Bind(new IPEndPoint(IPAddress.Parse(serverIP), serverPort));
        //}
        
        //public void Start()
        //{
        //    listener.Listen(10);

        //    while (true)
        //    {
        //        Socket clientSocket = listener.Accept();
        //        Console.WriteLine("Клиент подключен: " +  clientSocket.RemoteEndPoint);
        //        HandleClient(clientSocket);
        //    }
        //}

        //private async void HandleClient(Socket clientSocket)
        //{
        //    byte[] buffer = new byte[1024];
        //    int bytesRead = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

        //    if (bytesRead > 0)
        //    {
        //        string reqest = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        //        string[] reqestData = reqest.Split(',');

        //        if (reqestData.Length == 2)
        //        {
        //            string username = reqestData[0];
        //            string password = reqestData[1];

        //            //Console.WriteLine(username + " : " + password);

        //            bool isAuthenticated = AuthentificatePlayer(username, password);

        //            string response = isAuthenticated ? "true" : "false";
        //            byte[] responseBuffer = Encoding.UTF8.GetBytes(response);

        //            clientSocket.Send(responseBuffer);
        //        }
        //        else if (reqestData.Length == 3 && reqestData[2] == "Register")
        //        {
        //            string username = reqestData[0];
        //            string password = reqestData[1];

        //            bool isRegistered = RegisterNewPlayer(username, password);

        //            string response = isRegistered ? "true" : "false";
        //            byte[] responseBuffer = Encoding.UTF8.GetBytes(response);

        //            clientSocket.Send(responseBuffer);
        //        }
        //    }
        //    clientSocket.Close();
        //}

        //private bool AuthentificatePlayer(string name, string pass)
        //{
        //    using(applicationContext = new ApplicationContext())
        //    {
        //        var user = applicationContext.Players.FirstOrDefault(u => u.Name == name && u.Password == pass);

        //        if (user != null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }            
        //}

        //private bool RegisterNewPlayer(string name, string password)
        //{
        //    try
        //    {
        //        using(applicationContext = new ApplicationContext())
        //        {
        //            var user = new Player() { Name = name, Password = password };
        //            applicationContext.Players.Add(user);
        //            applicationContext.SaveChanges();
        //            return true;
        //        }                
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
                       
        //}

        //public void Stop()
        //{
            
        //    listener.Dispose();
        //}

        #endregion
    }
}
