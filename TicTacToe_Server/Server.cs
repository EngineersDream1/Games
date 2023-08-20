using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace TicTacToe_Server
{
    public class Server
    {
        private Socket _listener;
        private ApplicationContext _context;

        public void Start()
        {
            _context = new ApplicationContext();
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(new IPEndPoint(IPAddress.Parse("192.168.0.186"), 2023));
            _listener.Listen(200);
            _listener.BeginAccept(AcceptCallback, null);

        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket clientSocket = _listener.EndAccept(ar);
            Console.WriteLine("Клиент подключен: " + clientSocket.RemoteEndPoint);

            byte[] buffer = new byte[1024];
            //clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new StateObject() { Socket = clientSocket, Buffer = buffer});
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var state = (StateObject) ar.AsyncState;
            var clientSocket = state.Socket;

            int bytesRead = clientSocket.EndReceive(ar);

            if (bytesRead > 0)
            {
                string username = Encoding.ASCII.GetString(state.Buffer, 0, bytesRead);
                string password = Encoding.ASCII.GetString(state.Buffer,0, bytesRead);
                bool isAuthenticated = AuthenticateUser(username, password);

                if (isAuthenticated)
                {
                    byte [] response = Encoding.UTF8.GetBytes("Succeses connect!");
                    clientSocket.Send(response);

                    clientSocket.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None, ReceiveCallback, null);
                }
            }
        }

        public bool AuthenticateUser(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == username &&  u.Password == password);
            return user != null;
        }

        public void Stop()
        {
            _context.Dispose();
        }
    }
}
