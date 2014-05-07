using System;
using System.Collections.Generic;
using System.Linq;

namespace TikTak
{
    public static class ComputerPlayer
    {
        private static int[,] Winners = Utility.Winners;

        private static int[] attackIndex = new int[] { 4,2,0,6,8 };        
        
        public static int MakeDecision(Dictionary<int, string> board)
        {
            if (board.Count < 1)//first player
            {
                return GetRandomIndexForACorner();
            }

            if (board.Count < 4) //Attack
            {
                if (!board.ContainsKey(4))
                {
                    return 4;
                }
                if (board.Count == 1)
                {
                    return GetRandomIndexForACorner();
                }

                return CheckSquaresAndGetAttackIndex(board);
            }
            //Defend
            
                //1. play to win, 
                //OR
                //2. Block opponent
                //OR
                //3. Play to the corner, and if corners are filled play to the edge

                //1. play to win
            var computerSquares = board.Where(x => x.Value == "O").ToDictionary();
            for (int i = 0; i < 8; i++)
            {
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
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
            var opponentSquares = board.Where(x => x.Value == "X").ToDictionary();
            for (int i = 0; i < 8; i++)
            {
                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
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
            return IndexForEdgeoOrCorner(board);
        }

        private static int GetRandomIndexForACorner()
        {
            var index = (new Random(2).Next(1, 5));
            return attackIndex[index - 1];
        }

        private static int IndexForEdgeoOrCorner(Dictionary<int, string> board)
        {
            var edgesIndex = new int[] {1, 3, 5, 7};
            var cornersIndex = new [] {0, 2, 6, 8};
            foreach (int index in cornersIndex)
            {
                if (!board.ContainsKey(index))
                {
                    return index;
                }
            }
            foreach (int index in edgesIndex)
            {
                if (!board.ContainsKey(index))
                {
                    return index;
                }
            }
            return 0;
        }

        private static int CheckSquaresAndGetAttackIndex(Dictionary<int, string> board)
        {
            var opponentSquares = board.Where(input => input.Value == "X").ToDictionary();
            if (opponentSquares.Count > 1)//means opponent is the first player
            {
                //Check if the opponent is about to win
                for (int i = 0; i < 8; i++)
                {
                    int a = Winners[i, 0], b = Winners[i, 1],
                        c = Winners[i, 2];

                    if (opponentSquares.TakeWhile(input => input.Key == a || input.Key == b || input.Key == c).Count() > 1)
                    {
                        //Block
                        if (!board.ContainsKey(a))
                        {
                            return a;
                        }
                        else if (!board.ContainsKey(b))
                        {
                            return b;
                        }
                        else if(!board.ContainsKey(c))
                        {
                            return c;
                        }

                    }
                }

                //Check for a straight diagonal line
                if (hasDiagonal(opponentSquares))
                {
                    //play to the edge
                    if (board.ContainsKey(7))
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
                        if (!board.ContainsKey(index))
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
                int key = board.First(input => input.Value == "O").Key;
                //check if the opponent has the center
                if (board[4] == "X")
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
                        if (!(board.ContainsKey(7) & board.ContainsKey(8)))
                        {
                            return 8;
                        }
                        return 0;
                    }
                    case 8:
                    {
                        if (!(board.ContainsKey(6) & board.ContainsKey(7)))
                        {
                            return 6;
                        }
                        return 2;
                    }
                    case 0:
                    {
                        if (!(board.ContainsKey(1) & board.ContainsKey(2)))
                        {
                            return 2;
                        }
                        return 6;
                    }
                    case 2:
                    {
                        if (!(board.ContainsKey(0) & board.ContainsKey(1)))
                        {
                            return 0;
                        }
                        return 8;
                    }
                }
            }

            return 0;//does nothing actually. just enforces a return
        }

        private static bool hasDiagonal(Dictionary<int, string> opponentSquares)
        {            
            if (opponentSquares.ContainsKey(2) && opponentSquares.ContainsKey(6))
                return true;
            if (opponentSquares.ContainsKey(0) && opponentSquares.ContainsKey(8))
                return true;
            return false;
        }
    }
}
