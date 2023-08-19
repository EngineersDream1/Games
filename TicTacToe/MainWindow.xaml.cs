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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;
        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int row = Grid.GetRow(button) - 1;
            int column = Grid.GetColumn(button);

            if(game.MakeMove(row, column))
            {
                button.Content = game.GetCurrentPlayer();
                if (game.CheckWin())
                {
                    MessageBox.Show("Игрок " + game.GetCurrentPlayer().ToString() + " победил!");
                    game.Reset();
                    ResetButtons();
                    return;
                }
                game.SwitchPlayer();
            }            
        }

        private void ResetButtons()
        {
            foreach(var button in grid.Children.OfType<Button>())
            {
                button.Content = String.Empty;
            }
        }
    }
}
