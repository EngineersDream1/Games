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
        Player CurrentPlayer { get; set; } = new Player();

        public Game()
        {
            board = new string [3,3];            
            CurrentPlayer.Symbol = "X";
        }


        //Метод проверяет может ли игрок совершить ход
        public bool MakeMove(int row, int column)
        {
            /*if (CurrentPlayer.IsActive == false)  //Возможно будет необходимо для реализации более продвинутого мультиплеера
                return false;*/

            if (string.IsNullOrEmpty(board[row, column]))
            {
                board[row, column] = CurrentPlayer.Symbol;
                return true;
            }
            return false;
        }

        //Метод реализует смену хода у игроков
        public void SwitchPlayer()
        {
            CurrentPlayer.Symbol = CurrentPlayer.Symbol == "X" ? "0" : "X";
        }

        //Метод получает текущего игрока
        public string GetCurrentPlayer()
        {
            return CurrentPlayer.Symbol;
        }

        //Метод проверяет игровое поле на выйгрышные комбинации
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

        //Метод проверяет заполнено ли игровое поле полностью
        public bool IsBoardFull()
        {
            for(int row = 0;row < 3; row++)
            {
                for (int column = 0;column < 3; column++)
                {
                    if (string.IsNullOrEmpty(board[row, column])) return false;
                }
            }
            return true;
        }

        //Метод сощдает новое чистое игровое поле
        public void Reset()
        {
            board = new string[3,3];

        }
    }
}
