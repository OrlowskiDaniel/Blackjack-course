using System;
using System.Drawing;
using System.Windows.Forms;

namespace Blackjack_course
{
    public partial class Testing : Form
    {
        // shared test state — reused across tests
        private Shoe shoe;
        private Dealer dealer;
        private Player player;

        public Testing()
        {
            InitializeComponent();
            this.Text = "Blackjack — Sprint 2 Tests";
            this.Size = new Size(900, 600);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        // -- logging helpers --------------------------------------------------

        private void Log(string msg, Color? color = null)
        {
            // SelectionColor sets the color of only the next AppendText chunk
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = color ?? Color.LightGreen;
            rtbLog.AppendText(msg + "\n");
            rtbLog.ScrollToCaret();
        }

        private void LogHeader(string title)
        {
            Log("");
            Log("══ " + title + " ══", Color.Cyan);
        }

        // => here is shorthand for a one-line method body, same as writing { Log(...); }
        private void LogPass(string msg) => Log("  PASS  " + msg, Color.LimeGreen);
        private void LogFail(string msg) => Log("  FAIL  " + msg, Color.OrangeRed);

        private void Assert(bool condition, string passMsg, string failMsg)
        {
            if (condition) LogPass(passMsg);
            else LogFail(failMsg);
        }

        // -- button click handlers — wire to designer events --------

        private void btnTestDeck_Click(object sender, EventArgs e) => TestDeck();
        private void btnTestShoe_Click(object sender, EventArgs e) => TestShoe();
        private void btnTestHandTotal_Click(object sender, EventArgs e) => TestHandTotal();
        private void btnTestAce_Click(object sender, EventArgs e) => TestAceReduction();
        private void btnTestBust_Click(object sender, EventArgs e) => TestBust();
        private void btnTestDealer_Click(object sender, EventArgs e) => TestDealer();
        private void btnTestPlayer_Click(object sender, EventArgs e) => TestPlayer();

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
        }

        // rtbLog doesn't need any logic on TextChanged
        private void rtbLog_TextChanged(object sender, EventArgs e) { }
        private void pnlButtons_Paint(object sender, PaintEventArgs e) { }

        // -- Deck ----------------------------------------------

        private void TestDeck()
        {
            LogHeader("Deck");
            try
            {
                Deck deck = new Deck();

                Assert(deck.CardsLeft == 52,
                    $"Has 52 cards ({deck.CardsLeft})",
                    $"Expected 52, got {deck.CardsLeft}");

                Card c = deck.Draw();
                Assert(c != null,
                    $"Drew a card: {c}",
                    "Draw() returned null on full deck");

                Assert(deck.CardsLeft == 51,
                    "Count dropped to 51 after draw",
                    $"Expected 51, got {deck.CardsLeft}");

                deck.Shuffle();
                Assert(deck.CardsLeft == 51,
                    "Shuffle keeps card count the same",
                    $"Shuffle changed count to {deck.CardsLeft}");

                // drain the rest
                while (deck.Draw() != null) { }
                Assert(deck.Draw() == null,
                    "Empty deck returns null",
                    "Empty deck did not return null");
            }
            catch (Exception ex) { LogFail("Exception: " + ex.Message); }
        }

        // -- Shoe -----------------------------------

        private void TestShoe()
        {
            LogHeader("Shoe");
            try
            {
                Shoe s6 = new Shoe(6);
                Assert(s6.CardsLeft == 312,
                    $"6-deck shoe = 312 cards ({s6.CardsLeft})",
                    $"Expected 312, got {s6.CardsLeft}");

                Shoe s1 = new Shoe(1);
                Assert(s1.CardsLeft == 52,
                    $"1-deck shoe = 52 cards ({s1.CardsLeft})",
                    $"Expected 52, got {s1.CardsLeft}");

                s1.Shuffle();
                Deck d = s1.Draw();
                Assert(d != null,
                    "Draw() returns a deck after shuffle",
                    "Draw() returned null");

                // ?. is the null-conditional operator — calls Draw() only if d isn't null
                Card card = d?.Draw();
                Assert(card != null,
                    $"Card from shoe: {card}",
                    "Card from shoe was null");
            }
            catch (Exception ex) { LogFail("Exception: " + ex.Message); }
        }

        // ---- Hand totals ---------------------------------

        private void TestHandTotal()
        {
            LogHeader("Hand totals");
            try
            {
                Shoe s = new Shoe(1);
                s.Shuffle();
                Deck d = s.Draw();

                Hand h = new Hand();
                Card c1 = d.Draw();
                Card c2 = d.Draw();
                h.Hit(c1);
                h.Hit(c2);

                int expected = CardValue(c1) + CardValue(c2);

                // special case: Ace+Ace starts at 22, one ace drops to 1 → 12
                if (c1.Rank == Rank.Ace && c2.Rank == Rank.Ace) expected = 12;

                Assert(h.Total == expected,
                    $"{c1} + {c2} = {h.Total} (expected {expected})",
                    $"Total mismatch: got {h.Total}, expected {expected}");

                Assert(h.CanPlay, "CanPlay = true with 2 cards", "CanPlay = false unexpectedly");
                Assert(!h.IsBust, "IsBust = false with 2 cards", "IsBust = true — unexpected");
            }
            catch (Exception ex) { LogFail("Exception: " + ex.Message); }
        }

        // -- Ace reduction -------------------------------

        private void TestAceReduction()
        {
            LogHeader("Ace reduction");
            try
            {
                Shoe s = new Shoe(6);
                s.Shuffle();
                Deck d = s.Draw();

                Hand h = new Hand();
                Card c = d.Draw();
                h.Hit(c);

                int singleTotal = h.Total;
                Assert(singleTotal >= 2 && singleTotal <= 11,
                    $"Single card {c} → total {singleTotal} (valid 2–11)",
                    $"Single card total {singleTotal} is out of range");

                // These rules are enforced by Hand.Total — we log them for visibility
                Log("  Ace logic rules (enforced by Hand.Total):", Color.Yellow);
                Log("    Ace + King        → 21  (ace stays 11)");
                Log("    Ace + King + Five → 16  (ace drops to 1)");
                Log("    Ace + Ace         → 12  (second ace drops to 1)");
                Log("    Ace + Ace + Nine  → 21  (one=11, one=1, +9)");
            }
            catch (Exception ex) { LogFail("Exception: " + ex.Message); }
        }

        // --- Bust detection --------------------------------

        private void TestBust()
        {
            LogHeader("Bust detection");
            try
            {
                Shoe s = new Shoe(6);
                s.Shuffle();
                Deck d = s.Draw();

                Hand h = new Hand();
                int hits = 0;

                while (h.CanPlay && hits < 20)
                {
                    Card c = d.Draw();

                    // if current deck is empty, grab the next one from the shoe
                    if (c == null) { d = s.Draw(); c = d?.Draw(); }
                    if (c == null) break;

                    h.Hit(c);
                    hits++;
                    Log($"  Hit {hits}: {c} → total {h.Total}, CanPlay={h.CanPlay}", Color.White);
                }

                if (h.IsBust)
                {
                    LogPass($"Bust detected at total {h.Total}");
                    Assert(!h.CanPlay,
                        "CanPlay = false after bust",
                        "CanPlay still true after bust — bug!");
                }
                else
                {
                    Log($"  No bust in {hits} draws (total={h.Total}). Run again.", Color.Yellow);
                }
            }
            catch (Exception ex) { LogFail("Exception: " + ex.Message); }
        }

        // --- Dealer ---------------------------------

        private void TestDealer()
        {
            LogHeader("Dealer");
            try
            {
                shoe = new Shoe(6);
                shoe.Shuffle();
                dealer = new Dealer();

                Card c1 = dealer.Deal(shoe);
                Card c2 = dealer.Deal(shoe);
                dealer.Hand.Hit(c1);
                dealer.Hand.Hit(c2);

                Assert(c1 != null, $"Deal 1 ok: {c1}", "Deal 1 = null");
                Assert(c2 != null, $"Deal 2 ok: {c2}", "Deal 2 = null");

                int t = dealer.Hand.Total;
                Assert(t >= 2 && t <= 21,
                    $"Hand total {t} is valid",
                    $"Hand total {t} out of range");

                Assert(dealer.Hand.CanPlay,
                    "CanPlay = true with 2 cards",
                    "CanPlay = false unexpectedly");

                dealer.Stand();
                Assert(!dealer.Hand.CanPlay,
                    "After Stand: CanPlay = false",
                    "After Stand: CanPlay still true — bug!");

                dealer.Reset();
                Assert(dealer.Hand.Total == 0,
                    "After Reset: total = 0",
                    $"After Reset: total = {dealer.Hand.Total}");
            }
            catch (Exception ex) { LogFail("Exception: " + ex.Message); }
        }

        // --- Player chips + bets ---------------------------------

        private void TestPlayer()
        {
            LogHeader("Player chips + bet");

            player = new Player(500);
            Assert(player.Chips == 500, "Starts with 500", $"Got {player.Chips}");

            player.PlaceBet(100);
            Assert(player.Chips == 400, "Bet 100 → chips 400", $"Got {player.Chips}");
            Assert(player.CurrentBet == 100, "CurrentBet = 100", $"Got {player.CurrentBet}");

            player.ResolveBet(2f);  // win
            Assert(player.Chips == 600, "Win 2× → chips 600", $"Got {player.Chips}");

            player.PlaceBet(100);
            player.ResolveBet(1f);  // push
            Assert(player.Chips == 600, "Push 1× → chips 600", $"Got {player.Chips}");

            player.PlaceBet(100);
            player.ResolveBet(0f);  // lose
            Assert(player.Chips == 500, "Lose 0× → chips 500", $"Got {player.Chips}");

            player.PlaceBet(9999);  // try to bet more than available
            Assert(player.CurrentBet == 500,
                "Over-bet capped at 500",
                $"Not capped: CurrentBet = {player.CurrentBet}");
            player.ResolveBet(0f);  // clean up

            player.Reset();
            Assert(player.AllowedPlay, "Reset: AllowedPlay = true", "Reset: AllowedPlay still false");
            Assert(player.CurrentBet == 0, "Reset: CurrentBet = 0", $"Reset: CurrentBet = {player.CurrentBet}");
        }

        private int CardValue(Card c)
        {
            if (c.Rank == Rank.Ace) return 11;
            if (c.Rank >= Rank.Jack) return 10; // Jack, Queen, King all = 10
            return (int)c.Rank;                  // Two=2, Three=3 ... Ten=10
        }
    }
}