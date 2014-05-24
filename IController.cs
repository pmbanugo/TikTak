using System;
namespace TikTak
{
    public interface IController
    {
        void MakeMoveForComputerPlayer();
        void MakeMoveForHumanPlayer(int index);
        void RestartGame();
    }
}
