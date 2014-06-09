using TikTak.Core.Interfaces;

namespace TikTak.Core
{
    public class HumanPlayer : IHumanPlayer
    {
        private IGameService _gameService;
        public HumanPlayer(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void MakeMove(int index)
        {
            _gameService.AddToGameBoard(index, "X");
        }
    }
}
