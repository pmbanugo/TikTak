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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            firstPlayer = PlayerType.PlayerX;
            currentPlayer = firstPlayer;
        }

        bool gameover = false;
        //Player currentPlayer = Player.X;
        PlayerType currentPlayer;
        PlayerType firstPlayer;
        PlayerType? winner = null;
        int[] winningIndex;
        Dictionary<int, string> GameBoard = new Dictionary<int, string>();

        void Process(int index, Button btn)
        {
            if (GameBoard.ContainsKey(index))
            {
                MessageBox.Show("You cannot play the same square twice.\n Please pick another square.", "Play another square");
                return;
            }

            string value = currentPlayer == PlayerType.PlayerX ? "X" : "O";
            btn.Text = value;
            currentPlayer = currentPlayer == PlayerType.PlayerX ? PlayerType.PlayerO : PlayerType.PlayerX;

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("value", "value cannot be null");
            }

            GameBoard.Add(index, value);

            if (GameBoard.Count > 3)
            {
                gameover = GameProcess.CheckAndProcessWinner(GameBoard, out winner,out winningIndex);
            }
            
            if (gameover)
            {
                if (winningIndex.Length > 2)
                {
                    HighlightWinningSquares();
                }
                DisplayGameState();
                ResetGame();

                firstPlayer = firstPlayer == PlayerType.PlayerX ? PlayerType.PlayerO : PlayerType.PlayerX;
                currentPlayer = firstPlayer;
            }
            if (currentPlayer == PlayerType.PlayerO)
            {
                PlayForComputer();
            }
        }

        private void HighlightWinningSquares()
        {
            var buttons = this.Controls.OfType<Button>().OrderBy(c => c.Name).ToArray();
            foreach (int index in winningIndex)
            {
                buttons[index].BackColor = Color.Red;
            }
        }

        private void PlayForComputer()
        {
            int indexToPlay = ComputerPlayer.MakeDecision(GameBoard);
            var buttons = this.Controls.OfType<Button>();
            var button = buttons.First(btn => btn.Name == "button" + indexToPlay);
            Process(indexToPlay, button);
        }

        private void DisplayGameState()
        {
            switch (winner)
            {
                case PlayerType.PlayerX:
                    {
                        MessageBox.Show("You win");
                        lblHuman.Text = (Convert.ToInt32(lblHuman.Text) + 1).ToString();
                        break;
                    }
                case PlayerType.PlayerO:
                    {
                        MessageBox.Show("You Lost");
                        lblComputer.Text = (Convert.ToInt32(lblComputer.Text) + 1).ToString();
                        break;
                    }
                default:
                    {
                        MessageBox.Show("It's a draw");
                        lblDraw.Text = (Convert.ToInt32(lblDraw.Text) + 1).ToString();
                        break;
                    }
            }
        }

        private void ResetGame()
        {
            GameBoard.Clear();
            var buttons = this.Controls.OfType<Button>();
            foreach (Button button in buttons)
            {
                button.Text = "";
                button.BackColor = SystemColors.Control;
            }
            currentPlayer = PlayerType.PlayerX;
            gameover = false;
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "button0":
                    {
                        Process(0, btn);
                        break;
                    }
                case "button1":
                    {
                        Process(1, btn);
                        break;
                    }
                case "button2":
                    {
                        Process(2, btn);
                        break;
                    }
                case "button3":
                    {
                        Process(3, btn);
                        break;
                    }
                case "button4":
                    {
                        Process(4, btn);
                        break;
                    }
                case "button5":
                    {
                        Process(5, btn);
                        break;
                    }
                case "button6":
                    {
                        Process(6, btn);
                        break;
                    }
                case "button7":
                    {
                        Process(7, btn);
                        break;
                    }
                case "button8":
                    {
                        Process(8, btn);
                        break;
                    }
            }
        }
    }
}
