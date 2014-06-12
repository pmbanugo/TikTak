using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TikTak.Core.Interfaces;

namespace TikTak.Core
{
    public class GameService : IGameService
    {
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

        protected virtual void OnGameCompleted(GameState gameState)
        {
            if (GameCompleted != null)
            {
                GameCompleted(this, new GameCompletedEventArgs(gameState));
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

            //_gameManager.ProcessWinner();

            //if (GameOver())
            //{
            //    var gameState = new GameState { GameOver = true, GameDrawn = _gameManager.Winner == null };
            //    Player? winner = _gameManager.Winner;
            //    int[] winnerIndex = _gameManager.WinnerIndex;

            //    OnGameCompleted(gameState, winner, winnerIndex);
            //}
            var gameState = _gameManager.ProcessWinner();
            if (gameState.GameOver)
            {
                OnGameCompleted(gameState);
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

        public bool GameOver()
        {
            return _gameManager.GameState.GameOver;
        }

        public Player CurrentPlayer
        {
            get { return _gameManager.CurrentPlayer; }
        }
    }
}
