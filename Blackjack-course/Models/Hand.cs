using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_course
{
    public class Hand
    {
        private List<Card> cards = new List<Card>();
        private bool hasPassed = false; // true when player stood OR busted

        // CanPlay = still in the round (not stood, not bust)
        public bool CanPlay => !hasPassed && Total <= 21;
        public List<Card> Cards => cards;

        public int Total
        {
            get
            {
                int total = 0;
                int aces = 0;

                foreach (Card c in cards)
                {
                    if (c.Rank == Rank.Ace)
                    {
                        aces++;
                        total += 11; // start optimistic, reduce later if needed
                    }
                    else if (c.Rank >= Rank.Jack) // 11, 12, 13 (Jack, Queen, King)
                    {
                        total += 10; // all face cards worth 10 — standard blackjack rule
                    }
                    else
                    {
                        total += (int)c.Rank; // 2-10 are worth their rank value
                    }
                }

                // if we're over 21 and have aces, flip one ace from 11 → 1 (subtract 10)
                while (total > 21 && aces > 0)
                {
                    total -= 10;
                    aces--;
                }

                return total;
            }
        }

        public bool IsBlackjack => cards.Count == 2 && Total == 21; // natural blackjack
        public bool IsBust => Total > 21;

        // returns false if the hit caused a bust
        public bool Hit(Card card)
        {
            cards.Add(card);
            if (Total > 21)
            {
                hasPassed = true; // bust ends the hand automatically
                return false;
            }
            return true;
        }

        public void Stand()
        {
            hasPassed = true;
        }

        public void Reset()
        {
            cards.Clear();
            hasPassed = false;
        }
    }
}