namespace TikTak.Core.Interfaces
{
    public interface IGameManager
    {
        void AddToGameBoard(int index, string value);
        Player CurrentPlayer { get; }
        IBoard GameBoard { get; }
        GameState GameState { get; }
        GameState ProcessWinner();
        void ResetGame();
    }
}
