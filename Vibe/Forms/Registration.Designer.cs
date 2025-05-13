namespace Vibe.Forms
{
    partial class Registration
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
            lblLogin = new Label();
            txtLogin = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnRegister = new Button();
            lblPerference = new Label();
            checkedGenres = new CheckedListBox();
            checkedArtists = new CheckedListBox();
            lblGenres = new Label();
            lblArtists = new Label();
            SuspendLayout();
            // 
            // lblLogin
            // 
            lblLogin.Anchor = AnchorStyles.None;
            lblLogin.AutoSize = true;
            lblLogin.BackColor = Color.Transparent;
            lblLogin.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblLogin.ForeColor = SystemColors.ButtonHighlight;
            lblLogin.Location = new Point(590, 90);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(65, 23);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "логин:";
            lblLogin.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtLogin
            // 
            txtLogin.Anchor = AnchorStyles.None;
            txtLogin.Font = new Font("Arial Rounded MT Bold", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtLogin.Location = new Point(485, 116);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(273, 31);
            txtLogin.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.None;
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPassword.ForeColor = SystemColors.ButtonHighlight;
            lblPassword.Location = new Point(590, 154);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(81, 23);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "пароль:";
            lblPassword.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            txtPassword.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtPassword.Location = new Point(485, 180);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(273, 30);
            txtPassword.TabIndex = 3;
            // 
            // btnRegister
            // 
            btnRegister.Anchor = AnchorStyles.None;
            btnRegister.BackColor = Color.Gold;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnRegister.Location = new Point(531, 568);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(200, 50);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Зарегистрироваться";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // lblPerference
            // 
            lblPerference.Anchor = AnchorStyles.None;
            lblPerference.AutoSize = true;
            lblPerference.BackColor = Color.Transparent;
            lblPerference.Font = new Font("Arial", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPerference.ForeColor = SystemColors.ButtonHighlight;
            lblPerference.Location = new Point(545, 225);
            lblPerference.Name = "lblPerference";
            lblPerference.Size = new Size(168, 19);
            lblPerference.TabIndex = 6;
            lblPerference.Text = "ваши предпочтения:";
            // 
            // checkedGenres
            // 
            checkedGenres.Anchor = AnchorStyles.None;
            checkedGenres.FormattingEnabled = true;
            checkedGenres.Location = new Point(726, 285);
            checkedGenres.Name = "checkedGenres";
            checkedGenres.Size = new Size(354, 246);
            checkedGenres.TabIndex = 7;
            // 
            // checkedArtists
            // 
            checkedArtists.Anchor = AnchorStyles.None;
            checkedArtists.FormattingEnabled = true;
            checkedArtists.Location = new Point(188, 285);
            checkedArtists.Name = "checkedArtists";
            checkedArtists.Size = new Size(354, 246);
            checkedArtists.TabIndex = 8;
            // 
            // lblGenres
            // 
            lblGenres.Anchor = AnchorStyles.None;
            lblGenres.AutoSize = true;
            lblGenres.BackColor = Color.Transparent;
            lblGenres.ForeColor = SystemColors.ButtonHighlight;
            lblGenres.Location = new Point(839, 247);
            lblGenres.Name = "lblGenres";
            lblGenres.Size = new Size(132, 20);
            lblGenres.TabIndex = 9;
            lblGenres.Text = "любимые жанры:";
            // 
            // lblArtists
            // 
            lblArtists.Anchor = AnchorStyles.None;
            lblArtists.AutoSize = true;
            lblArtists.BackColor = Color.Transparent;
            lblArtists.ForeColor = SystemColors.ButtonHighlight;
            lblArtists.Location = new Point(265, 247);
            lblArtists.Name = "lblArtists";
            lblArtists.Size = new Size(175, 20);
            lblArtists.TabIndex = 10;
            lblArtists.Text = "любимые исполнители:";
            // 
            // Registration
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            BackgroundImage = Properties.Resources.вход_фон;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1262, 673);
            Controls.Add(lblArtists);
            Controls.Add(lblGenres);
            Controls.Add(checkedArtists);
            Controls.Add(checkedGenres);
            Controls.Add(lblPerference);
            Controls.Add(btnRegister);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtLogin);
            Controls.Add(lblLogin);
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Registration";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Регистрация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLogin;
        private TextBox txtLogin;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnRegister;
        private Label lblPerference;
        private CheckedListBox checkedGenres;
        private CheckedListBox checkedArtists;
        private Label lblGenres;
        private Label lblArtists;
    }
}