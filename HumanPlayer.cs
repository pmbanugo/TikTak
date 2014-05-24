using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
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
