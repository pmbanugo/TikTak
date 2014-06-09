using System.Collections.Generic;
using TikTak.Core.Interfaces;

namespace TikTak.Core
{
    public class ComputerWithMinimax : IComputerPlayer
    {
        private IGameService _gameService;

        public ComputerWithMinimax(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void MakeMove()
        {
            
        }
    
    }
}
