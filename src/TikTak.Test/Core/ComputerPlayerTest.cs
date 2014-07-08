using TikTak.Core;
using TikTak.Core.Interfaces;
using Xunit;

namespace TikTak.Test.Core
{
    public class ComputerPlayerTest
    {
        private IComputerPlayer _computer;
        private IGameService _gameService;

        public ComputerPlayerTest()
        {
            _gameService = new GameService(new GameManager(new Board()));
            _computer = new ComputerPlayer(_gameService);
        }

        [Fact]
        public void ShouldMakeMove()
        {
            var result = false;
            _gameService.GameBoardUpdated += (sender, args) => result = true;

            _computer.MakeMove();

            Assert.True(result);
        }
    }
}
