using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
{
    public class ComputerWithMinimax : IComputerPlayer
    {
        public ComputerWithMinimax(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void MakeMove()
        {
            IBoard board = _gameService.GetGameBoard();
            //var indexToPlay = MakeDecision(board);
            //_gameService.AddToGameBoard(indexToPlay, "O");
            evaluatePosition(board.Content);
        }

        private int[,] winningIndex = new int[,]  {
						                    {0,1,2},
						                    {3,4,5},
						                    {6,7,8},
						                    {0,3,6},
						                    {1,4,7},
						                    {2,5,8},
						                    {0,4,8},
						                    {2,4,6}
                                        };

        int[,] Heuristic_Array = new int[,] {
                    {     0,   -10,  -100, -1000 },
                    {    10,     0,     0,     0 },
                    {   100,     0,     0,     0 },
                    {  1000,     0,     0,     0 }
        };
        private IGameService _gameService;

        int evaluatePosition(Dictionary<int, string> board)
        {
            int players, others, t = 0;
            for (int i = 0; i < 8; i++)
            {
                players = others = 0;
                for (int j = 0; j < 3; j++)
                {
                    string piece;
                    if (board.TryGetValue(winningIndex[i, j], out piece))
                    {
                        if (piece == "O")
                        {
                            players++;
                        }
                        else if (piece == "X")
                        {
                            others++;
                        }
                    }                    
                }
                t += Heuristic_Array[players, others];
            }
            return t;
        }
    
    }
}
