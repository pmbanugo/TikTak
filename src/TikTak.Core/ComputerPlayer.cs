using System;
using System.Collections.Generic;
using System.Linq;
using TikTak.Core.Interfaces;

namespace TikTak.Core
{
    public  class ComputerPlayer : IComputerPlayer
    {
        private IGameService _gameService;

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

        private int[] attackIndex = { 4, 2, 0, 6, 8 };

        public ComputerPlayer(IGameService gameservice)
        {
            _gameService = gameservice;
        }      
        
        public  int MakeDecision(IBoard board)
        {
            var boardContent = board.Content;
            if (boardContent.Count < 1)//first player
            {
                return GetRandomIndexForACorner();
            }

            if (boardContent.Count < 4) //Attack
            {
                if (!board.ContainsKey(4))
                {
                    return 4;
                }
                if (boardContent.Count == 1)
                {
                    return GetRandomIndexForACorner();
                }

                return CheckSquaresAndGetAttackIndex(boardContent);
            }
            //Defend
            
                //1. play to win, 
                //OR
                //2. Block opponent
                //OR
                //3. Play to the corner, and if corners are filled play to the edge

                //1. play to win
            var computerSquares = boardContent.Where(x => x.Value == "O").ToDictionary();
            for (int i = 0; i < 8; i++)
            {
                int a = winningIndex[i, 0], b = winningIndex[i, 1], c = winningIndex[i, 2];
                int count = 0;
                int[] winningRow = {a, b, c};

                for (int j = 0; j < winningRow.Length; j++)
                {
                    if (computerSquares.ContainsKey(winningRow[j]))
                    {
                        count++;
                    }
                }
                if (count > 1)
                {
                    foreach (int rowIndex in winningRow)
                    {
                        if (!board.ContainsKey(rowIndex))
                        {
                            return rowIndex;
                        }
                    }
                }
            }

            //2. Block opponent
            var opponentSquares = boardContent.Where(x => x.Value == "X").ToDictionary();
            for (int i = 0; i < 8; i++)
            {
                int a = winningIndex[i, 0], b = winningIndex[i, 1], c = winningIndex[i, 2];
                int count = 0;
                int[] winningRow = { a, b, c };

                for (int j = 0; j < winningRow.Length; j++)
                {
                    if (opponentSquares.ContainsKey(winningRow[j]))
                    {
                        count++;
                    }
                }
                if (count > 1)
                {
                    foreach (int rowIndex in winningRow)
                    {
                        if (!board.ContainsKey(rowIndex))
                        {
                            return rowIndex;
                        }
                    }
                }
            }

            //3. Play to the corner, and if corners are filled play to the edge
            return IndexForEdgeoOrCorner(boardContent);
        }

        private  int GetRandomIndexForACorner()
        {
            var index = (new Random(2).Next(1, 5));
            return attackIndex[index - 1];
        }

        private  int IndexForEdgeoOrCorner(Dictionary<int, string> boardContent)
        {
            var edgesIndex = new int[] {1, 3, 5, 7};
            var cornersIndex = new [] {0, 2, 6, 8};
            foreach (int index in cornersIndex)
            {
                if (!boardContent.ContainsKey(index))
                {
                    return index;
                }
            }
            foreach (int index in edgesIndex)
            {
                if (!boardContent.ContainsKey(index))
                {
                    return index;
                }
            }
            return 0;
        }

        private  int CheckSquaresAndGetAttackIndex(Dictionary<int, string> boardContent)
        {
            var opponentSquares = boardContent.Where(input => input.Value == "X").ToDictionary();
            if (opponentSquares.Count > 1)//means opponent is the first player
            {
                //Check if the opponent is about to win
                for (int i = 0; i < 8; i++)
                {
                    int a = winningIndex[i, 0], b = winningIndex[i, 1],
                        c = winningIndex[i, 2];

                    if (opponentSquares.TakeWhile(input => input.Key == a || input.Key == b || input.Key == c).Count() > 1)
                    {
                        //Block
                        if (!boardContent.ContainsKey(a))
                        {
                            return a;
                        }
                        else if (!boardContent.ContainsKey(b))
                        {
                            return b;
                        }
                        else if(!boardContent.ContainsKey(c))
                        {
                            return c;
                        }

                    }
                }

                //Check for a straight diagonal line
                if (hasDiagonal(opponentSquares))
                {
                    //play to the edge
                    if (boardContent.ContainsKey(7))
                    {
                        return 1;
                    }
                    return 7;
                }
                else
                {
                    //Play to the corner, inorder to prevent a fork
                    for (int j = 1; j < attackIndex.Length; j++)
                    {
                        var index = attackIndex[j];
                        if (!boardContent.ContainsKey(index))
                        {
                            return index;
                        }
                    }

                    //the opponent has the edges, then play to the corners furthest to them
                    if (opponentSquares.ContainsKey(0))
                    {
                        return 8;
                    }
                    else if (opponentSquares.ContainsKey(8))
                    {
                        return 0;
                    }
                    else if (opponentSquares.ContainsKey(2))
                    {
                        return 6;
                    }
                    else if (opponentSquares.ContainsKey(6))
                    {
                        return 2;
                    }
                }

            }
            else 
            {
                int key = boardContent.First(input => input.Value == "O").Key;
                //check if the opponent has the center
                if (boardContent[4] == "X")
                {
                    //form a diagonal
                    switch (key)
                    {
                        case 0:
                            return 8;
                        case 8:
                            return 0;
                        case 2:
                            return 6;
                        case 6:
                            return 2;
                    }
                }
                //Pick a row that doesn’t contain your opponent’s move. and play to the corner
                switch (key)
                {
                    case 6:
                    {
                        if (!(boardContent.ContainsKey(7) & boardContent.ContainsKey(8)))
                        {
                            return 8;
                        }
                        return 0;
                    }
                    case 8:
                    {
                        if (!(boardContent.ContainsKey(6) & boardContent.ContainsKey(7)))
                        {
                            return 6;
                        }
                        return 2;
                    }
                    case 0:
                    {
                        if (!(boardContent.ContainsKey(1) & boardContent.ContainsKey(2)))
                        {
                            return 2;
                        }
                        return 6;
                    }
                    case 2:
                    {
                        if (!(boardContent.ContainsKey(0) & boardContent.ContainsKey(1)))
                        {
                            return 0;
                        }
                        return 8;
                    }
                }
            }

            return 0;//does nothing actually. just enforces a return
        }

        private  bool hasDiagonal(Dictionary<int, string> opponentSquares)
        {            
            if (opponentSquares.ContainsKey(2) && opponentSquares.ContainsKey(6))
                return true;
            if (opponentSquares.ContainsKey(0) && opponentSquares.ContainsKey(8))
                return true;
            return false;
        }

        public void MakeMove()
        {
            IBoard board = _gameService.GetGameBoard();
            var indexToPlay = MakeDecision(board);
            _gameService.AddToGameBoard(indexToPlay, "O");
        }
    }
}
