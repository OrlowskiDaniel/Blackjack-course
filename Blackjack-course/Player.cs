using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Blackjack_course
{
    public class Player
    {
        // fields
        
        public int Chips { get; private set; }
        public bool AllowedPlay { get; set; }

        // constructor
        public Player(int chips)
        {
            Chips = chips;
            
            AllowedPlay = true;
        }

        // add a card to the current hand
       
        // reset player for new round
        public void Reset()
        {
            Hands.Clear();
            AllowedPlay = true;
        }
    }
}