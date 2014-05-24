using System;
namespace TikTak
{
    public interface IGameManager
    {
        void AddToGameBoard(int index, string value);
        PlayerSign CurrentPlayer { get; }
        PlayerSign FirstPlayer { get; }
        IBoard GameBoard { get; }
        bool GameOver { get; }
        void ValidateBoard();
        void ResetGame();
        PlayerSign? Winner { get; }
        int[] WinnerIndex { get; }
    }
}
