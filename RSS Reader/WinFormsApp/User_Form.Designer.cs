namespace WinFormsApp
{
    partial class User_Form
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            tbName = new TextBox();
            tbEmail = new TextBox();
            tbUsername = new TextBox();
            rtbNotes = new RichTextBox();
            btnSave = new Button();
            btnClose = new Button();
            lblName = new Label();
            lblUsername = new Label();
            lblEmail = new Label();
            lblPassword = new Label();
            tbPassword = new TextBox();
            label6 = new Label();
            cbRole = new ComboBox();
            lblRole = new Label();
            label7 = new Label();
            lblCategories = new Label();
            label5 = new Label();
            clbCategories = new CheckedListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(29, 34);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(29, 84);
            label2.Name = "label2";
            label2.Size = new Size(46, 20);
            label2.TabIndex = 1;
            label2.Text = "Email";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(29, 134);
            label3.Name = "label3";
            label3.Size = new Size(75, 20);
            label3.TabIndex = 2;
            label3.Text = "Username";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(391, 280);
            label4.Name = "label4";
            label4.Size = new Size(48, 20);
            label4.TabIndex = 3;
            label4.Text = "Notes";
            // 
            // tbName
            // 
            tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbName.Location = new Point(125, 31);
            tbName.Name = "tbName";
            tbName.Size = new Size(622, 27);
            tbName.TabIndex = 4;
            // 
            // tbEmail
            // 
            tbEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbEmail.Location = new Point(125, 81);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(622, 27);
            tbEmail.TabIndex = 5;
            // 
            // tbUsername
            // 
            tbUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbUsername.Location = new Point(125, 131);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(622, 27);
            tbUsername.TabIndex = 7;
            // 
            // rtbNotes
            // 
            rtbNotes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbNotes.Location = new Point(445, 280);
            rtbNotes.Name = "rtbNotes";
            rtbNotes.Size = new Size(302, 145);
            rtbNotes.TabIndex = 8;
            rtbNotes.Text = "";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(522, 431);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(104, 33);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(645, 431);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(102, 33);
            btnClose.TabIndex = 10;
            btnClose.Text = "Cancel";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblName.ForeColor = Color.Firebrick;
            lblName.Location = new Point(125, 11);
            lblName.Name = "lblName";
            lblName.Size = new Size(36, 17);
            lblName.TabIndex = 11;
            lblName.Text = "Error";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblUsername.ForeColor = Color.Firebrick;
            lblUsername.Location = new Point(125, 111);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(36, 17);
            lblUsername.TabIndex = 12;
            lblUsername.Text = "Error";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblEmail.ForeColor = Color.Firebrick;
            lblEmail.Location = new Point(125, 61);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(36, 17);
            lblEmail.TabIndex = 13;
            lblEmail.Text = "Error";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblPassword.ForeColor = Color.Firebrick;
            lblPassword.Location = new Point(125, 161);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(36, 17);
            lblPassword.TabIndex = 16;
            lblPassword.Text = "Error";
            // 
            // tbPassword
            // 
            tbPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbPassword.Location = new Point(125, 181);
            tbPassword.Name = "tbPassword";
            tbPassword.Size = new Size(622, 27);
            tbPassword.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F);
            label6.Location = new Point(29, 184);
            label6.Name = "label6";
            label6.Size = new Size(70, 20);
            label6.TabIndex = 14;
            label6.Text = "Password";
            // 
            // cbRole
            // 
            cbRole.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRole.FormattingEnabled = true;
            cbRole.Location = new Point(125, 229);
            cbRole.Name = "cbRole";
            cbRole.Size = new Size(364, 28);
            cbRole.TabIndex = 17;
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblRole.ForeColor = Color.Firebrick;
            lblRole.Location = new Point(125, 209);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(36, 17);
            lblRole.TabIndex = 18;
            lblRole.Text = "Error";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11F);
            label7.Location = new Point(29, 232);
            label7.Name = "label7";
            label7.Size = new Size(45, 20);
            label7.TabIndex = 19;
            label7.Text = "Roles";
            // 
            // lblCategories
            // 
            lblCategories.AutoSize = true;
            lblCategories.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblCategories.ForeColor = Color.Firebrick;
            lblCategories.Location = new Point(125, 260);
            lblCategories.Name = "lblCategories";
            lblCategories.Size = new Size(36, 17);
            lblCategories.TabIndex = 46;
            lblCategories.Text = "Error";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.Location = new Point(29, 280);
            label5.Name = "label5";
            label5.Size = new Size(80, 20);
            label5.TabIndex = 45;
            label5.Text = "Categories";
            // 
            // clbCategories
            // 
            clbCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            clbCategories.FormattingEnabled = true;
            clbCategories.Location = new Point(126, 280);
            clbCategories.Name = "clbCategories";
            clbCategories.Size = new Size(259, 158);
            clbCategories.TabIndex = 44;
            // 
            // User_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(770, 469);
            Controls.Add(lblCategories);
            Controls.Add(label5);
            Controls.Add(clbCategories);
            Controls.Add(label7);
            Controls.Add(lblRole);
            Controls.Add(cbRole);
            Controls.Add(lblPassword);
            Controls.Add(tbPassword);
            Controls.Add(label6);
            Controls.Add(lblEmail);
            Controls.Add(lblUsername);
            Controls.Add(lblName);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(rtbNotes);
            Controls.Add(tbUsername);
            Controls.Add(tbEmail);
            Controls.Add(tbName);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(786, 508);
            Name = "User_Form";
            StartPosition = FormStartPosition.CenterParent;
            Text = "User";
            FormClosed += User_Form_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox tbName;
        private TextBox tbEmail;
        private TextBox tbUsername;
        private RichTextBox rtbNotes;
        private Button btnSave;
        private Button btnClose;
        private Label lblName;
        private Label lblUsername;
        private Label lblEmail;
        private Label lblPassword;
        private TextBox tbPassword;
        private Label label6;
        private ComboBox cbRole;
        private Label lblRole;
        private Label label7;
        private Label lblCategories;
        private Label label5;
        private CheckedListBox clbCategories;
    }
}