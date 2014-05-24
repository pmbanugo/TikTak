using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
{
    public static class GameProcess
    {
        private static int[,] winners = new int[,]  {
						                    {0,1,2},
						                    {3,4,5},
						                    {6,7,8},
						                    {0,3,6},
						                    {1,4,7},
						                    {2,5,8},
						                    {0,4,8},
						                    {2,4,6}
                                        };

        public static int[,] Winners { get { return winners; } }

        public static bool ProcessGameState(Dictionary<int,string> board, out Nullable<PlayerSign> winner, out int[] winningIndex)
        {
            bool gameOver = false;

            for (int i = 0; i < 8; i++)
            {
                int a = winners[i, 0], b = winners[i, 1], c = winners[i, 2];

                if (board.ContainsKey(a) && board.ContainsKey(b) && board.ContainsKey(c))
                {
                    Func<KeyValuePair<int, string>, bool> containsO = input => (input.Key == a || input.Key == b || input.Key == c) && input.Value == "O";

                    Func<KeyValuePair<int, string>, bool> containsX = input => (input.Key == a || input.Key == b || input.Key == c) && input.Value == "X";

                    var oResult = board.Where(containsO).ToDictionary();
                    if (oResult.Count == 3)
                    {
                        winner = PlayerSign.Nought;
                        winningIndex = oResult.Keys.ToArray();
                        return gameOver = true;
                    }

                    var xResult = board.Where(containsX).ToDictionary();
                    if (xResult.Count == 3)
                    {
                        winner = PlayerSign.Cross;
                        winningIndex = xResult.Keys.ToArray();
                        return gameOver = true;
                    }
                }
            }
            if (board.Count == 9)
            {
                //if board is full and there is no winner, this means the game is a draw
                winner = null;
                winningIndex = new int[1];
                return gameOver = true;
            }

            winner = null;
            winningIndex = new int[1];
            return gameOver;
        }
    }
}
