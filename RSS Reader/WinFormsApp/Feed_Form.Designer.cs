namespace WinFormsApp
{
    partial class Feed_Form
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
            label6 = new Label();
            lblSource = new Label();
            lblParser = new Label();
            lblName = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            tbSource = new TextBox();
            tbName = new TextBox();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            cbParser = new ComboBox();
            cbItemParser = new ComboBox();
            lblItemParser = new Label();
            pbImage = new PictureBox();
            btnUpload = new Button();
            openFileDialog1 = new OpenFileDialog();
            btnRemove = new Button();
            label3 = new Label();
            lblImage = new Label();
            clbCategories = new CheckedListBox();
            lblCategories = new Label();
            label7 = new Label();
            ((System.ComponentModel.ISupportInitialize)pbImage).BeginInit();
            SuspendLayout();
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F);
            label6.Location = new Point(30, 128);
            label6.Name = "label6";
            label6.Size = new Size(48, 20);
            label6.TabIndex = 30;
            label6.Text = "Parser";
            // 
            // lblSource
            // 
            lblSource.AutoSize = true;
            lblSource.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblSource.ForeColor = Color.Firebrick;
            lblSource.Location = new Point(115, 58);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(36, 17);
            lblSource.TabIndex = 29;
            lblSource.Text = "Error";
            // 
            // lblParser
            // 
            lblParser.AutoSize = true;
            lblParser.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblParser.ForeColor = Color.Firebrick;
            lblParser.Location = new Point(116, 108);
            lblParser.Name = "lblParser";
            lblParser.Size = new Size(36, 17);
            lblParser.TabIndex = 28;
            lblParser.Text = "Error";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblName.ForeColor = Color.Firebrick;
            lblName.Location = new Point(114, 8);
            lblName.Name = "lblName";
            lblName.Size = new Size(36, 17);
            lblName.TabIndex = 27;
            lblName.Text = "Error";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(638, 381);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(102, 33);
            btnCancel.TabIndex = 26;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(528, 381);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(104, 33);
            btnSave.TabIndex = 25;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // tbSource
            // 
            tbSource.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSource.Location = new Point(115, 78);
            tbSource.Name = "tbSource";
            tbSource.Size = new Size(412, 27);
            tbSource.TabIndex = 22;
            // 
            // tbName
            // 
            tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbName.Location = new Point(114, 28);
            tbName.Name = "tbName";
            tbName.Size = new Size(412, 27);
            tbName.TabIndex = 21;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.Location = new Point(30, 179);
            label4.Name = "label4";
            label4.Size = new Size(82, 20);
            label4.TabIndex = 20;
            label4.Text = "Item Parser";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(28, 81);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 18;
            label2.Text = "Source";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(27, 31);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 17;
            label1.Text = "Name";
            // 
            // cbParser
            // 
            cbParser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbParser.DropDownStyle = ComboBoxStyle.DropDownList;
            cbParser.FormattingEnabled = true;
            cbParser.Location = new Point(117, 128);
            cbParser.Name = "cbParser";
            cbParser.Size = new Size(412, 28);
            cbParser.TabIndex = 33;
            // 
            // cbItemParser
            // 
            cbItemParser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbItemParser.DropDownStyle = ComboBoxStyle.DropDownList;
            cbItemParser.FormattingEnabled = true;
            cbItemParser.Location = new Point(117, 179);
            cbItemParser.Name = "cbItemParser";
            cbItemParser.Size = new Size(412, 28);
            cbItemParser.TabIndex = 34;
            // 
            // lblItemParser
            // 
            lblItemParser.AutoSize = true;
            lblItemParser.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblItemParser.ForeColor = Color.Firebrick;
            lblItemParser.Location = new Point(117, 159);
            lblItemParser.Name = "lblItemParser";
            lblItemParser.Size = new Size(36, 17);
            lblItemParser.TabIndex = 35;
            lblItemParser.Text = "Error";
            // 
            // pbImage
            // 
            pbImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pbImage.Location = new Point(564, 28);
            pbImage.Name = "pbImage";
            pbImage.Size = new Size(163, 144);
            pbImage.TabIndex = 36;
            pbImage.TabStop = false;
            // 
            // btnUpload
            // 
            btnUpload.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUpload.Location = new Point(564, 178);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(77, 31);
            btnUpload.TabIndex = 37;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnRemove
            // 
            btnRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRemove.Location = new Point(649, 178);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(78, 31);
            btnRemove.TabIndex = 38;
            btnRemove.Text = "Remove";
            btnRemove.UseVisualStyleBackColor = true;
            btnRemove.Click += btnRemove_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(564, 5);
            label3.Name = "label3";
            label3.Size = new Size(51, 20);
            label3.TabIndex = 39;
            label3.Text = "Image";
            // 
            // lblImage
            // 
            lblImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblImage.AutoSize = true;
            lblImage.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblImage.ForeColor = Color.Firebrick;
            lblImage.Location = new Point(621, 9);
            lblImage.Name = "lblImage";
            lblImage.Size = new Size(36, 17);
            lblImage.TabIndex = 40;
            lblImage.Text = "Error";
            // 
            // clbCategories
            // 
            clbCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            clbCategories.FormattingEnabled = true;
            clbCategories.Location = new Point(117, 234);
            clbCategories.Name = "clbCategories";
            clbCategories.Size = new Size(355, 158);
            clbCategories.TabIndex = 41;
            // 
            // lblCategories
            // 
            lblCategories.AutoSize = true;
            lblCategories.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblCategories.ForeColor = Color.Firebrick;
            lblCategories.Location = new Point(116, 214);
            lblCategories.Name = "lblCategories";
            lblCategories.Size = new Size(36, 17);
            lblCategories.TabIndex = 43;
            lblCategories.Text = "Error";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11F);
            label7.Location = new Point(29, 234);
            label7.Name = "label7";
            label7.Size = new Size(80, 20);
            label7.TabIndex = 42;
            label7.Text = "Categories";
            // 
            // Feed_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.Linen;
            ClientSize = new Size(748, 426);
            Controls.Add(lblCategories);
            Controls.Add(label7);
            Controls.Add(clbCategories);
            Controls.Add(lblImage);
            Controls.Add(label3);
            Controls.Add(btnRemove);
            Controls.Add(btnUpload);
            Controls.Add(pbImage);
            Controls.Add(lblItemParser);
            Controls.Add(cbItemParser);
            Controls.Add(cbParser);
            Controls.Add(label6);
            Controls.Add(lblSource);
            Controls.Add(lblParser);
            Controls.Add(lblName);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(tbSource);
            Controls.Add(tbName);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(764, 465);
            Name = "Feed_Form";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Feed";
            FormClosed += Feed_Form_FormClosed;
            ((System.ComponentModel.ISupportInitialize)pbImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPassword;
        private Label label6;
        private Label lblSource;
        private Label lblParser;
        private Label lblName;
        private Button btnCancel;
        private Button btnSave;
        private TextBox tbSource;
        private TextBox tbName;
        private Label label4;
        private Label label2;
        private Label label1;
        private ComboBox cbParser;
        private ComboBox cbItemParser;
        private Label lblItemParser;
        private PictureBox pbImage;
        private Button btnUpload;
        private OpenFileDialog openFileDialog1;
        private Button btnRemove;
        private Label label3;
        private Label lblImage;
        private CheckedListBox clbCategories;
        private Label lblCategories;
        private Label label7;
    }
}