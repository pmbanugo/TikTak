﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TikTak.Core;
using TikTak.Core.Interfaces;

namespace TikTak.UI.WinForms
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
            if (e.GameState.GameTied)
            {
                lblTied.Text = (Convert.ToInt32(lblTied.Text) + 1).ToString();
                MessageBox.Show("Game Tied", "Game Over");
            }
            if (e.GameState.Winner != null)
            {

                if (e.GameState.Winner.Player == Player.Human)
                {
                    HighlightWinningSquares(e.GameState.Winner.WinnerIndex);
                    lblHuman.Text = (Convert.ToInt32(lblHuman.Text) + 1).ToString();
                    MessageBox.Show("You Won", "Game Over");
                }
                if (e.GameState.Winner.Player == Player.Computer)
                {
                    HighlightWinningSquares(e.GameState.Winner.WinnerIndex);
                    lblComputer.Text = (Convert.ToInt32(lblComputer.Text) + 1).ToString();
                    MessageBox.Show("You Lose", "Game Over");
                }
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
