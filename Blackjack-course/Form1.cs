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
            
            HowToPlayForm infoWindow = new HowToPlayForm();

            // show the window
            // using .ShowDialog() makes it a "popup"
            infoWindow.ShowDialog();

            // clean up memory after the window is closed
            infoWindow.Dispose();
        }
    }
}
