using System.Collections.Generic;
using TikTak.Core;
using TikTak.Core.Interfaces;
using Xunit;

namespace TikTak.Test
{
    public class GameControllerTest
    {
        private IGameService gameService;
        private IController controller;

        public GameControllerTest()
        {
            IBoard board = new Board();
            IGameManager gameManager = new GameManager(board);
            gameService = new GameService(gameManager);
            IComputerPlayer computerPlayer = new ComputerPlayer(gameService);
            IHumanPlayer humanPlayer = new HumanPlayer(gameService);
            controller = new Controller(computerPlayer, humanPlayer, gameService);
        }

        [Fact]
        public void Should_MakeMove_For_ComputerPlayer()
        {
            var result = false;
            gameService.GameBoardUpdated += (sender, args) => result = true;

            controller.MakeMoveForComputerPlayer();

            Assert.True(result);
        }

        [Fact]
        public void Should_MakeMove_For_HumanPlayer()
        {
            var result = false;
            gameService.GameBoardUpdated += (sender, args) => result = true;

            controller.MakeMoveForHumanPlayer(2);

            Assert.True(result);
        }

        [Fact]
        public void ShouldRestartGame()
        {
            controller.RestartGame();
            var actualResult = gameService.CurrentPlayer;

            Assert.Equal(Player.Human, actualResult);
        }
    }
}
