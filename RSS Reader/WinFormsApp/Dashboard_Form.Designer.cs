using Data.DTOs;

namespace WinFormsApp
{
    partial class Dashboard_Form
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
            btnLogOut = new Button();
            pnlFeeds = new Panel();
            btnFeedsUpdate = new Button();
            btnFeedsAdd = new Button();
            btnFeedsRemove = new Button();
            lblFeedErrors = new Label();
            dgvFeeds = new DataGridView();
            cbHomeSearch = new ComboBox();
            tbHomeSearch = new TextBox();
            btnHomeSearch = new Button();
            pnlUsers = new Panel();
            btnUsersUpdate = new Button();
            lblUserErrors = new Label();
            btnUsersAdd = new Button();
            btnUsersRemove = new Button();
            dgvUsers = new DataGridView();
            cbUsersSearch = new ComboBox();
            tbUsersSearch = new TextBox();
            btnUsersSearch = new Button();
            pnlButtons = new Panel();
            btnOthers = new Button();
            btnStatistics = new Button();
            btnUsers = new Button();
            btnFeeds = new Button();
            lblHeader = new Label();
            pbLogo = new PictureBox();
            pnlStatistics = new Panel();
            pnlOthers = new Panel();
            lblRolesError = new Label();
            lblCategoriesError = new Label();
            btnOtherRolesRemove = new Button();
            label2 = new Label();
            tbOtherRoles = new TextBox();
            btnOtherRolesAdd = new Button();
            lbOtherRoles = new ListBox();
            btnOtherCategoriesRemove = new Button();
            label1 = new Label();
            tbOtherCategories = new TextBox();
            btnOtherCategoriesAdd = new Button();
            lbOtherCategories = new ListBox();
            pnlFeeds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFeeds).BeginInit();
            pnlUsers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbLogo).BeginInit();
            pnlOthers.SuspendLayout();
            SuspendLayout();
            // 
            // btnLogOut
            // 
            btnLogOut.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLogOut.Location = new Point(12, 518);
            btnLogOut.Margin = new Padding(3, 4, 3, 4);
            btnLogOut.Name = "btnLogOut";
            btnLogOut.Size = new Size(94, 32);
            btnLogOut.TabIndex = 1;
            btnLogOut.Text = "Log Out";
            btnLogOut.UseVisualStyleBackColor = true;
            btnLogOut.Click += btnLogOut_Click;
            // 
            // pnlFeeds
            // 
            pnlFeeds.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlFeeds.Controls.Add(btnFeedsUpdate);
            pnlFeeds.Controls.Add(btnFeedsAdd);
            pnlFeeds.Controls.Add(btnFeedsRemove);
            pnlFeeds.Controls.Add(lblFeedErrors);
            pnlFeeds.Controls.Add(dgvFeeds);
            pnlFeeds.Controls.Add(cbHomeSearch);
            pnlFeeds.Controls.Add(tbHomeSearch);
            pnlFeeds.Controls.Add(btnHomeSearch);
            pnlFeeds.Location = new Point(165, 41);
            pnlFeeds.Name = "pnlFeeds";
            pnlFeeds.Size = new Size(711, 509);
            pnlFeeds.TabIndex = 1;
            // 
            // btnFeedsUpdate
            // 
            btnFeedsUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFeedsUpdate.Location = new Point(121, 476);
            btnFeedsUpdate.Name = "btnFeedsUpdate";
            btnFeedsUpdate.Size = new Size(105, 27);
            btnFeedsUpdate.TabIndex = 14;
            btnFeedsUpdate.Text = "Update";
            btnFeedsUpdate.UseVisualStyleBackColor = true;
            btnFeedsUpdate.Click += btnFeedsUpdate_Click;
            // 
            // btnFeedsAdd
            // 
            btnFeedsAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFeedsAdd.Location = new Point(18, 476);
            btnFeedsAdd.Name = "btnFeedsAdd";
            btnFeedsAdd.Size = new Size(97, 27);
            btnFeedsAdd.TabIndex = 13;
            btnFeedsAdd.Text = "Add";
            btnFeedsAdd.UseVisualStyleBackColor = true;
            btnFeedsAdd.Click += btnFeedsAdd_Click;
            // 
            // btnFeedsRemove
            // 
            btnFeedsRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFeedsRemove.Location = new Point(232, 476);
            btnFeedsRemove.Name = "btnFeedsRemove";
            btnFeedsRemove.Size = new Size(105, 27);
            btnFeedsRemove.TabIndex = 12;
            btnFeedsRemove.Text = "Remove";
            btnFeedsRemove.UseVisualStyleBackColor = true;
            btnFeedsRemove.Click += btnFeedsRemove_Click;
            // 
            // lblFeedErrors
            // 
            lblFeedErrors.AutoSize = true;
            lblFeedErrors.BackColor = Color.Linen;
            lblFeedErrors.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblFeedErrors.ForeColor = Color.Firebrick;
            lblFeedErrors.Location = new Point(18, 49);
            lblFeedErrors.Name = "lblFeedErrors";
            lblFeedErrors.Size = new Size(70, 17);
            lblFeedErrors.TabIndex = 8;
            lblFeedErrors.Text = "Feed Errors";
            // 
            // dgvFeeds
            // 
            dgvFeeds.AllowUserToAddRows = false;
            dgvFeeds.AllowUserToDeleteRows = false;
            dgvFeeds.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvFeeds.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFeeds.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFeeds.Location = new Point(18, 77);
            dgvFeeds.MultiSelect = false;
            dgvFeeds.Name = "dgvFeeds";
            dgvFeeds.ReadOnly = true;
            dgvFeeds.Size = new Size(673, 393);
            dgvFeeds.TabIndex = 7;
            // 
            // cbHomeSearch
            // 
            cbHomeSearch.DropDownStyle = ComboBoxStyle.DropDownList;
            cbHomeSearch.FormattingEnabled = true;
            cbHomeSearch.Location = new Point(18, 15);
            cbHomeSearch.Name = "cbHomeSearch";
            cbHomeSearch.Size = new Size(121, 28);
            cbHomeSearch.TabIndex = 6;
            // 
            // tbHomeSearch
            // 
            tbHomeSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbHomeSearch.Location = new Point(145, 16);
            tbHomeSearch.Name = "tbHomeSearch";
            tbHomeSearch.Size = new Size(426, 27);
            tbHomeSearch.TabIndex = 5;
            // 
            // btnHomeSearch
            // 
            btnHomeSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHomeSearch.Location = new Point(577, 16);
            btnHomeSearch.Name = "btnHomeSearch";
            btnHomeSearch.Size = new Size(100, 27);
            btnHomeSearch.TabIndex = 4;
            btnHomeSearch.Text = "Search";
            btnHomeSearch.UseVisualStyleBackColor = true;
            btnHomeSearch.Click += btnFeedsSearch_Click;
            // 
            // pnlUsers
            // 
            pnlUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlUsers.Controls.Add(btnUsersUpdate);
            pnlUsers.Controls.Add(lblUserErrors);
            pnlUsers.Controls.Add(btnUsersAdd);
            pnlUsers.Controls.Add(btnUsersRemove);
            pnlUsers.Controls.Add(dgvUsers);
            pnlUsers.Controls.Add(cbUsersSearch);
            pnlUsers.Controls.Add(tbUsersSearch);
            pnlUsers.Controls.Add(btnUsersSearch);
            pnlUsers.Location = new Point(165, 41);
            pnlUsers.Name = "pnlUsers";
            pnlUsers.Size = new Size(711, 509);
            pnlUsers.TabIndex = 2;
            // 
            // btnUsersUpdate
            // 
            btnUsersUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUsersUpdate.Location = new Point(121, 476);
            btnUsersUpdate.Name = "btnUsersUpdate";
            btnUsersUpdate.Size = new Size(105, 27);
            btnUsersUpdate.TabIndex = 12;
            btnUsersUpdate.Text = "Update";
            btnUsersUpdate.UseVisualStyleBackColor = true;
            btnUsersUpdate.Click += btnUsersUpdate_Click;
            // 
            // lblUserErrors
            // 
            lblUserErrors.AutoSize = true;
            lblUserErrors.BackColor = Color.Linen;
            lblUserErrors.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblUserErrors.ForeColor = Color.Firebrick;
            lblUserErrors.Location = new Point(18, 49);
            lblUserErrors.Name = "lblUserErrors";
            lblUserErrors.Size = new Size(70, 17);
            lblUserErrors.TabIndex = 9;
            lblUserErrors.Text = "User Errors";
            // 
            // btnUsersAdd
            // 
            btnUsersAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUsersAdd.Location = new Point(18, 476);
            btnUsersAdd.Name = "btnUsersAdd";
            btnUsersAdd.Size = new Size(97, 27);
            btnUsersAdd.TabIndex = 11;
            btnUsersAdd.Text = "Add";
            btnUsersAdd.UseVisualStyleBackColor = true;
            btnUsersAdd.Click += btnUsersAdd_Click;
            // 
            // btnUsersRemove
            // 
            btnUsersRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUsersRemove.Location = new Point(232, 476);
            btnUsersRemove.Name = "btnUsersRemove";
            btnUsersRemove.Size = new Size(105, 27);
            btnUsersRemove.TabIndex = 10;
            btnUsersRemove.Text = "Remove";
            btnUsersRemove.UseVisualStyleBackColor = true;
            btnUsersRemove.Click += btnUsersRemove_Click;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(18, 77);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.Size = new Size(673, 393);
            dgvUsers.TabIndex = 4;
            // 
            // cbUsersSearch
            // 
            cbUsersSearch.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUsersSearch.FormattingEnabled = true;
            cbUsersSearch.Location = new Point(18, 15);
            cbUsersSearch.Name = "cbUsersSearch";
            cbUsersSearch.Size = new Size(121, 28);
            cbUsersSearch.TabIndex = 3;
            // 
            // tbUsersSearch
            // 
            tbUsersSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbUsersSearch.Location = new Point(145, 16);
            tbUsersSearch.Name = "tbUsersSearch";
            tbUsersSearch.Size = new Size(426, 27);
            tbUsersSearch.TabIndex = 2;
            // 
            // btnUsersSearch
            // 
            btnUsersSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUsersSearch.Location = new Point(577, 16);
            btnUsersSearch.Name = "btnUsersSearch";
            btnUsersSearch.Size = new Size(100, 27);
            btnUsersSearch.TabIndex = 1;
            btnUsersSearch.Text = "Search";
            btnUsersSearch.UseVisualStyleBackColor = true;
            btnUsersSearch.Click += btnUsersSearch_Click;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnOthers);
            pnlButtons.Controls.Add(btnStatistics);
            pnlButtons.Controls.Add(btnUsers);
            pnlButtons.Controls.Add(btnFeeds);
            pnlButtons.Location = new Point(12, 97);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(128, 365);
            pnlButtons.TabIndex = 2;
            // 
            // btnOthers
            // 
            btnOthers.Location = new Point(17, 175);
            btnOthers.Name = "btnOthers";
            btnOthers.Size = new Size(91, 33);
            btnOthers.TabIndex = 6;
            btnOthers.Text = "Others";
            btnOthers.UseVisualStyleBackColor = true;
            btnOthers.Click += btnOthers_Click;
            // 
            // btnStatistics
            // 
            btnStatistics.Location = new Point(17, 125);
            btnStatistics.Name = "btnStatistics";
            btnStatistics.Size = new Size(91, 33);
            btnStatistics.TabIndex = 5;
            btnStatistics.Text = "Statistics";
            btnStatistics.UseVisualStyleBackColor = true;
            btnStatistics.Click += btnStatistics_Click;
            // 
            // btnUsers
            // 
            btnUsers.Location = new Point(17, 72);
            btnUsers.Name = "btnUsers";
            btnUsers.Size = new Size(91, 33);
            btnUsers.TabIndex = 4;
            btnUsers.Text = "Users";
            btnUsers.UseVisualStyleBackColor = true;
            btnUsers.Click += btnUsers_Click;
            // 
            // btnFeeds
            // 
            btnFeeds.Location = new Point(17, 21);
            btnFeeds.Name = "btnFeeds";
            btnFeeds.Size = new Size(91, 33);
            btnFeeds.TabIndex = 3;
            btnFeeds.Text = "Feed";
            btnFeeds.UseVisualStyleBackColor = true;
            btnFeeds.Click += btnFeeds_Click;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI Semibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHeader.Location = new Point(152, 8);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(194, 30);
            lblHeader.TabIndex = 3;
            lblHeader.Text = "Some Guiding Text";
            // 
            // pbLogo
            // 
            pbLogo.Image = Properties.Resources.hamster_logo;
            pbLogo.Location = new Point(15, 8);
            pbLogo.Name = "pbLogo";
            pbLogo.Size = new Size(122, 83);
            pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbLogo.TabIndex = 4;
            pbLogo.TabStop = false;
            // 
            // pnlStatistics
            // 
            pnlStatistics.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlStatistics.Location = new Point(165, 41);
            pnlStatistics.Name = "pnlStatistics";
            pnlStatistics.Size = new Size(711, 509);
            pnlStatistics.TabIndex = 9;
            // 
            // pnlOthers
            // 
            pnlOthers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlOthers.Controls.Add(lblRolesError);
            pnlOthers.Controls.Add(lblCategoriesError);
            pnlOthers.Controls.Add(btnOtherRolesRemove);
            pnlOthers.Controls.Add(label2);
            pnlOthers.Controls.Add(tbOtherRoles);
            pnlOthers.Controls.Add(btnOtherRolesAdd);
            pnlOthers.Controls.Add(lbOtherRoles);
            pnlOthers.Controls.Add(btnOtherCategoriesRemove);
            pnlOthers.Controls.Add(label1);
            pnlOthers.Controls.Add(tbOtherCategories);
            pnlOthers.Controls.Add(btnOtherCategoriesAdd);
            pnlOthers.Controls.Add(lbOtherCategories);
            pnlOthers.Location = new Point(165, 41);
            pnlOthers.Name = "pnlOthers";
            pnlOthers.Size = new Size(711, 509);
            pnlOthers.TabIndex = 10;
            // 
            // lblRolesError
            // 
            lblRolesError.AutoSize = true;
            lblRolesError.BackColor = Color.Linen;
            lblRolesError.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblRolesError.ForeColor = Color.Firebrick;
            lblRolesError.Location = new Point(391, 51);
            lblRolesError.Name = "lblRolesError";
            lblRolesError.Size = new Size(69, 17);
            lblRolesError.TabIndex = 13;
            lblRolesError.Text = "Error Roles";
            // 
            // lblCategoriesError
            // 
            lblCategoriesError.AutoSize = true;
            lblCategoriesError.BackColor = Color.Linen;
            lblCategoriesError.Font = new Font("Segoe UI", 9.75F, FontStyle.Italic);
            lblCategoriesError.ForeColor = Color.Firebrick;
            lblCategoriesError.Location = new Point(18, 51);
            lblCategoriesError.Name = "lblCategoriesError";
            lblCategoriesError.Size = new Size(98, 17);
            lblCategoriesError.TabIndex = 12;
            lblCategoriesError.Text = "Error Categories";
            // 
            // btnOtherRolesRemove
            // 
            btnOtherRolesRemove.Location = new Point(571, 43);
            btnOtherRolesRemove.Name = "btnOtherRolesRemove";
            btnOtherRolesRemove.Size = new Size(85, 28);
            btnOtherRolesRemove.TabIndex = 11;
            btnOtherRolesRemove.Text = "Remove";
            btnOtherRolesRemove.UseVisualStyleBackColor = true;
            btnOtherRolesRemove.Click += btnOtherRolesRemove_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            label2.Location = new Point(391, 24);
            label2.Name = "label2";
            label2.Size = new Size(57, 25);
            label2.TabIndex = 10;
            label2.Text = "Roles";
            // 
            // tbOtherRoles
            // 
            tbOtherRoles.Location = new Point(391, 394);
            tbOtherRoles.Name = "tbOtherRoles";
            tbOtherRoles.Size = new Size(174, 27);
            tbOtherRoles.TabIndex = 9;
            // 
            // btnOtherRolesAdd
            // 
            btnOtherRolesAdd.Location = new Point(571, 394);
            btnOtherRolesAdd.Name = "btnOtherRolesAdd";
            btnOtherRolesAdd.Size = new Size(85, 28);
            btnOtherRolesAdd.TabIndex = 8;
            btnOtherRolesAdd.Text = "Add";
            btnOtherRolesAdd.UseVisualStyleBackColor = true;
            btnOtherRolesAdd.Click += btnOtherRolesAdd_Click;
            // 
            // lbOtherRoles
            // 
            lbOtherRoles.FormattingEnabled = true;
            lbOtherRoles.ItemHeight = 20;
            lbOtherRoles.Location = new Point(391, 77);
            lbOtherRoles.Name = "lbOtherRoles";
            lbOtherRoles.Size = new Size(265, 304);
            lbOtherRoles.TabIndex = 7;
            // 
            // btnOtherCategoriesRemove
            // 
            btnOtherCategoriesRemove.Location = new Point(198, 43);
            btnOtherCategoriesRemove.Name = "btnOtherCategoriesRemove";
            btnOtherCategoriesRemove.Size = new Size(85, 28);
            btnOtherCategoriesRemove.TabIndex = 6;
            btnOtherCategoriesRemove.Text = "Remove";
            btnOtherCategoriesRemove.UseVisualStyleBackColor = true;
            btnOtherCategoriesRemove.Click += btnOtherCategoriesRemove_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            label1.Location = new Point(18, 24);
            label1.Name = "label1";
            label1.Size = new Size(103, 25);
            label1.TabIndex = 5;
            label1.Text = "Categories";
            // 
            // tbOtherCategories
            // 
            tbOtherCategories.Location = new Point(18, 394);
            tbOtherCategories.Name = "tbOtherCategories";
            tbOtherCategories.Size = new Size(174, 27);
            tbOtherCategories.TabIndex = 3;
            // 
            // btnOtherCategoriesAdd
            // 
            btnOtherCategoriesAdd.Location = new Point(198, 394);
            btnOtherCategoriesAdd.Name = "btnOtherCategoriesAdd";
            btnOtherCategoriesAdd.Size = new Size(85, 28);
            btnOtherCategoriesAdd.TabIndex = 1;
            btnOtherCategoriesAdd.Text = "Add";
            btnOtherCategoriesAdd.UseVisualStyleBackColor = true;
            btnOtherCategoriesAdd.Click += btnOtherCategoriesAdd_Click;
            // 
            // lbOtherCategories
            // 
            lbOtherCategories.FormattingEnabled = true;
            lbOtherCategories.ItemHeight = 20;
            lbOtherCategories.Location = new Point(18, 77);
            lbOtherCategories.Name = "lbOtherCategories";
            lbOtherCategories.Size = new Size(265, 304);
            lbOtherCategories.TabIndex = 0;
            // 
            // Dashboard_Form
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(888, 562);
            Controls.Add(lblHeader);
            Controls.Add(pbLogo);
            Controls.Add(pnlButtons);
            Controls.Add(btnLogOut);
            Controls.Add(pnlFeeds);
            Controls.Add(pnlUsers);
            Controls.Add(pnlOthers);
            Controls.Add(pnlStatistics);
            Font = new Font("Segoe UI", 11F);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(904, 601);
            Name = "Dashboard_Form";
            StartPosition = FormStartPosition.CenterParent;
            Text = " ";
            FormClosed += Dashboard_Form_FormClosed;
            pnlFeeds.ResumeLayout(false);
            pnlFeeds.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFeeds).EndInit();
            pnlUsers.ResumeLayout(false);
            pnlUsers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbLogo).EndInit();
            pnlOthers.ResumeLayout(false);
            pnlOthers.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnLogOut;
        private Panel pnlFeeds;
        private Panel pnlButtons;
        private Button button4;
        private Button btnStatistics;
        private Button btnUsers;
        private Button btnFeeds;
        private Label lblHeader;
        private Panel pnlUsers;
        private Button btnUsersSearch;
        private TextBox tbUsersSearch;
        private DataGridView dgvUsers;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn notesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private ComboBox cbUsersSearch;
        private PictureBox pbLogo;
        private ComboBox cbHomeSearch;
        private TextBox tbHomeSearch;
        private Button btnHomeSearch;
        private DataGridView dgvFeeds;
        private DataGridViewImageColumn imageDataGridViewImageColumn;
        private Panel pnlStatistics;
        private Button btnUsersAdd;
        private Button btnUsersRemove;
        private Button btnOthers;
        private Panel pnlOthers;
        private Button btnOtherRolesRemove;
        private Label label2;
        private TextBox tbOtherRoles;
        private Button btnOtherRolesAdd;
        private ListBox lbOtherRoles;
        private Button btnOtherCategoriesRemove;
        private Label label1;
        private TextBox tbOtherCategories;
        private Button btnOtherCategoriesAdd;
        private ListBox lbOtherCategories;
        private Label lblUserErrors;
        private Label lblFeedErrors;
        private Label lblRolesError;
        private Label lblCategoriesError;
        private Button btnFeedsAdd;
        private Button btnFeedsRemove;
        private Button btnFeedsUpdate;
        private Button btnUsersUpdate;
    }
}