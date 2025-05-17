namespace Vibe.Forms
{
    partial class SongForm
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
            pctrBox = new PictureBox();
            lblname = new Label();
            lblartist = new Label();
            btnnext = new Button();
            btnprevious = new Button();
            btnLike = new Button();
            btnDislike = new Button();
            btnfavoriteSong = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pctrBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pctrBox
            // 
            pctrBox.Anchor = AnchorStyles.None;
            pctrBox.Location = new Point(510, 151);
            pctrBox.Name = "pctrBox";
            pctrBox.Size = new Size(243, 219);
            pctrBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pctrBox.TabIndex = 0;
            pctrBox.TabStop = false;
            // 
            // lblname
            // 
            lblname.AutoSize = true;
            lblname.BackColor = Color.Transparent;
            lblname.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblname.ForeColor = SystemColors.ButtonHighlight;
            lblname.Location = new Point(549, 396);
            lblname.Name = "lblname";
            lblname.Size = new Size(162, 24);
            lblname.TabIndex = 1;
            lblname.Text = "Название трека";
            // 
            // lblartist
            // 
            lblartist.AutoSize = true;
            lblartist.BackColor = Color.Transparent;
            lblartist.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblartist.ForeColor = SystemColors.ButtonHighlight;
            lblartist.Location = new Point(579, 431);
            lblartist.Name = "lblartist";
            lblartist.Size = new Size(111, 19);
            lblartist.TabIndex = 2;
            lblartist.Text = "Исполнитель";
            // 
            // btnnext
            // 
            btnnext.BackColor = Color.Black;
            btnnext.BackgroundImage = Properties.Resources.istockphoto_1470459331_612x612__1_;
            btnnext.BackgroundImageLayout = ImageLayout.Stretch;
            btnnext.FlatStyle = FlatStyle.Flat;
            btnnext.ForeColor = SystemColors.ActiveCaptionText;
            btnnext.Location = new Point(656, 477);
            btnnext.Name = "btnnext";
            btnnext.Size = new Size(34, 30);
            btnnext.TabIndex = 3;
            btnnext.UseVisualStyleBackColor = false;
            btnnext.Click += btnnext_Click;
            // 
            // btnprevious
            // 
            btnprevious.BackColor = Color.Transparent;
            btnprevious.BackgroundImage = Properties.Resources.istockphoto_1470459331_612x612;
            btnprevious.BackgroundImageLayout = ImageLayout.Stretch;
            btnprevious.FlatStyle = FlatStyle.Flat;
            btnprevious.ForeColor = SystemColors.ActiveCaptionText;
            btnprevious.Location = new Point(579, 477);
            btnprevious.Name = "btnprevious";
            btnprevious.Size = new Size(32, 30);
            btnprevious.TabIndex = 4;
            btnprevious.UseVisualStyleBackColor = false;
            btnprevious.Click += btnprevious_Click;
            // 
            // btnLike
            // 
            btnLike.BackColor = Color.Gold;
            btnLike.FlatAppearance.BorderSize = 0;
            btnLike.FlatStyle = FlatStyle.Flat;
            btnLike.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnLike.Location = new Point(687, 513);
            btnLike.Name = "btnLike";
            btnLike.Size = new Size(167, 48);
            btnLike.TabIndex = 5;
            btnLike.Text = "нравится";
            btnLike.UseVisualStyleBackColor = false;
            btnLike.Click += btnLike_Click;
            // 
            // btnDislike
            // 
            btnDislike.BackColor = Color.Gold;
            btnDislike.FlatAppearance.BorderSize = 0;
            btnDislike.FlatStyle = FlatStyle.Flat;
            btnDislike.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnDislike.Location = new Point(414, 513);
            btnDislike.Name = "btnDislike";
            btnDislike.Size = new Size(167, 48);
            btnDislike.TabIndex = 6;
            btnDislike.Text = " не нравится";
            btnDislike.UseVisualStyleBackColor = false;
            btnDislike.Click += btnDislike_Click;
            // 
            // btnfavoriteSong
            // 
            btnfavoriteSong.BackColor = Color.Transparent;
            btnfavoriteSong.BackgroundImage = Properties.Resources.Frame_1;
            btnfavoriteSong.BackgroundImageLayout = ImageLayout.Stretch;
            btnfavoriteSong.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            btnfavoriteSong.FlatStyle = FlatStyle.Flat;
            btnfavoriteSong.ForeColor = SystemColors.ControlLightLight;
            btnfavoriteSong.Location = new Point(3, 12);
            btnfavoriteSong.Name = "btnfavoriteSong";
            btnfavoriteSong.Size = new Size(75, 68);
            btnfavoriteSong.TabIndex = 7;
            btnfavoriteSong.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnfavoriteSong.UseVisualStyleBackColor = false;
            btnfavoriteSong.Click += btnfavoriteSong_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.player_button;
            pictureBox1.Location = new Point(485, 461);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(288, 10);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // SongForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.песни_фон;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1262, 673);
            Controls.Add(pictureBox1);
            Controls.Add(btnfavoriteSong);
            Controls.Add(btnDislike);
            Controls.Add(btnLike);
            Controls.Add(btnprevious);
            Controls.Add(btnnext);
            Controls.Add(lblartist);
            Controls.Add(lblname);
            Controls.Add(pctrBox);
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "SongForm";
            ((System.ComponentModel.ISupportInitialize)pctrBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pctrBox;
        private Label lblname;
        private Label lblartist;
        private Button btnnext;
        private Button btnprevious;
        private Button btnLike;
        private Button btnDislike;
        private Button btnfavoriteSong;
        private PictureBox pictureBox1;
    }
}