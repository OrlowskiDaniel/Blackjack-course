using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack_course
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            // 1. Create an instance of your new window
            HowToPlayForm infoWindow = new HowToPlayForm();

            // 2. Show the window. 
            // Using .ShowDialog() makes it a "popup" that must be closed before returning to the game.
            infoWindow.ShowDialog();

            // 3. Clean up memory after the window is closed
            infoWindow.Dispose();
        }
    }
}
