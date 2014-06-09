namespace TikTak.Core.Interfaces
{
    public interface IController
    {
        void MakeMoveForComputerPlayer();
        void MakeMoveForHumanPlayer(int index);
        void RestartGame();
    }
}
