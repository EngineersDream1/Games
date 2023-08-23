using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server
{
    public class ClientHandler
    {
        private Server server;
        private NetworkStream stream;
        private TcpClient client;
        private ApplicationContext applicationContext;

        public ClientHandler(TcpClient client, Server server)
        {
            this.server = server;
            this.client = client;
        }

        public void HandleClient()
        {
            stream = client.GetStream();

            while (true)
            {
                try
                {
                    byte[] data = new byte[4096];
                    int bytes = stream.Read(data, 0, data.Length);
                    string message = Encoding.UTF8.GetString(data);

                    if(message.StartsWith("LOGIN:"))
                    {
                        string[] loginData = message.Substring(6).Split(':');
                        string playerName = loginData[0];
                        string playerPassword = loginData[1];

                        bool isAuthenticated = AuthentificatePlayer(playerName, playerPassword);

                        if (isAuthenticated)
                        {
                            Console.WriteLine($"Игрок {playerName} зашел в игру.");
                            SendMessage("true");                            
                        }
                        else
                        {
                            Console.WriteLine("Ошибка при попытке входа игрока.");
                            SendMessage("false");
                        }
                    }
                    else if(message.StartsWith("REGISTER:"))
                    {
                        string[] registerData = message.Substring(9).Split(':');
                        string playerName = registerData[0];
                        string playerPassword = registerData[1];

                        bool isRegistered = RegisterNewPlayer(playerName, playerPassword);

                        if (isRegistered)
                        {
                            Console.WriteLine($"Зарегистрирован новый пользователь {playerName}.");
                            SendMessage("true");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка при решистрации нового игрока.");
                            SendMessage("false");
                        }
                    }
                    else if(message.StartsWith("Move:"))
                    {
                        //Реализация совершения хода пользователем
                        Console.WriteLine("Пользователь совершил ход.");
                    }
                    else
                    {
                        Console.WriteLine("Возникла непредвиденная ошибка.");
                    }

                }
                catch(Exception e)
                {
                    Console.WriteLine("Ошибка чтения данных от клиента: " + e.Message);
                    break;
                }
            }
        }

        private void SendMessage(string message)
        {
            byte[] mes = Encoding.UTF8.GetBytes(message);
            stream.Write(mes, 0, mes.Length);
        }

        private bool AuthentificatePlayer(string name, string pass)
        {
            using (applicationContext = new ApplicationContext())
            {
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

        private bool RegisterNewPlayer(string name, string password)
        {
            try
            {
                using (applicationContext = new ApplicationContext())
                {
                    var user = new Player() { Name = name, Password = password };
                    applicationContext.Players.Add(user);
                    applicationContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
    }
}
