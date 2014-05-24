using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
{
    public class GameService : IGameService
    {
        //TODO: Decide if these events should be in gameService or GameController
        public event EventHandler<GameBoardUpdatedEventArgs> GameBoardUpdated;
        public event EventHandler<GameCompletedEventArgs> GameCompleted;


        private IGameManager _gameManager;

        public GameService(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void AddToGameBoard(int index, string value)
        {
            _gameManager.AddToGameBoard(index, value);
            OnGameBoardUpdated();
        }

        protected virtual void OnGameBoardUpdated()
        {
            if (GameBoardUpdated != null)
            {
                GameBoardUpdated(this,new GameBoardUpdatedEventArgs(_gameManager.GameBoard));
            }
        }

        protected virtual void OnGameCompleted(GameState gameState, Player? winner, int[] winnerIndex)
        {
            if (GameCompleted != null)
            {
                GameCompleted(this, new GameCompletedEventArgs(gameState, winner, winnerIndex));
            }
        }

        public void ResetGame()
        {
            _gameManager.ResetGame();
        }

        public void ProcessGameState()
        {
            if (_gameManager.GameBoard.Content.Count < 4)
            {
                return;
            }

            _gameManager.ValidateBoard();

            if (GameOver())
            {
                var gameState = new GameState { GameOver = true, GameDrawn = _gameManager.Winner == null ? true : false };
                Player? winner = getWinner();
                int[] winnerIndex = _gameManager.WinnerIndex;
                OnGameCompleted(gameState, winner, winnerIndex);
            }
        }

        public IBoard GetGameBoard()
        {
            return _gameManager.GameBoard;
        }

        public bool CanPlayValue(int index)
        {
            return !_gameManager.GameBoard.ContainsKey(index);
        }

        private Player? getWinner()
        {
            Player? player = null;
            switch (_gameManager.Winner)
            {
                case PlayerSign.Cross:
                    {
                        player = Player.Human;
                        break;
                    }
                case PlayerSign.Nought:
                    {
                        player = Player.Computer;
                        break;
                    }
                default:
                    break;                    
            }

            return player;
        }

        public bool GameOver()
        {
            return _gameManager.GameOver;
        }

        public PlayerSign NextPlayer
        {
            get
            {
                return _gameManager.CurrentPlayer;
            }
        }
    }
}
