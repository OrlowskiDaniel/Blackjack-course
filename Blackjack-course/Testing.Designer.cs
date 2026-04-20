namespace Blackjack_course
{
    partial class Testing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnTestPlayer = new System.Windows.Forms.Button();
            this.btnTestDealer = new System.Windows.Forms.Button();
            this.btnTestBust = new System.Windows.Forms.Button();
            this.btnTestAce = new System.Windows.Forms.Button();
            this.btnTestHandTotal = new System.Windows.Forms.Button();
            this.btnTestShoe = new System.Windows.Forms.Button();
            this.btnTestDeck = new System.Windows.Forms.Button();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnClear);
            this.pnlButtons.Controls.Add(this.btnTestPlayer);
            this.pnlButtons.Controls.Add(this.btnTestDealer);
            this.pnlButtons.Controls.Add(this.btnTestBust);
            this.pnlButtons.Controls.Add(this.btnTestAce);
            this.pnlButtons.Controls.Add(this.btnTestHandTotal);
            this.pnlButtons.Controls.Add(this.btnTestShoe);
            this.pnlButtons.Controls.Add(this.btnTestDeck);
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(226, 678);
            this.pnlButtons.TabIndex = 0;
            this.pnlButtons.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlButtons_Paint);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Red;
            this.btnClear.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClear.Location = new System.Drawing.Point(3, 334);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(220, 40);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear Logs";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnTestPlayer
            // 
            this.btnTestPlayer.Location = new System.Drawing.Point(6, 288);
            this.btnTestPlayer.Name = "btnTestPlayer";
            this.btnTestPlayer.Size = new System.Drawing.Size(220, 40);
            this.btnTestPlayer.TabIndex = 6;
            this.btnTestPlayer.Text = "Test Player";
            this.btnTestPlayer.UseVisualStyleBackColor = true;
            this.btnTestPlayer.Click += new System.EventHandler(this.btnTestPlayer_Click);
            // 
            // btnTestDealer
            // 
            this.btnTestDealer.Location = new System.Drawing.Point(6, 242);
            this.btnTestDealer.Name = "btnTestDealer";
            this.btnTestDealer.Size = new System.Drawing.Size(220, 40);
            this.btnTestDealer.TabIndex = 5;
            this.btnTestDealer.Text = "Test Dealer";
            this.btnTestDealer.UseVisualStyleBackColor = true;
            this.btnTestDealer.Click += new System.EventHandler(this.btnTestDealer_Click);
            // 
            // btnTestBust
            // 
            this.btnTestBust.Location = new System.Drawing.Point(3, 196);
            this.btnTestBust.Name = "btnTestBust";
            this.btnTestBust.Size = new System.Drawing.Size(220, 40);
            this.btnTestBust.TabIndex = 4;
            this.btnTestBust.Text = "Test Bust";
            this.btnTestBust.UseVisualStyleBackColor = true;
            this.btnTestBust.Click += new System.EventHandler(this.btnTestBust_Click);
            // 
            // btnTestAce
            // 
            this.btnTestAce.Location = new System.Drawing.Point(3, 150);
            this.btnTestAce.Name = "btnTestAce";
            this.btnTestAce.Size = new System.Drawing.Size(220, 40);
            this.btnTestAce.TabIndex = 3;
            this.btnTestAce.Text = "Test Ace reduction";
            this.btnTestAce.UseVisualStyleBackColor = true;
            this.btnTestAce.Click += new System.EventHandler(this.btnTestAce_Click);
            // 
            // btnTestHandTotal
            // 
            this.btnTestHandTotal.Location = new System.Drawing.Point(6, 104);
            this.btnTestHandTotal.Name = "btnTestHandTotal";
            this.btnTestHandTotal.Size = new System.Drawing.Size(220, 40);
            this.btnTestHandTotal.TabIndex = 2;
            this.btnTestHandTotal.Text = "Test Hand Total";
            this.btnTestHandTotal.UseVisualStyleBackColor = true;
            this.btnTestHandTotal.Click += new System.EventHandler(this.btnTestHandTotal_Click);
            // 
            // btnTestShoe
            // 
            this.btnTestShoe.Location = new System.Drawing.Point(3, 58);
            this.btnTestShoe.Name = "btnTestShoe";
            this.btnTestShoe.Size = new System.Drawing.Size(220, 40);
            this.btnTestShoe.TabIndex = 1;
            this.btnTestShoe.Text = "Test Shoe";
            this.btnTestShoe.UseVisualStyleBackColor = true;
            this.btnTestShoe.Click += new System.EventHandler(this.btnTestShoe_Click);
            // 
            // btnTestDeck
            // 
            this.btnTestDeck.Location = new System.Drawing.Point(3, 12);
            this.btnTestDeck.Name = "btnTestDeck";
            this.btnTestDeck.Size = new System.Drawing.Size(220, 40);
            this.btnTestDeck.TabIndex = 0;
            this.btnTestDeck.Text = "Test Deck";
            this.btnTestDeck.UseVisualStyleBackColor = true;
            this.btnTestDeck.Click += new System.EventHandler(this.btnTestDeck_Click);
            // 
            // rtbLog
            // 
            this.rtbLog.BackColor = System.Drawing.SystemColors.Desktop;
            this.rtbLog.Location = new System.Drawing.Point(232, 12);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.Size = new System.Drawing.Size(935, 666);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.Text = "";
            this.rtbLog.TextChanged += new System.EventHandler(this.rtbLog_TextChanged);
            // 
            // Testing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 690);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.pnlButtons);
            this.Name = "Testing";
            this.Text = "Testing";
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnTestDeck;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnTestPlayer;
        private System.Windows.Forms.Button btnTestDealer;
        private System.Windows.Forms.Button btnTestBust;
        private System.Windows.Forms.Button btnTestAce;
        private System.Windows.Forms.Button btnTestHandTotal;
        private System.Windows.Forms.Button btnTestShoe;
    }
}