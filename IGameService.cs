using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
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

        PlayerSign NextPlayer { get; }

        IBoard GetGameBoard();
    }
}
