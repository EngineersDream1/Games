using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicTacToe.Views
{
    /// <summary>
    /// Логика взаимодействия для UserAuth.xaml
    /// </summary>
    public partial class UserAuth : Window
    {
        private TcpClient client;
        private NetworkStream stream;
        private Thread clientThread;
        private MainWindow mainWindow;


        public UserAuth()
        {
            InitializeComponent();            
        }

        private void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024];
                    int bytes = stream.Read(data, 0, data.Length);
                    string message = Encoding.UTF8.GetString(data, 0, bytes);

                    if(message == "true")
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            mainWindow = new MainWindow();
                            mainWindow.Show();
                            this.Close();
                        }));                        
                    }

                    //Dispatcher.Invoke(() =>
                    //{
                    //    ChatTextBox.Text += message + Environment.NewLine;
                    //});
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения данных: " + ex.Message);
                    break;
                }
            }            
        }

        private void EntrBtn_Click(object sender, RoutedEventArgs e)
        {
            
            #region
            try
            {
                client = new TcpClient("127.0.0.1", 2023);
                stream = client.GetStream();

                clientThread = new Thread(ReceiveMessages);
                clientThread.Start();

                string username = NameTxt.Text;
                string password = PassTxt.Text;

                string loginMessage = $"LOGIN:{username}:{password}";
                SendMessage(loginMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }                       

            #endregion                      
        }



        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {

            #region

            try
            {
                client = new TcpClient("127.0.0.1", 2023);
                stream = client.GetStream();
                clientThread = new Thread(ReceiveMessages);
                clientThread.Start();

                string username = NameTxt.Text;
                string password = PassTxt.Text;

                string registerMessage = $"REGISTER:{username}:{password}";
                SendMessage(registerMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            

            #endregion           
        }
    }
}
