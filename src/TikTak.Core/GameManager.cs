using System;
using System.Collections.Generic;
using System.Linq;
using TikTak.Core.Interfaces;

namespace TikTak.Core
{
    public class GameManager : IGameManager
    {

        private static int[,] winningIndex = new int[,]  {
						                    {0,1,2},
						                    {3,4,5},
						                    {6,7,8},
						                    {0,3,6},
						                    {1,4,7},
						                    {2,5,8},
						                    {0,4,8},
						                    {2,4,6}
                                        };

        private bool _gameOver;

        private Player _currentPlayer;
        private Player _firstPlayer;

        public GameManager(IBoard board)
        {
            GameBoard = board;

            _firstPlayer = Player.Human;
            _currentPlayer = _firstPlayer;
            _gameOver = false;
            Winner = null;
        }

        public IBoard GameBoard { get; private set; }

        public int[] WinnerIndex { get; private set; }

        public Player? Winner { get; private set; }

        public bool GameOver { get { return _gameOver; } }

        public Player CurrentPlayer { get { return _currentPlayer; } }

        public void ProcessWinner()
        {
            var board = GameBoard.Content;
            for (int i = 0; i < 8; i++)
            {
                int a = winningIndex[i, 0], b = winningIndex[i, 1], c = winningIndex[i, 2];

                if (board.ContainsKey(a) && board.ContainsKey(b) && board.ContainsKey(c))
                {
                    Func<KeyValuePair<int, string>, bool> containsO = input => (input.Key == a || input.Key == b || input.Key == c) && input.Value == "O";

                    Func<KeyValuePair<int, string>, bool> containsX = input => (input.Key == a || input.Key == b || input.Key == c) && input.Value == "X";

                    var oResult = board.Where(containsO).ToDictionary();
                    if (oResult.Count == 3)
                    {
                        Winner = Player.Computer;
                        WinnerIndex = oResult.Keys.ToArray();
                        _gameOver = true;
                        return;
                    }

                    var xResult = board.Where(containsX).ToDictionary();
                    if (xResult.Count == 3)
                    {
                        Winner = Player.Human;
                        WinnerIndex = xResult.Keys.ToArray();
                        _gameOver = true;
                        return;
                    }
                }
            }
            if (board.Count == 9)
            {
                _gameOver = true;
                Winner = null;
            }
        }

        public void AddToGameBoard(int index, string value)
        {
            GameBoard.Add(index, value);
            changeCurrentPlayer();
        }

        private void changeCurrentPlayer()
        {
            _currentPlayer = _currentPlayer == Player.Human ? Player.Computer : Player.Human;
        }

        public void ResetGame()
        {
            GameBoard.Clear();

            //reset the current/first player
            _firstPlayer = _firstPlayer == Player.Human ? Player.Computer : Player.Human;
            _currentPlayer = _firstPlayer;

            _gameOver = false;
        }
    }
}
