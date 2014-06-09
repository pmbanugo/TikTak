using System;
using TikTak.Core;
using TikTak.Core.Interfaces;
using Xunit;

namespace TikTak.Test
{
    public class GameServiceTest
    {
        private IGameManager gameManager;
        private IGameService gameService;

        public GameServiceTest()
        {
            IBoard board = new Board();
            gameManager = new GameManager(board);
            gameService = new GameService(gameManager);
        }

        [Fact]
        public void Should_ResetGame()
        {
            gameService.ResetGame();
            var boardContent = gameService.GetGameBoard().Content;
            var gameover = gameService.GameOver();

            Assert.False(boardContent.Count > 0 && gameover);
        }

        [Fact]
        public void Should_AddData_ToGameBoard()
        {
            var result = false;
            gameService.GameBoardUpdated += (sender, args) => result = true;

            gameService.AddToGameBoard(1, "O");

            Assert.True(result);
        }

        [Fact]
        public void CanPlayValue_ReturnsTrue_IfValueDoesNotExist()
        {
            var result = gameService.CanPlayValue(1);

            Assert.True(result);
        }

        [Fact]
        public void CanPlayValue_ReturnsFalse_IfValueExist()
        {
            gameService.AddToGameBoard(1, "X");
            var result = gameService.CanPlayValue(1);

            Assert.False(result);
        }

        [Fact]
        public void GameIsADraw()
        {
            //Arrange
            var result = false;

            gameService.AddToGameBoard(0, "O");
            gameService.AddToGameBoard(1, "X");
            gameService.AddToGameBoard(2, "O");
            gameService.AddToGameBoard(3, "O");
            gameService.AddToGameBoard(4, "X");
            gameService.AddToGameBoard(5, "O");
            gameService.AddToGameBoard(6, "X");
            gameService.AddToGameBoard(7, "O");
            gameService.AddToGameBoard(8, "X");
            
            gameService.GameCompleted += (sender, args) => result = args.GameState.GameDrawn;

            //Act
            gameService.ProcessGameState();

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Player_WithValues_InARow_IsWinner()
        {
            //Arrange
            gameService.AddToGameBoard(0, "X");
            gameService.AddToGameBoard(1, "X");
            gameService.AddToGameBoard(2, "X");
            gameService.AddToGameBoard(4, "O");

            Player? expectedPlayer = Player.Human;
            Player? actualPlayer = null;
            gameService.GameCompleted += (sender, args) => actualPlayer = args.Winner;

            //Act
            gameService.ProcessGameState();

            //Assert
            Assert.Equal(expectedPlayer, actualPlayer);
        }

        [Fact]
        public void Player_WithValues_In_HorizontalColumns_IsWinner()
        {
            //Arrange
            gameService.AddToGameBoard(0, "X");
            gameService.AddToGameBoard(3, "X");
            gameService.AddToGameBoard(6, "X");
            gameService.AddToGameBoard(4, "O");

            Player? expectedPlayer = Player.Human;
            Player? actualPlayer = null;
            gameService.GameCompleted += (sender, args) => actualPlayer = args.Winner;

            //Act
            gameService.ProcessGameState();

            //Assert
            Assert.Equal(expectedPlayer, actualPlayer);
        }

        [Fact]
        public void Player_WithValues_Diagonally_IsWinner()
        {
            //Arrange
            gameService.AddToGameBoard(2, "X");
            gameService.AddToGameBoard(4, "X");
            gameService.AddToGameBoard(6, "X");
            gameService.AddToGameBoard(3, "O");

            Player? expectedPlayer = Player.Human;
            Player? actualPlayer = null;
            gameService.GameCompleted += (sender, args) => actualPlayer = args.Winner;

            //Act
            gameService.ProcessGameState();

            //Assert
            Assert.Equal(expectedPlayer, actualPlayer);
        }
    }
}
