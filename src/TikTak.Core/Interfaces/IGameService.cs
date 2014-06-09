using System;

namespace TikTak.Core.Interfaces
{
    public interface IGameService
    {
        event EventHandler<GameBoardUpdatedEventArgs> GameBoardUpdated;
        event EventHandler<GameCompletedEventArgs> GameCompleted;

        void AddToGameBoard(int index, string value);

        void ResetGame();

        void ProcessGameState();

        bool CanPlayValue(int index);

        bool GameOver();

        Player CurrentPlayer { get; }

        IBoard GetGameBoard();
    }
}
