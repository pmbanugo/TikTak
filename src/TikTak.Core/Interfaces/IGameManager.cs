namespace TikTak.Core.Interfaces
{
    public interface IGameManager
    {
        void AddToGameBoard(int index, string value);
        Player CurrentPlayer { get; }
        IBoard GameBoard { get; }
        bool GameOver { get; }
        void ProcessWinner();
        void ResetGame();
        Player? Winner { get; }
        int[] WinnerIndex { get; }
    }
}
