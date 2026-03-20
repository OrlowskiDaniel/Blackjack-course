using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Blackjack_course
{
    public class Shoe
    {
        private List<Deck> decks = new List<Deck>();
        // how many cards remain in the shoe
        public int CardsLeft => decks.Sum(d => d.CardsLeft);


        public Shoe(int numDecks) 
        {
            decks = new List<Deck>(); // reset shoe to be empty before adding decks

            for (int i = 0; i < numDecks; i++)
            {
                Console.WriteLine($"Adding deck {i + 1} to shoe.");
                decks.Add(new Deck());
                

                //Console.WriteLine($"Deck {i + 1} added. Total cards in shoe: {(i + 1) * cardsPerDeck}");
                
            }

        }

        public void Shuffle()
        {
            foreach (Deck d in decks)
            {
                d.Shuffle();
                Console.WriteLine("shuffling deck...");
            }

        }
        public Deck Draw()
        {
            foreach (Deck d in decks) {
                Console.WriteLine(
                    $"cards left {CardsLeft} ");
                if (d.CardsLeft > 0)
                {
                    return d;
                }
            }

            return null;
        }

    }
}