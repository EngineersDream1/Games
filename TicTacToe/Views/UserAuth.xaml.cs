﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Client client;

        public UserAuth()
        {
            InitializeComponent();
        }

        private void EntrBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(NameTxt.Text) || string.IsNullOrEmpty(PassTxt.Text))
            {
                return;
            }
            else
            {
                client = new Client();
            }
        }
    }
}
