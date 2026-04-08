using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_course
{
    internal class Dealer
    {
        private Hand hand = new Hand();

        public Hand Hand => hand;

        // user calls this to hit the dealer hand
        public void Hit(Card card)
        {
            hand.Hit(card);
        }

        public void Stand()
        {
            hand.Stand();
        }

        // draws one card from the shoe and returns it (used during deal phase)
        public Card Deal(Shoe shoe)
        {
            Deck deck = shoe.Draw();
            if (deck == null) return null;
            return deck.Draw();
        }

        public void Reset()
        {
            hand.Reset();
        }
    }
}
