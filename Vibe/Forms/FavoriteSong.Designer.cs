namespace Vibe.Forms
{
    partial class FavoriteSong
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
            panelMain = new Panel();
            labelTitle = new Label();
            buttonBack = new Button();
            panelTracks = new Panel();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.Gray;
            panelMain.Controls.Add(labelTitle);
            panelMain.Location = new Point(195, 40);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(851, 68);
            panelMain.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelTitle.ForeColor = Color.Gold;
            labelTitle.Location = new Point(339, 20);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(132, 24);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "ИЗБРАННОЕ";
            // 
            // buttonBack
            // 
            buttonBack.BackColor = Color.Transparent;
            buttonBack.BackgroundImage = Properties.Resources.Снимок_экрана_2025_05_17_035848;
            buttonBack.BackgroundImageLayout = ImageLayout.Stretch;
            buttonBack.FlatStyle = FlatStyle.Flat;
            buttonBack.Location = new Point(21, 21);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(53, 42);
            buttonBack.TabIndex = 2;
            buttonBack.UseVisualStyleBackColor = false;
            buttonBack.Click += buttonBack_Click;
            // 
            // panelTracks
            // 
            panelTracks.BackColor = SystemColors.ActiveCaptionText;
            panelTracks.Location = new Point(194, 108);
            panelTracks.Name = "panelTracks";
            panelTracks.Size = new Size(852, 446);
            panelTracks.TabIndex = 3;
            // 
            // FavoriteSong
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.песни_фон1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1262, 673);
            Controls.Add(panelTracks);
            Controls.Add(buttonBack);
            Controls.Add(panelMain);
            DoubleBuffered = true;
            MaximizeBox = false;
            Name = "FavoriteSong";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Избранное";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private Label labelTitle;
        private Button buttonBack;
        private Panel panelTracks;
    }
}