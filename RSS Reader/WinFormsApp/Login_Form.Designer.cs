namespace WinFormsApp
{
    partial class Login_Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtBxEmail = new TextBox();
            txtBxPassword = new TextBox();
            btnLogIn = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtBxEmail
            // 
            txtBxEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBxEmail.Location = new Point(231, 181);
            txtBxEmail.Margin = new Padding(3, 4, 3, 4);
            txtBxEmail.Name = "txtBxEmail";
            txtBxEmail.Size = new Size(350, 27);
            txtBxEmail.TabIndex = 0;
            txtBxEmail.Text = "TestUser@email.com";
            // 
            // txtBxPassword
            // 
            txtBxPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBxPassword.Location = new Point(231, 239);
            txtBxPassword.Margin = new Padding(3, 4, 3, 4);
            txtBxPassword.Name = "txtBxPassword";
            txtBxPassword.PasswordChar = '*';
            txtBxPassword.Size = new Size(350, 27);
            txtBxPassword.TabIndex = 1;
            txtBxPassword.Text = "password";
            // 
            // btnLogIn
            // 
            btnLogIn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogIn.Location = new Point(478, 289);
            btnLogIn.Margin = new Padding(3, 4, 3, 4);
            btnLogIn.Name = "btnLogIn";
            btnLogIn.Size = new Size(86, 31);
            btnLogIn.TabIndex = 2;
            btnLogIn.Text = "Login";
            btnLogIn.UseVisualStyleBackColor = true;
            btnLogIn.Click += btnLogIn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(149, 242);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 3;
            label1.Text = "Password";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(149, 184);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 4;
            label2.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 18.25F, FontStyle.Bold);
            label3.Location = new Point(320, 63);
            label3.Name = "label3";
            label3.Size = new Size(204, 70);
            label3.TabIndex = 5;
            label3.Text = "RSS Feed Viewer\r\nManager";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.BackColor = Color.Linen;
            lblUsername.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblUsername.ForeColor = Color.Firebrick;
            lblUsername.Location = new Point(231, 160);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(36, 17);
            lblUsername.TabIndex = 6;
            lblUsername.Text = "Error";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.BackColor = Color.Linen;
            lblPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblPassword.ForeColor = Color.Firebrick;
            lblPassword.Location = new Point(231, 218);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(36, 17);
            lblPassword.TabIndex = 7;
            lblPassword.Text = "Error";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.hamster_logo;
            pictureBox1.Location = new Point(176, 45);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(138, 112);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // Login_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(734, 391);
            Controls.Add(pictureBox1);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnLogIn);
            Controls.Add(txtBxPassword);
            Controls.Add(txtBxEmail);
            Font = new Font("Segoe UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(750, 430);
            Name = "Login_Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtBxEmail;
        private TextBox txtBxPassword;
        private Button btnLogIn;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblUsername;
        private Label lblPassword;
        private PictureBox pictureBox1;
    }
}
