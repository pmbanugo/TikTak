using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TikTak
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
                throw new Exception("Board already contains this key.");

            if (string.IsNullOrEmpty(value))
                throw new Exception("value cannot be null or empty");

            gameBoard.Add(key, value);
        }

        public bool ContainsKey(int key)
        {
            return gameBoard.ContainsKey(key);
        }

        public void Remove(int key) { gameBoard.Remove(key); }

        public Dictionary<int, string> Content 
        {
            get { return gameBoard; }
        }
    }
}
