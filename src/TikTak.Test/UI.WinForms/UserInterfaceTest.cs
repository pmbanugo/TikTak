using System;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;
using Xunit;

namespace TikTak.Test.UI.WinForms
{
    public class UserInterfaceTest :IDisposable
    {
        private Application app;
        private Window window;

        public UserInterfaceTest()
        {
            app = Application.Launch("TikTak.UI.WinForms.exe");
            window = app.GetWindow("Tik Tak");
        }

        [Fact]
        public void Player__Should_BeNotifiedWhen_GameIsOver()
        {
            var btn0 = window.Get<Button>("button0");
            var btn6 = window.Get<Button>("button6");
            var btn8 = window.Get<Button>("button8");
            btn0.Click();
            btn6.Click();
            btn8.Click();
            var messageBox = window.MessageBox("Game Over");

            Assert.NotNull(messageBox);
        }

        [Fact]
        public void PlayerMove_Should_BeReflectedInTheInterface()
        {
            var btn = window.Get<Button>("button0");
            var expected = string.Empty;
            btn.Click();
            var actual = btn.Text;

            Assert.NotEqual<string>(expected, actual);
        }

        [Fact]
        public void Player_ShouldNot_MakeTheSameMoveTwice()
        {
            var btn = window.Get<Button>("button0");

            btn.Click();
            btn.Click();
            var messageBox = window.MessageBox("Play another square");

            Assert.NotNull(messageBox);
        }

        public void Dispose()
        {
            app.Kill();
        }
    }
}
