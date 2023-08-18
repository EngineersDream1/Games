using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Game
    {
        private string[,] board;
        Player CurrentPlayer { get; set; }

        public Game(Player player)
        {
            board = new string [3,3];
            CurrentPlayer = player;
        }

        public bool MakeMove(Player player, int row, int column)
        {
            if (player.IsActive == false)
                return false;

            if (string.IsNullOrEmpty(board[row, column]))
            {
                board[row, column] = player.Symbol;
                return true;
            }
            return false;
        }

        public bool CheckWin()
        {
            for(int row = 0; row < 3; row++)
            {
                if (board[row, 0] == CurrentPlayer.Symbol &&
                    board[row, 1] == CurrentPlayer.Symbol &&
                    board[row, 2] == CurrentPlayer.Symbol)
                    return true;
            }

            for(int column = 0; column < 3; column++)
            {
                if (board[0, column] == CurrentPlayer.Symbol &&
                    board[1, column] == CurrentPlayer.Symbol &&
                    board[2, column] == CurrentPlayer.Symbol)
                    return true;
            }

            if (board[0,0] == CurrentPlayer.Symbol &&
                board[1,1] == CurrentPlayer.Symbol &&
                board[2,2] == CurrentPlayer.Symbol)
                return true;

            if (board[0,2] == CurrentPlayer.Symbol &&
                board[1,1] == CurrentPlayer.Symbol &&
                board[2,0] == CurrentPlayer.Symbol)
                return true;

            return false;
        }

        public void Reset()
        {
            board = new string[3,3];

        }
    }
}
