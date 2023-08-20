﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToe
{
    public class Client
    {
        private Socket client;        
        private const string serverIP = "127.0.0.1";
        private const int serverPort = 2023;

        public Client()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);            
            //client.Connect(new IPEndPoint(IPAddress.Parse(serverIP),serverPort));
            //MessageBox.Show("Успешное подключение к серверу!");

        }

        public async Task<bool> AuthtorisingPlayer(string playerName, string password)
        {
            try
            {
                await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(serverIP), serverPort));
                MessageBox.Show("Подключено к серверу!");

                string reqest = playerName + "," + password;
                byte[] buffer = Encoding.ASCII.GetBytes(reqest);
                await client.SendAsync(buffer, SocketFlags.None);
                buffer = new byte[1024];
                int bytesRead = await client.ReceiveAsync(buffer, SocketFlags.None);

                if(bytesRead > 0)
                {
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    if(response == "true")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }            
        }
    }
}
