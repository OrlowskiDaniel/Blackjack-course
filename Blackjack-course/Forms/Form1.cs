using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Blackjack_course
{
    public partial class Form1 : Form
    {
        private Table table;
        private int shoeSize = 8;
        private int playerCount = 3;
        private bool isSoloMode;
        private Panel modeSelectionPanel;

        public Form1()
        {
            InitializeComponent();
            nudPlayers.Minimum = 1;
            nudPlayers.Maximum = 3;
            nudPlayers.Value = 3;
            nudShoeSize.Minimum = 1;
            nudShoeSize.Maximum = 8;
            nudShoeSize.Value = 8;
            gamePanel.Visible = false;
            InitializeModeSelectionPanel();
        }

        private void InitializeModeSelectionPanel()
        {
            modeSelectionPanel = new Panel
            {
                Name = "modeSelectionPanel",
                BackColor = Color.FromArgb(230, 235, 242, 248),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(420, 270),
                Location = new Point((ClientSize.Width - 420) / 2, (ClientSize.Height - 270) / 2),
                Visible = false
            };

            var title = new Label
            {
                AutoSize = true,
                Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold),
                Location = new Point(72, 22),
                Text = "Choose a game mode"
            };
            var dealerModeButton = new Button
            {
                BackColor = Color.DarkTurquoise,
                Location = new Point(40, 78),
                Size = new Size(338, 52),
                Text = "Dealer Training Mode"
            };
            dealerModeButton.Click += (sender, e) => StartGame(false);

            var soloModeButton = new Button
            {
                BackColor = Color.PaleGreen,
                Location = new Point(40, 140),
                Size = new Size(338, 52),
                Text = "Solo Blackjack"
            };
            soloModeButton.Click += (sender, e) => StartGame(true);

            var backButton = new Button
            {
                Location = new Point(40, 210),
                Size = new Size(100, 36),
                Text = "Back"
            };
            backButton.Click += (sender, e) =>
            {
                modeSelectionPanel.Visible = false;
                menuPanel.Visible = true;
                gameLabel.Visible = true;
                menuPanel.BringToFront();
            };

            modeSelectionPanel.Controls.Add(title);
            modeSelectionPanel.Controls.Add(dealerModeButton);
            modeSelectionPanel.Controls.Add(soloModeButton);
            modeSelectionPanel.Controls.Add(backButton);
            Controls.Add(modeSelectionPanel);
            modeSelectionPanel.BringToFront();
        }

        private void StartGame(bool soloMode)
        {
            isSoloMode = soloMode;
            modeSelectionPanel.Visible = false;
            menuPanel.Visible = false;
            gameLabel.Visible = false;
            gamePanel.Visible = true;
            player2panel.Visible = !isSoloMode;
            player3panel.Visible = !isSoloMode;
            nudPlayers.Visible = !isSoloMode;
            label1.Visible = !isSoloMode;
            player1label.Text = isSoloMode ? "You" : "Player 1";
            dealerHitBtn.Text = isSoloMode ? "Hit" : "Dealer Hit";

            StartNewGame();
        }

        // -- menu buttons --------------------------------------

        private void playBtn_Click(object sender, EventArgs e)
        {
            menuPanel.Visible = false;
            gameLabel.Visible = false;
            modeSelectionPanel.Visible = true;
            modeSelectionPanel.BringToFront();
        }

        private void quitbtn_Click(object sender, EventArgs e) => Close();


        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            ShowHowToPlay();
        }

        private void ShowHowToPlay()
        {
            using (var infoWindow = new HowToPlayForm())
            {
                infoWindow.ShowDialog(this);
            }
        }

        // -- game setup -----------------------------------

        private void UpdateUI()
        {
            if (table == null) return;

            // update game state
            gameStateLabel.Text = $"Game Status: {table.State}";

            // update dealer info
            cardsValuesDealer.Text = $"Value: {table.Dealer.Hand.Total}";

            // update player 1
            if (table.Players.Count > 0)
            {
                var p1 = table.Players[0];
                cardsValuesPlayer1.Text = $"Value: {p1.Hands[0].Total}";
                betPlayer1.Text = $"Bet: ${p1.CurrentBet}";
                chipsPlayer1.Text = $"Chips: ${p1.Chips}";
            }

            // update player 2
            if (table.Players.Count > 1)
            {
                var p2 = table.Players[1];
                cardsValuesPlayer2.Text = $"Value: {p2.Hands[0].Total}";
                betPlayer2.Text = $"Bet: ${p2.CurrentBet}";
                chipsPlayer2.Text = $"Chips: ${p2.Chips}";
            }

            // update player 3
            if (table.Players.Count > 2)
            {
                var p3 = table.Players[2];
                cardsValuesPlayer3.Text = $"Value: {p3.Hands[0].Total}";
                betPlayer3.Text = $"Bet: ${p3.CurrentBet}";
                chipsPlayer3.Text = $"Chips: ${p3.Chips}";
            }
        }

        private void StartNewGame()
        {
            // take values from UI controls 
            shoeSize = (int)nudShoeSize.Value;
            playerCount = (int)nudPlayers.Value;

            // safety check: don t start a game with 0 players
            if (playerCount < 1) playerCount = 1;
            if (shoeSize < 1) shoeSize = 1;

            table = new Table(shoeSize, isSoloMode ? 1 : playerCount, isSoloMode);

            Log(isSoloMode
                ? $"Solo game started with a {shoeSize}-deck shoe. Place your bet and deal."
                : $"New game — {playerCount} players, {shoeSize}-deck shoe.");
            UpdateButtons();
            RefreshCardDisplay();
            UpdateUI();
        }

        // -- game button handlers ---------------------------------

        private void dealBtn_Click(object sender, EventArgs e)
        {
            // NextMove("deal") runs betting then moves to dealing
            string msg = table.NextMove("deal");
            Log(msg);

            // players act automatically right after dealing
            if (table.State == GameState.PlayerTurns)
                Log(table.NextMove(""));

            UpdateButtons();
            RefreshCardDisplay();
            UpdateUI();
        }

        private void dealerHitBtn_Click(object sender, EventArgs e)
        {
            Log(table.NextMove("hit"));

            // if hitting caused a bust, resolve immediately
            if (table.State == GameState.Resolving)
                Log(table.NextMove(""));

            UpdateButtons();
            RefreshCardDisplay();
            UpdateUI();
        }

        private void standBtn_Click(object sender, EventArgs e)
        {
            Log(table.NextMove("stand"));

            if (table.State == GameState.Resolving)
                Log(table.NextMove(""));

            UpdateButtons();
            RefreshCardDisplay();
            UpdateUI();
        }

        private void newRoundBtn_Click(object sender, EventArgs e)
        {
            table.NewRound();
            Log("--- New Round ---");
            ClearCardDisplay();
            UpdateButtons();
            UpdateUI();
        }

        private void howToPlaybtn2_Click(object sender, EventArgs e)
        {
            ShowHowToPlay();
        }

        // -- shoe/player count pickers -----------------------------------

        private void nudShoeSize_ValueChanged(object sender, EventArgs e)
        {
            // only apply if a game is already running
            if (table != null)
                shoeSize = (int)nudShoeSize.Value;
        }

        private void nudPlayers_ValueChanged(object sender, EventArgs e)
        {
            if (table != null)
                playerCount = (int)nudPlayers.Value;
        }

        // -- button enable/disable based on GameState -----------------------------------

        private void UpdateButtons()
        {
            bool isDealerTurn = table.State == GameState.DealerTurn;
            bool isSoloPlayerTurn = isSoloMode && table.State == GameState.PlayerTurns;
            bool isBetting = table.State == GameState.Betting;
            bool isDealing = table.State == GameState.Dealing;
            bool isDone = table.State == GameState.Done;

            dealBtn.Enabled = isBetting || isDealing;
            dealerHitBtn.Enabled = isDealerTurn || isSoloPlayerTurn;
            standBtn.Enabled = isDealerTurn || isSoloPlayerTurn;
            newRoundBtn.Enabled = isDone || isBetting;
        }

        // -- card display -----------------------------------
        // maps fixed designer PictureBoxes to the cards in the Table.
        // each player only shows their first 2 cards
        // TODO: add more PictureBoxes to extend this

        private void RefreshCardDisplay()
        {
            if (table == null) return;

            // dealer cards
            SetCard(dealerCard1, GetDealerCard(0));
            SetCard(dealerCard2, GetDealerCard(1));

            // player cards — indexed to PictureBox names
            SetCard(player1Card1, GetPlayerCard(0, 0));
            SetCard(player1Card2, GetPlayerCard(0, 1));
            SetCard(player2Card1, GetPlayerCard(1, 0));
            SetCard(player2Card2, GetPlayerCard(1, 1));
            SetCard(player3Card1, GetPlayerCard(2, 0));
            SetCard(player3Card2, GetPlayerCard(2, 1));
        }

        // puts a card image into a PictureBox, or clears it if card is null
        private void SetCard(PictureBox box, Card card)
        {
            if (card == null)
            {
                box.Image = null;
                box.Visible = false;
                return;
            }
            box.Image = card.CardImage;
            box.Visible = true;
            // SizeMode.Zoom keeps the card ratio inside the PictureBox
            box.SizeMode = PictureBoxSizeMode.Zoom;
        }

        // safey — return null if the card doesnt exist
        private Card GetDealerCard(int index)
        {
            var cards = table.Dealer.Hand.Cards;
            return index < cards.Count ? cards[index] : null;
        }

        private Card GetPlayerCard(int playerIndex, int cardIndex)
        {
            if (playerIndex >= table.Players.Count) return null;
            var cards = table.Players[playerIndex].Hands[0].Cards;
            return cardIndex < cards.Count ? cards[cardIndex] : null;
        }

        // hides all card PictureBoxes between rounds
        private void ClearCardDisplay()
        {
            var boxes = new[]
            {
                dealerCard1, dealerCard2,
                player1Card1, player1Card2,
                player2Card1, player2Card2,
                player3Card1, player3Card2
            };
            foreach (var box in boxes)
            {
                box.Image = null;
                box.Visible = false;
            }
        }

        // -- log box -----------------------------------------------

        private void Log(string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return;
            logBox.AppendText(message + "\n");
            logBox.ScrollToCaret();
        }


        // -- new game button -----------------------------------

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            StartNewGame();
            Log("Game Restarted.");
        }

        // --- unused ----------------------------------
        private void logBox_TextChanged(object sender, EventArgs e) { }
        private void dealerCard1_Click(object sender, EventArgs e) { }
        private void dealerCard2_Click(object sender, EventArgs e) { }
        private void player1Card1_Click(object sender, EventArgs e) { }
        private void player1Card2_Click(object sender, EventArgs e) { }
        private void player2Card1_Click(object sender, EventArgs e) { }
        private void player2Card2_Click(object sender, EventArgs e) { }
        private void player3Card1_Click(object sender, EventArgs e) { }
        private void player3Card2_Click(object sender, EventArgs e) { }
        private void gameStateLabel_Click(object sender, EventArgs e) { }
        private void betPlayer1_Click(object sender, EventArgs e) { }
        private void betPlayer2_Click(object sender, EventArgs e) { }
        private void betPlayer3_Click(object sender, EventArgs e) { }
        private void cardsValuesDealer_Click(object sender, EventArgs e) { }
        private void cardsValuesPlayer1_Click(object sender, EventArgs e) { }
        private void cardsValuesPlayer2_Click(object sender, EventArgs e) { }
        private void cardsValuesPlayer3_Click(object sender, EventArgs e) { }
        private void chipsPlayer1_Click(object sender, EventArgs e) { }
        private void chipsPlayer2_Click(object sender, EventArgs e) { }
        private void chipsPlayer3_Click(object sender, EventArgs e) { }
    }
}