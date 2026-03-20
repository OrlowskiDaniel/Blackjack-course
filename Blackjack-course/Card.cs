using System;
using System.Drawing;

namespace Blackjack_course
{
    public enum Suit { Spades, Hearts, Diamonds, Clubs }
    public enum Rank
    {
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6,
        Seven = 7, Eight = 8, Nine = 9, Ten = 10,
        Jack = 11, Queen = 12, King = 13, Ace = 14
    }

    public class Card
    {
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }
        public Image CardImage { get; private set; }

        public Card(Rank rank, Suit suit, Image image)
        {
            Rank = rank;
            Suit = suit;
            CardImage = image;
        }

        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}
