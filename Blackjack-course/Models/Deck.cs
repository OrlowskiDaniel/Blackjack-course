using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Blackjack_course
{
    public class Deck
    {
        private List<Card> cards = new List<Card>();

        // how many cards remain
        public int CardsLeft => cards.Count;

        public Deck()
        {
            FillDeck();
        }

        private void FillDeck()
        {
            cards.Clear(); // fresh sart

            int numberedCard = 9; // 2-10
            int faceCards = 4; // jack, queen, king, ace


            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                // Later changes:
                // - load images from a folder instead of hardcoding the path
                // - make it one loop
                for (int i = 0; i < numberedCard; i++)
                {
                    Rank rank = (Rank)(i + 2); // start from two to get correct numbered cards ranks
                    Image cardImage = Image.FromFile($"Resources/cards/{rank}_of_{suit}.png");
                    cards.Add(new Card(rank, suit, cardImage));
                }
                for (int j = 0; j < faceCards; j++)
                {
                    Rank rank = (Rank)(j + 11); // start from 11 to get the correct fece cards ranks
                    Image cardImage = Image.FromFile($"Resources/cards/{rank}_of_{suit}.png");
                    cards.Add(new Card(rank, suit, cardImage));
                }
            }

        }

        public void Shuffle()
        {
            Random rng = new Random();
            cards = cards.OrderBy(c => rng.Next()).ToList();
        }

        public Card Draw()
        {
            if (cards.Count == 0) return null;

            Card cardToReturn = cards[0];
            cards.RemoveAt(0);
            return cardToReturn;
        }
    }
}