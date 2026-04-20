using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_course
{
    public class Player
    {
        public List<Hand> Hands { get; private set; } = new List<Hand>();
        public int Chips { get; private set; }
        public bool AllowedPlay { get; set; }
        public int CurrentBet { get; private set; }

        public Player(int chips)
        {
            Chips = chips;
            AllowedPlay = true;
            Hands.Add(new Hand()); // every player starts with one hand
        }

        // basic strategy: hit everything below 17, stand on 17+
        public bool ShouldHit()
        {
            return Hands[0].Total < 17;
        }

        public void PlaceBet(int amount)
        {
            // math min keeps us from betting more chips than we have
            int bet = Math.Min(amount, Chips);
            CurrentBet = bet;
            Chips -= bet; // chips are held until round is over
        }

        // multiplier: 2.0 = win, 1.0 = push (get back), 0.0 = lose
        public void ResolveBet(float multiplier)
        {
            // (int) is an explicit cast — converts float to int, drops the decimal
            // have to change it only if I want to add complex rules like double down, split, insurance, etc. for now it's just a simple win/lose/push multiplier
            Chips += (int)(CurrentBet * multiplier);
            CurrentBet = 0;
        }

        public void Reset()
        {
            AllowedPlay = true;
            CurrentBet = 0;
            foreach (Hand h in Hands) h.Reset();
        }
    }
}