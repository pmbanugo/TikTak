using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TikTak
{
    public partial class GameUI : Form
    {
        IGameService gameService;
        IController controller;

        public GameUI(IGameService gameservice, IController controller)
        {
            InitializeComponent();

            this.gameService = gameservice;
            this.controller = controller;
            SubscribeToGameEvents();
        }

        private void SubscribeToGameEvents()
        {
            gameService.GameBoardUpdated += gameService_GameBoardUpdated;
            gameService.GameCompleted += gameService_GameCompleted;
        }

        private void Reset()
        {
            gameService.ResetGame();

            var buttons = this.Controls.OfType<Button>();
            foreach (Button button in buttons)
            {
                button.Text = "";
                button.UseVisualStyleBackColor = true;
            }

            controller.RestartGame();
        }

        private void HighlightWinningSquares(int[] winnerIndex)
        {
            var buttons = this.Controls.OfType<Button>().OrderBy(c => c.Name).ToArray();
            foreach (int index in winnerIndex)
            {
                buttons[index].BackColor = Color.Red;
            }
        }

        private void PlayValue(int index)
        {
            if (!gameService.CanPlayValue(index))
            {
                MessageBox.Show("You cannot play the same square twice.\n Please pick another square.", "Play another square");
                return;
            }
            controller.MakeMoveForHumanPlayer(index);
        }

        private void gameService_GameCompleted(object sender, GameCompletedEventArgs e)
        {
            if (e.GameState.GameDrawn)
            {
                lblDraw.Text = (Convert.ToInt32(lblDraw.Text) + 1).ToString();
                MessageBox.Show("Game Drawn");
            }
            if (e.Winner == Player.Human)
            {
                HighlightWinningSquares(e.WinnerIndex);
                lblHuman.Text = (Convert.ToInt32(lblHuman.Text) + 1).ToString();
                MessageBox.Show("You Won");
            }
            if (e.Winner == Player.Computer)
            {
                HighlightWinningSquares(e.WinnerIndex);
                lblComputer.Text = (Convert.ToInt32(lblComputer.Text) + 1).ToString();
                MessageBox.Show("You Lose");
            }

            Reset();
        }

        private void gameService_GameBoardUpdated(object sender, GameBoardUpdatedEventArgs e)
        {
            var buttons = this.Controls.OfType<Button>().OrderBy(c => c.Name).ToArray();
            foreach (var item in e.BoardContent)
            {
                var button = buttons[item.Key];
                if (string.IsNullOrEmpty(button.Text))
                {
                    button.Text = item.Value;
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "button0":
                    {                        
                        PlayValue(0);
                        break;
                    }
                case "button1":
                    {
                        PlayValue(1);
                        break;
                    }
                case "button2":
                    {
                        PlayValue(2);
                        break;
                    }
                case "button3":
                    {
                        PlayValue(3);
                        break;
                    }
                case "button4":
                    {
                        PlayValue(4);
                        break;
                    }
                case "button5":
                    {
                        PlayValue(5);
                        break;
                    }
                case "button6":
                    {
                        PlayValue(6);
                        break;
                    }
                case "button7":
                    {
                        PlayValue(7);
                        break;
                    }
                case "button8":
                    {
                        PlayValue(8);
                        break;
                    }
            }
        }
    }
}
