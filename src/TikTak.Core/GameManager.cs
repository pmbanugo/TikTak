using System;
using System.Collections.Generic;
using System.Linq;
using TikTak.Core.Interfaces;

namespace TikTak.Core
{
    public class GameManager : IGameManager
    {
        private Player _currentPlayer;
        private Player _firstPlayer;
        private Dictionary<int, string> boardContent;
        private Winner noWinner;

        public GameManager(IBoard board)
        {
            GameBoard = board;
            _firstPlayer = Player.Human;
            _currentPlayer = _firstPlayer;
            boardContent = GameBoard.Content;
            noWinner = null;
            GameState = new GameState {GameOver = false};
        }

        public IBoard GameBoard { get; private set; }

        public Player CurrentPlayer { get { return _currentPlayer; } }

        public GameState GameState { get; private set; }

        public void AddToGameBoard(int index, string value)
        {
            GameBoard.Add(index, value);
            changeCurrentPlayer();
        }

        public void ResetGame()
        {
            GameBoard.Clear();

            //reset the current/first player
            _firstPlayer = _firstPlayer == Player.Human ? Player.Computer : Player.Human;
            _currentPlayer = _firstPlayer;

            GameState.GameOver = false;
        }

        public GameState ProcessWinner()
        {
            var winner = checkForThreeIdenticalValueInColumns();
            if (winner != noWinner)
            {
                setWinningGameState(winner);
                return GameState;
            }

            winner = checkForThreeIdenticalValueInRows();
            if (winner != noWinner)
            {
                setWinningGameState(winner);
                return GameState;
            }

            winner = checkForThreeIdenticalValueDiagonally();
            if (winner != noWinner)
            {
                setWinningGameState(winner);
                return GameState;
            }

            if (boardContent.Count == 9)
            {
                setDrawGameState();
                return GameState;
            }

            setNoWinnerState();
            return GameState;
        }

        private void setNoWinnerState()
        {
            GameState.GameOver = false;
        }

        private void setDrawGameState()
        {
            GameState.GameOver = true;
            GameState.GameDrawn = true;
            GameState.Winner = noWinner;
        }

        private void setWinningGameState(Winner winner)
        {
            GameState.GameDrawn = false;
            GameState.GameOver = true;
            GameState.Winner = winner;
        }

        private Winner checkWinner(int firstIndex, int secondIndex, int thirdIndex)
        {
            var oResult = boardContent.Where(firstIndex, secondIndex, thirdIndex, "O").ToDictionary();
            if (oResult.Count == 3)
            {
                return new Winner() { Player = Player.Computer, WinnerIndex = oResult.Keys.ToArray() };
            }

            var xResult = boardContent.Where(firstIndex, secondIndex, thirdIndex, "X").ToDictionary();
            if (xResult.Count == 3)
            {
                return new Winner {Player = Player.Human, WinnerIndex = xResult.Keys.ToArray()};
            }
            return noWinner;
        }

        private Winner checkForThreeIdenticalValueInColumns()
        {
            var rowIndex = new[,]
            {
                {0, 3, 6},
                {1, 4, 7},
                {2, 5, 8}
            };

            for (int i = 0; i < rowIndex.GetLength(0); i++)
            {
                int firstIndex = rowIndex[i, 0], secondIndex = rowIndex[i, 1], thirdIndex = rowIndex[i, 2];

                var winner = checkWinner(firstIndex, secondIndex, thirdIndex);
                if (winner != noWinner)
                    return winner;
            }

            return noWinner;
        }

        private Winner checkForThreeIdenticalValueInRows()
        {
            var rowIndex = new[,]
            {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8}
            };

            for (int i = 0; i < rowIndex.GetLength(0); i++)
            {
                int firstIndex = rowIndex[i, 0], secondIndex = rowIndex[i, 1], thirdIndex = rowIndex[i, 2];

                var winner = checkWinner(firstIndex, secondIndex, thirdIndex);
                if (winner != noWinner)
                    return winner;
            }

            return noWinner;
        }

        private Winner checkForThreeIdenticalValueDiagonally()
        {
            var rowIndex = new[,]
            {
                {0, 4, 8},
                {2, 4, 6}
            };

            for (int i = 0; i < rowIndex.GetLength(0); i++)
            {
                int firstIndex = rowIndex[i, 0], secondIndex = rowIndex[i, 1], thirdIndex = rowIndex[i, 2];

                var winner = checkWinner(firstIndex, secondIndex, thirdIndex);
                if (winner != noWinner)
                    return winner;
            }

            return noWinner;
        }

        private void changeCurrentPlayer()
        {
            _currentPlayer = _currentPlayer == Player.Human ? Player.Computer : Player.Human;
        }
    }
}
