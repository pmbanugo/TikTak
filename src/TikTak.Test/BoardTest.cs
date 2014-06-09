using System;
using TikTak.Core;
using TikTak.Core.Interfaces;
using Xunit;

namespace TikTak.Test
{
    public class BoardTest
    {
        private IBoard board;

        public BoardTest()
        {
            board = new Board();
        }

        [Fact]
        public void Should_AddData_ToBoard()
        {
            //Arrange
            int key = 1;
            string value = "X";

            //Act
            board.Add(key, value);
            var result = board.ContainsKey(key);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Add_ExceptionThrown_IfKeyExist()
        {
            //Arrange
            int key = 1;
            string value = "X";
            board.Add(key, value);

            //Assert
            Assert.Throws<ArgumentException>(() => board.Add(key, value));
        }

        [Fact]
        public void Add_ExceptionThrown_IfValueIsEmpty()
        {
            //Arrange
            int key = 1;
            string value = string.Empty;

            //Assert
            Assert.Throws<ArgumentException>(() => board.Add(key, value));
        }

        [Fact]
        public void Add_ExceptionThrown_IfValueIsNull()
        {
            int key = 1;
            string value = null;

            Assert.Throws<ArgumentException>(() => board.Add(key, value));
        }

        [Fact]
        public void Add_ExceptionThrown_IfValueIs_Neither_X_Nor_O()
        {
            int key = 1;
            string value = "A";

            Assert.Throws<ArgumentException>(() => board.Add(key, value));
        }

        [Fact]
        public void ShouldClearBoardContent()
        {
            int key = 1;
            string value = "O";

            board.Add(key, value);
            board.Clear();

            Assert.False(board.ContainsKey(key));
        }
    }
}
