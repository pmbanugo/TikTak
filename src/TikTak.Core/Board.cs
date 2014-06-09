using System;
using System.Collections.Generic;
using System.IO;
using TikTak.Core.Interfaces;

namespace TikTak.Core
{
    public class Board : IBoard
    {
        private Dictionary<int, string> gameBoard;
        public Board()
        {
            gameBoard = new Dictionary<int, string>();
        }

        public void Clear() { gameBoard.Clear(); }

        public void Add(int key, string value)
        {
            if (gameBoard.ContainsKey(key))
                throw new ArgumentException("Board already contains this key.", "key");
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("value cannot be null or empty. You can either enter 'X' or 'Y'", "value");
            if (value != "O" && value != "X")
                throw new ArgumentException("You can only enter X or O as a value");

            gameBoard.Add(key, value);
        }

        public bool ContainsKey(int key)
        {
            return gameBoard.ContainsKey(key);
        }

        public Dictionary<int, string> Content 
        {
            get { return gameBoard; }
        }
    }
}
