using CodeHollow.FeedReader;
using Data;
using Data.DatabaseConnections;
using Domain;
using Domain.Services;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Dashboard_Form : Form
    {
        Domain.User LoggedInUser;
        List<Panel> panels = new();

        UserService userService = new();
        FeedService feedService = new();
        ParserService parserService = new();
        RoleService roleService = new();
        CategoryService categoryService = new();

        public Dashboard_Form(Domain.User user)
        {
            InitializeComponent();
            LoggedInUser = user;
            this.Text = $"Hello {user.Name}";
            lblHeader.Text = "Shall we look at some Feeds?";

            // Going through panel controls and adding them to the Panels list
            // Panels in list include: Home, Users
            foreach (Control control in Controls)
            {
                Panel? panel = control as Panel;
                if (panel != null)
                {
                    if (panel.Name != "pnlButtons") panels.Add(panel);
                    if (panel.Name == "pnlHome") panel.BringToFront();
                }
            }


            // Setting up DataGridViews and Search Filters

            // Feeds
            dgvFeeds.AutoGenerateColumns = false;
            dgvFeeds.AllowUserToAddRows = false;
            //dgvFeeds.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgvFeeds.Columns.Add("ID", "ID");
            dgvFeeds.Columns["ID"].DataPropertyName = "ID";

            dgvFeeds.Columns.Add("Name", "Name");
            dgvFeeds.Columns["Name"].DataPropertyName = "Name";

            dgvFeeds.Columns.Add("Source", "Source");
            dgvFeeds.Columns["Source"].DataPropertyName = "Source";

            dgvFeeds.Columns.Add("FeedParser", "FeedParser");
            dgvFeeds.Columns["FeedParser"].DataPropertyName = "FeedParserToString";

            dgvFeeds.Columns.Add("FeedItemParser", "FeedItemParser");
            dgvFeeds.Columns["FeedItemParser"].DataPropertyName = "FeedItemParserToString";

            dgvFeeds.Columns.Add("Categories", "Categories");
            dgvFeeds.Columns["Categories"].DataPropertyName = "NumberOfCategories";

            // Combo Box Filters
            cbHomeSearch.Items.Clear();
            foreach (DataGridViewColumn column in dgvFeeds.Columns)
            {
                cbHomeSearch.Items.Add(column);
                cbHomeSearch.DisplayMember = "HeaderText";
            }
            cbHomeSearch.SelectedIndex = 0;

            // Users
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.AllowUserToAddRows = false;
            //dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            dgvUsers.Columns.Add("ID", "ID");
            dgvUsers.Columns["ID"].DataPropertyName = "ID";

            dgvUsers.Columns.Add("Name", "Name");
            dgvUsers.Columns["Name"].DataPropertyName = "Name";

            dgvUsers.Columns.Add("Username", "Username");
            dgvUsers.Columns["Username"].DataPropertyName = "Username";

            dgvUsers.Columns.Add("Email", "Email");
            dgvUsers.Columns["Email"].DataPropertyName = "Email";

            dgvUsers.Columns.Add("Role", "Role");
            dgvUsers.Columns["Role"].DataPropertyName = "Role";

            dgvUsers.Columns.Add("Categories", "Liked Categories");
            dgvUsers.Columns["Categories"].DataPropertyName = "NumberOfCategories";

            dgvUsers.Columns.Add("Reviews", "Reviews");
            dgvUsers.Columns["Reviews"].DataPropertyName = "NumberOfReviews";

            // Combo Box Filters
            cbUsersSearch.Items.Clear();
            foreach (DataGridViewColumn column in dgvUsers.Columns)
            {
                cbUsersSearch.Items.Add(column);
                cbUsersSearch.DisplayMember = "HeaderText";

            }
            cbUsersSearch.SelectedIndex = 0;

            UpdateFromDatabase();
        }

        #region Data
        private void UpdateFromDatabase()
        {
            UpdateFeedsFromDatabase();
            UpdateUsersFromDatabase();
            UpdateRolesFromDatabase();
            UpdateCategoriesFromDatabase();
        }

        private async void UpdateFeedsFromDatabase()
        {
            List<Domain.Feed> feeds = new();
            lblFeedErrors.Text = String.Empty;

            try
            {
                feeds = await feedService.LoadFeedsAsync();
            }
            catch (Exception ex)
            {
                lblFeedErrors.Text = ex.Message;
                return;
            }

            dgvFeeds.DataSource = feeds;
        }
        private void UpdateUsersFromDatabase()
        {
            List<Domain.User> users = new();
            lblUserErrors.Text = String.Empty;

            try
            {
                users = userService.GetAll();
            }
            catch (Exception ex)
            {
                lblUserErrors.Text = ex.Message;
                return;
            }

            dgvUsers.DataSource = users;
        }
        private void UpdateRolesFromDatabase()
        {
            List<string> roles = new();
            lblRolesError.Text = String.Empty;

            try
            {
                roles = roleService.GetAll();
            }
            catch (Exception ex)
            {
                lblRolesError.Text = ex.Message;
                return;
            }

            lbOtherRoles.Items.Clear();
            lbOtherRoles.Items.AddRange(roles.ToArray());
        }
        private void UpdateCategoriesFromDatabase()
        {
            List<string> categories = new();
            lblCategoriesError.Text = String.Empty;

            try
            {
                categories = categoryService.GetAll();
            }
            catch (Exception ex)
            {
                lblCategoriesError.Text = ex.Message;
                return;
            }

            lbOtherCategories.Items.Clear();
            lbOtherCategories.Items.AddRange(categories.ToArray());
        }
        #endregion

        #region Navigation Panel

        private void btnFeeds_Click(object sender, EventArgs e)
        {
            UpdateFeedsFromDatabase();
            lblHeader.Text = "Shall we look at some Feeds?";
            panels.Find(p => p.Name == "pnlFeeds").BringToFront();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            UpdateUsersFromDatabase();
            lblHeader.Text = "Users management it is";
            panels.Find(p => p.Name == "pnlUsers").BringToFront();
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            lblHeader.Text = "This page is still in the works, unfortunately";
            panels.Find(p => p.Name == "pnlStatistics").BringToFront();
        }
        private void btnOthers_Click(object sender, EventArgs e)
        {
            UpdateRolesFromDatabase();
            UpdateCategoriesFromDatabase();
            lblHeader.Text = "Welcome to the...others";
            panels.Find(p => p.Name == "pnlOthers").BringToFront();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Feeds

        private void btnFeedsSearch_Click(object sender, EventArgs e)
        {
            UpdateFeedsFromDatabase();

            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgvFeeds.DataSource];
            currencyManager1.SuspendBinding();

            // Reset all rows to visible
            foreach (DataGridViewRow row in dgvFeeds.Rows) row.Visible = true;
            // Filter rows to hide
            foreach (DataGridViewRow row in dgvFeeds.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.OwningColumn.DataPropertyName == ((DataGridViewColumn)cbHomeSearch.SelectedItem).DataPropertyName
                        && !cell.Value.ToString().Contains(tbHomeSearch.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        row.Visible = false;
                    }
                }
            }
            currencyManager1.ResumeBinding();
            dgvFeeds.Refresh();
            // https://stackoverflow.com/questions/18942017/unable-to-set-row-visible-false-of-a-datagridview
            // This is necessary because of some kinks with assigning a DataSource
        }

        private void btnFeedsAdd_Click(object sender, EventArgs e)
        {
            // New form for a new Feed
            Feed_Form feedForm = new(feedService, categoryService, parserService);
            feedForm.Owner = this;
            feedForm.ShowDialog();
            UpdateFeedsFromDatabase();
        }
        private void btnFeedsUpdate_Click(object sender, EventArgs e)
        {
            if (dgvFeeds.SelectedRows.Count != 1)
            {
                lblFeedErrors.Text = "No Feed selected";
                return;
            }
            Domain.Feed? feed = dgvFeeds.SelectedRows[0].DataBoundItem as Domain.Feed;
            if (feed == null)
            {
                lblFeedErrors.Text = "Feed not found";
                return;
            }

            Feed_Form feedForm = new(feedService, categoryService, parserService, feed);
            feedForm.Owner = this;
            feedForm.ShowDialog();
            UpdateFeedsFromDatabase();
        }

        private void btnFeedsRemove_Click(object sender, EventArgs e)
        {
            if (dgvFeeds.SelectedRows.Count != 1)
            {
                lblFeedErrors.Text = "No Feed selected";
            }
            Domain.Feed? feed = dgvFeeds.SelectedRows[0].DataBoundItem as Domain.Feed;
            if (feed == null)
            {
                lblFeedErrors.Text = "Feed not found";
                return;
            }

            DBResult dBResult = feedService.Delete(feed);

            if (!dBResult.Success)
            {
                lblFeedErrors.Text = dBResult.Message;
                return;
            }
            UpdateFeedsFromDatabase();
        }
        #endregion

        #region Users

        private void btnUsersSearch_Click(object sender, EventArgs e)
        {
            UpdateUsersFromDatabase();

            CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgvUsers.DataSource];
            currencyManager1.SuspendBinding();

            // Reset all rows to visible
            foreach (DataGridViewRow row in dgvUsers.Rows) row.Visible = true;
            // Filter rows to hide
            foreach (DataGridViewRow row in dgvUsers.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.OwningColumn.DataPropertyName == ((DataGridViewColumn)cbUsersSearch.SelectedItem).DataPropertyName
                        && !cell.Value.ToString().Contains(tbUsersSearch.Text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        row.Visible = false;
                    }
                }
            }
            currencyManager1.ResumeBinding();
            // https://stackoverflow.com/questions/18942017/unable-to-set-row-visible-false-of-a-datagridview
            // This is necessary because of some kinks with assigning a DataSource
        }

        private void btnUsersAdd_Click(object sender, EventArgs e)
        {
            // New form for a new User
            User_Form userForm = new(userService, roleService, categoryService);
            userForm.Owner = this;
            userForm.ShowDialog();
            UpdateUsersFromDatabase();
        }

        private void btnUsersUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count != 1)
            {
                lblUserErrors.Text = "No User selected";
                return;
            }
            Domain.User? user = dgvUsers.SelectedRows[0].DataBoundItem as Domain.User;
            if (user == null)
            {
                lblUserErrors.Text = "User not found";
                return;
            }

            // Filled in form
            User_Form userForm = new(userService, roleService, categoryService, user);
            userForm.Owner = this;
            userForm.ShowDialog();
            UpdateUsersFromDatabase();
        }

        private void btnUsersRemove_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count != 1)
            {
                lblUserErrors.Text = "No User selected";
            }
            Domain.User? user = dgvUsers.SelectedRows[0].DataBoundItem as Domain.User;
            if (user == null)
            {
                lblUserErrors.Text = "User not found";
                return;
            }

            DBResult dBResult = userService.Delete(user);

            if (!dBResult.Success)
            {
                lblUserErrors.Text = dBResult.Message;
                return;
            }
            UpdateUsersFromDatabase();
        }
        #endregion

        #region Others
        private void btnOtherCategoriesRemove_Click(object sender, EventArgs e)
        {
            if (lbOtherCategories.SelectedItems == null)
            {
                lblCategoriesError.Text = "*Please select a Category first";
                return;
            }

            DBResult dBResult = categoryService.Delete(lbOtherCategories.SelectedItems[0].ToString());

            if (!dBResult.Success)
            {
                lblCategoriesError.Text = dBResult.Message;
                return;
            }
            UpdateCategoriesFromDatabase();
        }

        private void btnOtherCategoriesAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbOtherCategories.Text))
            {
                lblCategoriesError.Text = "*Please give the Category a Name first";
                return;
            }

            DBResult dBResult = categoryService.Add(tbOtherCategories.Text);

            if (!dBResult.Success)
            {
                lblCategoriesError.Text = dBResult.Message;
                return;
            }
            tbOtherCategories.Text = string.Empty;
            tbOtherCategories.Focus();
            UpdateCategoriesFromDatabase();
        }

        private void btnOtherRolesRemove_Click(object sender, EventArgs e)
        {
            if (lbOtherRoles.SelectedItems == null)
            {
                lblRolesError.Text = "*Please select a Role first";
                return;
            }

            DBResult dBResult = roleService.Delete(lbOtherRoles.SelectedItems[0].ToString());

            if (!dBResult.Success)
            {
                lblRolesError.Text = dBResult.Message;
                return;
            }
            UpdateRolesFromDatabase();
        }

        private void btnOtherRolesAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbOtherRoles.Text))
            {
                lblRolesError.Text = "*Please give the Role a Name first";
                return;
            }

            DBResult dBResult = roleService.Add(tbOtherRoles.Text);

            if (!dBResult.Success)
            {
                lblRolesError.Text = dBResult.Message;
                return;
            }
            tbOtherRoles.Text = string.Empty;
            tbOtherRoles.Focus();
            UpdateRolesFromDatabase();
        }
        #endregion

        private void Dashboard_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Show();
        }
    }
}
