//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TikTak.Core;
//using TikTak.Core.Interfaces;
//using Xunit;

//namespace TikTak.Test
//{
//    public class Old_BoardTest
//    {
//        private IBoard board;

//        public Old_BoardTest()
//        {
//            board = new Board();
//        }

//        [Fact]
//        public void Should_AddData_ToBoard()
//        {
//            //Arrange
//            int key = 1;
//            string value = "X";

//            //Act
//            board.Add(key, value);
//            var result = board.ContainsKey(key);

//            //Assert
//            Assert.True(result);
//        }
//        #region Thoughts on Testing for exception
//        //still comparing the best approach to asserting exception using A-A-A pattern
//        //but will work to make method short and readable even without following the A-A-A pattern
//        //Note that A-A-A pattern does not guarantee good test.
//        //aim to make them short and readable with a single focus of responsibility (SRP)
//        //Link to my question on SO: http://stackoverflow.com/q/24070115/2032333
//        #endregion

//        [Fact]
//        public void Add_ExceptionThrown_IfKeyExist()
//        {
//            //Arrange
//            int key = 1;
//            string value = "X";
//            board.Add(key, value);

//            //Act
//            var result = Assert.Throws<ArgumentException>(() => board.Add(key, value));

//            //Assert
//            Assert.IsType<ArgumentException>(result);
//        }

//        [Fact]
//        public void Add_ExceptionThrown_IfValueIsEmpty()
//        {
//            //Arrange
//            int key = 1;
//            string value = string.Empty;

//            //Act
//            Xunit.Assert.ThrowsDelegate addingSameKeySecondTime = () => board.Add(key, value);//TODO: (*thoughts*) this should be part of Arrange section because it's not directly calling the code under test, but marking a reference to the method under test. On a second thought, with this approach, it's seems like it's not necessary to have a delegate because the actual code invocation is done on the assert call and this performs the Act-Assert operation in one statement.

//            //Assert
//            Assert.Throws<ArgumentException>(addingSameKeySecondTime);
//        }

//        [Fact]
//        public void Add_ExceptionThrown_IfValueIsNull()
//        {
//            int key = 1;
//            string value = null;

//            Assert.Throws<ArgumentException>(() => board.Add(key, value));
//        }

//        [Fact]
//        public void Add_ExceptionThrown_IfValueIs_Neither_X_Nor_O()
//        {
//            int key = 1;
//            string value = "A";

//            Assert.Throws<ArgumentException>(() => board.Add(key, value));
//        }

//        [Fact]
//        public void ShouldClearBoardContent()
//        {
//            int key = 1;
//            string value = "O";

//            board.Add(key, value);
//            board.Clear();

//            Assert.False(board.ContainsKey(key));
//        }
//    }
//}
