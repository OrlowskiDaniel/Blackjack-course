using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack_course
{
    public enum GameState
    {
        Betting,
        Dealing,     
        PlayerTurns, 
        DealerTurn,  
        Resolving,   
        Done         
    }

    public class Table
    {
        private Shoe shoe;
        private Dealer dealer;
        private List<Player> players;
        private int activePlayerIndex;

        // get properties so form1 can read the and draw the ui
        public GameState State { get; private set; }
        public Dealer Dealer => dealer;
        public List<Player> Players => players;
        public int ActivePlayerIndex => activePlayerIndex;

        private int DefaultBet = 50;

        public Table(int shoeSize, int amountOfPlayers)
        {
            shoe = new Shoe(shoeSize);
            shoe.Shuffle();
            dealer = new Dealer();
            players = new List<Player>();

            for (int i = 0; i < amountOfPlayers; i++)
                players.Add(new Player(500)); // 500$ for each player

            State = GameState.Betting;
        }

        // form1 calls this with hit, stand, deal...
        public string NextMove(string input)
        {
            switch (State)
            {
                case GameState.Betting: return StartBetting();
                case GameState.Dealing: return DealInitialCards();
                case GameState.PlayerTurns: return RunPlayerTurns();
                case GameState.DealerTurn:
                    if (input == "hit") return DealerHit();
                    if (input == "stand") return DealerStand();
                    return "Dealer turn: press hit or stand btn";
                case GameState.Resolving: return Resolve();
                case GameState.Done: return "Round over, press New Round";
            }
            return "";
        }

        private string StartBetting()
        {
            foreach (Player p in players)
            {
                p.Reset();
                p.PlaceBet(DefaultBet);
            }
            dealer.Reset();
            State = GameState.Dealing;
            return "bets placed, press Deal";
        }

        private string DealInitialCards()
        {
            // two steps: give every player 1 card, then repeat
            // make it how real dealers deal cards
            for (int round = 0; round < 2; round++)
            {
                foreach (Player p in players)
                    p.Hands[0].Hit(dealer.Deal(shoe));

                dealer.Hand.Hit(dealer.Deal(shoe));
            }

            activePlayerIndex = 0;
            State = GameState.PlayerTurns;
            return RunPlayerTurns();
        }

        private string RunPlayerTurns()
        {
            while (activePlayerIndex < players.Count)
            {
                Player p = players[activePlayerIndex];
                Hand h = p.Hands[0];

                if (!p.AllowedPlay || !h.CanPlay)
                {
                    activePlayerIndex++; // skip finished players 
                    continue;
                }

                if (p.ShouldHit())
                {
                    h.Hit(dealer.Deal(shoe));
                    // don't advance index same player might need to hit again
                }
                else
                {
                    h.Stand();
                    activePlayerIndex++;
                }
            }

            State = GameState.DealerTurn;
            return "players done, dealer turn choose hit or stand";
        }

        private string DealerHit()
        {
            Card card = dealer.Deal(shoe);
            dealer.Hit(card);

            if (dealer.Hand.IsBust)
            {
                State = GameState.Resolving;
                return $"Dealer bust at {dealer.Hand.Total}! Resolving...";
            }
            return $"Dealer hits total: {dealer.Hand.Total}";
        }

        private string DealerStand()
        {
            dealer.Stand();
            State = GameState.Resolving;
            return $"Dealer stands at {dealer.Hand.Total}. Resolving...";
        }

        private string Resolve()
        {
            int dealerTotal = dealer.Hand.Total;
            bool dealerBust = dealer.Hand.IsBust;
            string log = "";

            foreach (Player p in players)
            {
                Hand h = p.Hands[0];
                int playerTotal = h.Total;

                if (h.IsBust)
                {
                    p.ResolveBet(0);   // player bust = automatic loss
                    log += $"Player bust ({playerTotal}), loses. Chips: {p.Chips}\n";
                }
                else if (dealerBust || playerTotal > dealerTotal)
                {
                    p.ResolveBet(2);   // win: get back 2× (profit + original stake)
                    log += $"Player wins ({playerTotal} vs {dealerTotal}). Chips: {p.Chips}\n";
                }
                else if (playerTotal == dealerTotal)
                {
                    p.ResolveBet(1);   // push: get exact bet back, no profit
                    log += $"Push ({playerTotal}). Chips: {p.Chips}\n";
                }
                else
                {
                    p.ResolveBet(0);   // dealer wins: player loses their bet
                    log += $"Player loses ({playerTotal} vs {dealerTotal}). Chips: {p.Chips}\n";
                }
            }

            State = GameState.Done;
            return log.TrimEnd(); // removes whitespace
        }

        public void NewRound()
        {
            // reshuffle when the shoe runs low
            if (shoe.CardsLeft < 52)
            {
                shoe = new Shoe(shoe.NumDecks);
                shoe.Shuffle();
            }
            State = GameState.Betting;
        }
    }
}
