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
            btn1 = new Button();
            lblPassword2 = new Label();
            txtPassword2 = new TextBox();
            btn2 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // lblLogin
            // 
            lblLogin.AutoSize = true;
            lblLogin.BackColor = Color.Transparent;
            lblLogin.ForeColor = SystemColors.ButtonHighlight;
            lblLogin.Location = new Point(606, 183);
            lblLogin.Name = "lblLogin";
            lblLogin.Size = new Size(53, 20);
            lblLogin.TabIndex = 0;
            lblLogin.Text = "логин:";
            // 
            // txtLogin
            // 
            txtLogin.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtLogin.Location = new Point(496, 206);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(282, 30);
            txtLogin.TabIndex = 1;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Transparent;
            lblPassword.ForeColor = SystemColors.ButtonHighlight;
            lblPassword.Location = new Point(606, 246);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(63, 20);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "пароль:";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.None;
            txtPassword.BackColor = SystemColors.ButtonHighlight;
            txtPassword.Location = new Point(496, 269);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(282, 27);
            txtPassword.TabIndex = 3;
            // 
            // btn1
            // 
            btn1.Anchor = AnchorStyles.None;
            btn1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn1.BackColor = Color.Transparent;
            btn1.BackgroundImage = Properties.Resources.view;
            btn1.BackgroundImageLayout = ImageLayout.Stretch;
            btn1.ForeColor = Color.Cornsilk;
            btn1.Location = new Point(749, 269);
            btn1.Name = "btn1";
            btn1.Size = new Size(29, 27);
            btn1.TabIndex = 4;
            btn1.TextAlign = ContentAlignment.MiddleRight;
            btn1.UseVisualStyleBackColor = false;
            btn1.Click += btn1_Click;
            // 
            // lblPassword2
            // 
            lblPassword2.AutoSize = true;
            lblPassword2.BackColor = Color.Transparent;
            lblPassword2.ForeColor = SystemColors.ButtonHighlight;
            lblPassword2.Location = new Point(567, 314);
            lblPassword2.Name = "lblPassword2";
            lblPassword2.Size = new Size(140, 20);
            lblPassword2.TabIndex = 5;
            lblPassword2.Text = "повторите пароль:";
            // 
            // txtPassword2
            // 
            txtPassword2.Location = new Point(496, 346);
            txtPassword2.Name = "txtPassword2";
            txtPassword2.Size = new Size(282, 27);
            txtPassword2.TabIndex = 6;
            // 
            // btn2
            // 
            btn2.Anchor = AnchorStyles.None;
            btn2.BackColor = Color.Transparent;
            btn2.BackgroundImage = Properties.Resources.hide;
            btn2.BackgroundImageLayout = ImageLayout.Stretch;
            btn2.Location = new Point(749, 346);
            btn2.Name = "btn2";
            btn2.Size = new Size(29, 27);
            btn2.TabIndex = 7;
            btn2.UseVisualStyleBackColor = false;
            btn2.Click += btn2_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Gold;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            button2.Location = new Point(555, 404);
            button2.Name = "button2";
            button2.Size = new Size(172, 45);
            button2.TabIndex = 8;
            button2.Text = "Продолжить";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // Registration
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.вход_фон;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1262, 673);
            Controls.Add(button2);
            Controls.Add(btn2);
            Controls.Add(txtPassword2);
            Controls.Add(lblPassword2);
            Controls.Add(btn1);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtLogin);
            Controls.Add(lblLogin);
            MaximizeBox = false;
            Name = "Registration";
            Text = "Регистрация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLogin;
        private TextBox txtLogin;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btn1;
        private Label lblPassword2;
        private TextBox txtPassword2;
        private Button btn2;
        private Button button2;
    }
}