using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
{
    public static class GameProcess
    {
        static private int[,] Winners = Utility.Winners;

        public static bool CheckAndProcessWinner(Dictionary<int,string> board, out Nullable<PlayerType> winner, out int[] winningIndex)
        {
            bool gameOver = false;

            for (int i = 0; i < 8; i++)
            {
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];

                if (board.ContainsKey(a) && board.ContainsKey(b) && board.ContainsKey(c))
                {
                    Func<KeyValuePair<int, string>, bool> containsO = input => (input.Key == a || input.Key == b || input.Key == c) && input.Value == "O";

                    Func<KeyValuePair<int, string>, bool> containsX = input => (input.Key == a || input.Key == b || input.Key == c) && input.Value == "X";

                    var oResult = board.Where(containsO).ToDictionary();
                    if (oResult.Count == 3)
                    {
                        winner = PlayerType.PlayerO;
                        winningIndex = oResult.Keys.ToArray();
                        return gameOver = true;
                    }

                    var xResult = board.Where(containsX).ToDictionary();
                    if (xResult.Count == 3)
                    {
                        winner = PlayerType.PlayerX;
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
