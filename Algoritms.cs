//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TikTak
//{
//    class Algoritms
//    {
//        static readonly int[,] Winners = Utility.Winners;

//        static int BlockOpponent2(Dictionary<int, string> board)
//        {
//            var opponentSquares = board.Where(x => x.Value == "X").ToDictionary();
//            for (int i = 0; i < 8; i++)
//            {
//                int a = Winners[i, 0], b = Winners[i, 1], c = Winners[i, 2];
//                int count = 0;
//                int[] winningRow = { a, b, c };

//                for (int j = 0; j < winningRow.Length; j++)
//                {
//                    if (opponentSquares.ContainsKey(winningRow[j]))
//                    {
//                        count++;
//                    }
//                }
//                if (count > 1)
//                {
//                    foreach (int rowIndex in winningRow)
//                    {
//                        if (!board.ContainsKey(rowIndex))
//                        {
//                            return rowIndex;
//                        }
//                    }
//                }
//            }
            
//        }
        

//        static int BlockOpponent1(Dictionary<int, string> board)
//        {
//            var opponentSquares = board.Where(input => input.Value == "X").ToDictionary();

//            //Check if the opponent is about to win
//            for (int i = 0; i < 8; i++)
//            {
//                int a = Winners[i, 0], b = Winners[i, 1],
//                    c = Winners[i, 2];

//                if (opponentSquares.TakeWhile(input => input.Key == a || input.Key == b || input.Key == c).Count() > 1)
//                {
//                    //Block
//                    if (!board.ContainsKey(a))
//                    {
//                        return a;
//                    }
//                    else if (!board.ContainsKey(b))
//                    {
//                        return b;
//                    }
//                    else if (!board.ContainsKey(c))
//                    {
//                        return c;
//                    }
//                }
//            }
//        }
//    }
//}