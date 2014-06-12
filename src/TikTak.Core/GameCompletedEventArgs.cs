using System;

namespace TikTak.Core
{
    public class GameCompletedEventArgs : EventArgs
    {
        public GameCompletedEventArgs(GameState gameState)
        {
            GameState = gameState;
        }

        public GameState GameState { get; private set; }

    }
}
