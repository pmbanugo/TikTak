using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
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

        private PlayerSign _currentPlayer;
        private PlayerSign _firstPlayer;

        public GameManager(IBoard board)
        {
            GameBoard = board;

            _firstPlayer = PlayerSign.Cross;
            _currentPlayer = _firstPlayer;
            _gameOver = false;
        }

        public IBoard GameBoard { get; private set; }

        public static int[,] WinningIndex { get { return winningIndex; } }

        public int[] WinnerIndex { get; private set; }

        public PlayerSign? Winner { get; private set; }

        public bool GameOver { get { return _gameOver; } }

        public PlayerSign CurrentPlayer { get { return _currentPlayer; } }

        public PlayerSign FirstPlayer { get { return _firstPlayer; } }

        public void ValidateBoard()
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
                        Winner = PlayerSign.Nought;
                        WinnerIndex = oResult.Keys.ToArray();
                        _gameOver = true;
                    }

                    var xResult = board.Where(containsX).ToDictionary();
                    if (xResult.Count == 3)
                    {
                        Winner = PlayerSign.Cross;
                        WinnerIndex = xResult.Keys.ToArray();
                        _gameOver = true;
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
            if (GameBoard.ContainsKey(index))
                throw new Exception("Board already contains this key.");

            if (string.IsNullOrEmpty(value))
                throw new Exception(@"value cannot be null or empty. You can either enter 'X' or 'Y'");

            GameBoard.Add(index, value);

            changeCurrentPlayer();
        }

        private void changeCurrentPlayer()
        {
            _currentPlayer = _currentPlayer == PlayerSign.Cross ? PlayerSign.Nought : PlayerSign.Cross;
        }

        public void ResetGame()
        {
            GameBoard.Clear();

            //reset the current/first player
            _firstPlayer = _firstPlayer == PlayerSign.Cross ? PlayerSign.Nought : PlayerSign.Cross;
            _currentPlayer = _firstPlayer;

            _gameOver = false;
        }
    }
}
