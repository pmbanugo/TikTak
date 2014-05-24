using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TikTak
{
    public class GameCompletedEventArgs
    {
        public GameCompletedEventArgs(GameState gameState, Player? winner,int[] winnerIndex)
        {
            GameState = gameState;
            Winner = winner;
            WinnerIndex = winnerIndex;
        }

        public GameState GameState { get; private set; }
        public Player? Winner { get; set; }
        public int[] WinnerIndex { get; set; }

    }
}
