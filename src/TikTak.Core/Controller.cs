using TikTak.Core.Interfaces;

namespace TikTak.Core
{
    public class Controller : IController
    {
        private IHumanPlayer _humanPlayer;
        private IComputerPlayer _computerPlayer;
        private IGameService _gameService;

        public Controller(IComputerPlayer computerPlayer, IHumanPlayer humanPlayer, IGameService gameService)
        {
            _computerPlayer = computerPlayer;
            _humanPlayer = humanPlayer;
            _gameService = gameService;
        }

        public void RestartGame()
        {
            _gameService.ResetGame();

            if (isComputerTurn())
            {
                MakeMoveForComputerPlayer();
            }
        }

        public void MakeMoveForHumanPlayer(int index)
        {
            _humanPlayer.MakeMove(index);
            _gameService.ProcessGameState();

            if (isComputerTurn())
            {
                MakeMoveForComputerPlayer();
            }
        }

        private bool isComputerTurn()
        {
            return _gameService.CurrentPlayer == Player.Computer;
        }

        public void MakeMoveForComputerPlayer()
        {
            _computerPlayer.MakeMove();
            _gameService.ProcessGameState();
        }
    }
}
