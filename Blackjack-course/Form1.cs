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
            gamePanel.Visible = false;
        }

        private void testingbtn_Click(object sender, EventArgs e)
        {
            Testing testingWindow = new Testing();
            testingWindow.ShowDialog();
            testingWindow.Dispose();
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            HowToPlayForm infoWindow = new HowToPlayForm();

            // show the window. 
            // using .ShowDialog() makes it a "popup"
            infoWindow.ShowDialog();

            // clean up memory after the window is closed
            infoWindow.Dispose();
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            menuPanel.Visible = false;
            gameLabel.Visible = false;
            gamePanel.Visible = true;

        }
        

        private void quitbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void testingbtn_Click(object sender, EventArgs e)
        {
            Testing testingWindow = new Testing();

            testingWindow.ShowDialog();

            testingWindow.Dispose();
        }

        private void player2_Click(object sender, EventArgs e)
        {

        }

        private void player1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
