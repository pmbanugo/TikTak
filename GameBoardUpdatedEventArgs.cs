using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TikTak
{
    public class GameBoardUpdatedEventArgs : EventArgs
    {
        private IBoard _board;
        public GameBoardUpdatedEventArgs(IBoard board)
        {
            _board = board;
        }

        public Dictionary<int, string> BoardContent { get { return _board.Content; } }
    }
}
