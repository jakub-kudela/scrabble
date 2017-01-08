namespace ScrabbleClient
{
    partial class GameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
            this.boardGroupBox = new System.Windows.Forms.GroupBox();
            this.boardPictureBox = new System.Windows.Forms.PictureBox();
            this.playersGroupBox = new System.Windows.Forms.GroupBox();
            this.playersRichTextBox = new System.Windows.Forms.RichTextBox();
            this.logGroupBox = new System.Windows.Forms.GroupBox();
            this.logRichTextBox = new System.Windows.Forms.RichTextBox();
            this.handGroupBox = new System.Windows.Forms.GroupBox();
            this.handPictureBox = new System.Windows.Forms.PictureBox();
            this.moveButton = new System.Windows.Forms.Button();
            this.connectionGroupBox = new System.Windows.Forms.GroupBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.portLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.ipLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.boardGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.boardPictureBox)).BeginInit();
            this.playersGroupBox.SuspendLayout();
            this.logGroupBox.SuspendLayout();
            this.handGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.handPictureBox)).BeginInit();
            this.connectionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // boardGroupBox
            // 
            this.boardGroupBox.Controls.Add(this.boardPictureBox);
            this.boardGroupBox.Location = new System.Drawing.Point(13, 13);
            this.boardGroupBox.Name = "boardGroupBox";
            this.boardGroupBox.Size = new System.Drawing.Size(476, 490);
            this.boardGroupBox.TabIndex = 3;
            this.boardGroupBox.TabStop = false;
            this.boardGroupBox.Text = "Board:";
            // 
            // boardPictureBox
            // 
            this.boardPictureBox.Location = new System.Drawing.Point(6, 19);
            this.boardPictureBox.Name = "boardPictureBox";
            this.boardPictureBox.Size = new System.Drawing.Size(464, 464);
            this.boardPictureBox.TabIndex = 1;
            this.boardPictureBox.TabStop = false;
            this.boardPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.boardPictureBox_MouseClick);
            // 
            // playersGroupBox
            // 
            this.playersGroupBox.Controls.Add(this.playersRichTextBox);
            this.playersGroupBox.Location = new System.Drawing.Point(501, 148);
            this.playersGroupBox.Name = "playersGroupBox";
            this.playersGroupBox.Size = new System.Drawing.Size(222, 107);
            this.playersGroupBox.TabIndex = 4;
            this.playersGroupBox.TabStop = false;
            this.playersGroupBox.Text = "Players:";
            // 
            // playersRichTextBox
            // 
            this.playersRichTextBox.Location = new System.Drawing.Point(6, 20);
            this.playersRichTextBox.Name = "playersRichTextBox";
            this.playersRichTextBox.ReadOnly = true;
            this.playersRichTextBox.Size = new System.Drawing.Size(210, 81);
            this.playersRichTextBox.TabIndex = 0;
            this.playersRichTextBox.TabStop = false;
            this.playersRichTextBox.Text = "";
            // 
            // logGroupBox
            // 
            this.logGroupBox.Controls.Add(this.logRichTextBox);
            this.logGroupBox.Location = new System.Drawing.Point(501, 261);
            this.logGroupBox.Name = "logGroupBox";
            this.logGroupBox.Size = new System.Drawing.Size(222, 150);
            this.logGroupBox.TabIndex = 5;
            this.logGroupBox.TabStop = false;
            this.logGroupBox.Text = "Log:";
            // 
            // logRichTextBox
            // 
            this.logRichTextBox.Location = new System.Drawing.Point(6, 19);
            this.logRichTextBox.Name = "logRichTextBox";
            this.logRichTextBox.ReadOnly = true;
            this.logRichTextBox.Size = new System.Drawing.Size(210, 125);
            this.logRichTextBox.TabIndex = 0;
            this.logRichTextBox.TabStop = false;
            this.logRichTextBox.Text = "";
            // 
            // handGroupBox
            // 
            this.handGroupBox.Controls.Add(this.handPictureBox);
            this.handGroupBox.Controls.Add(this.moveButton);
            this.handGroupBox.Location = new System.Drawing.Point(501, 417);
            this.handGroupBox.Name = "handGroupBox";
            this.handGroupBox.Size = new System.Drawing.Size(228, 86);
            this.handGroupBox.TabIndex = 6;
            this.handGroupBox.TabStop = false;
            this.handGroupBox.Text = "Hand:";
            // 
            // handPictureBox
            // 
            this.handPictureBox.Location = new System.Drawing.Point(6, 19);
            this.handPictureBox.Name = "handPictureBox";
            this.handPictureBox.Size = new System.Drawing.Size(216, 30);
            this.handPictureBox.TabIndex = 2;
            this.handPictureBox.TabStop = false;
            this.handPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.handPictureBox_MouseClick);
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(6, 54);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(216, 25);
            this.moveButton.TabIndex = 7;
            this.moveButton.Text = "Perform move!";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // connectionGroupBox
            // 
            this.connectionGroupBox.Controls.Add(this.nameLabel);
            this.connectionGroupBox.Controls.Add(this.connectButton);
            this.connectionGroupBox.Controls.Add(this.portLabel);
            this.connectionGroupBox.Controls.Add(this.nameTextBox);
            this.connectionGroupBox.Controls.Add(this.ipLabel);
            this.connectionGroupBox.Controls.Add(this.portTextBox);
            this.connectionGroupBox.Controls.Add(this.ipTextBox);
            this.connectionGroupBox.Location = new System.Drawing.Point(501, 13);
            this.connectionGroupBox.Name = "connectionGroupBox";
            this.connectionGroupBox.Size = new System.Drawing.Size(222, 129);
            this.connectionGroupBox.TabIndex = 9;
            this.connectionGroupBox.TabStop = false;
            this.connectionGroupBox.Text = "Connection:";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(6, 74);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(38, 13);
            this.nameLabel.TabIndex = 4;
            this.nameLabel.Text = "Name:";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(9, 97);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(207, 25);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Connect!";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(6, 48);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 3;
            this.portLabel.Text = "Port:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(50, 71);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(166, 20);
            this.nameTextBox.TabIndex = 5;
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(6, 22);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(20, 13);
            this.ipLabel.TabIndex = 2;
            this.ipLabel.Text = "IP:";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(50, 45);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(166, 20);
            this.portTextBox.TabIndex = 1;
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(50, 19);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(166, 20);
            this.ipTextBox.TabIndex = 0;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 518);
            this.Controls.Add(this.connectionGroupBox);
            this.Controls.Add(this.handGroupBox);
            this.Controls.Add(this.logGroupBox);
            this.Controls.Add(this.playersGroupBox);
            this.Controls.Add(this.boardGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Scrabble Client";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameForm_Paint);
            this.boardGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.boardPictureBox)).EndInit();
            this.playersGroupBox.ResumeLayout(false);
            this.logGroupBox.ResumeLayout(false);
            this.handGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.handPictureBox)).EndInit();
            this.connectionGroupBox.ResumeLayout(false);
            this.connectionGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox logRichTextBox;
        private System.Windows.Forms.PictureBox boardPictureBox;
        private System.Windows.Forms.PictureBox handPictureBox;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.RichTextBox playersRichTextBox;
        private System.Windows.Forms.GroupBox boardGroupBox;
        private System.Windows.Forms.GroupBox playersGroupBox;
        private System.Windows.Forms.GroupBox logGroupBox;
        private System.Windows.Forms.GroupBox handGroupBox;
        private System.Windows.Forms.GroupBox connectionGroupBox;
    }
}