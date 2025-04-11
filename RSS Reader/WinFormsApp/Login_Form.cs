using Data.DTOs;
using Domain;
using Domain.Services;

namespace WinFormsApp
{
    public partial class Login_Form : Form
    {
        public Login_Form()
        {
            InitializeComponent();
            ResetErrorLabels();
        }

        private void ResetErrorLabels()
        {
            lblUsername.Text = string.Empty;
            lblPassword.Text = string.Empty;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            ResetErrorLabels();

            if (string.IsNullOrEmpty(txtBxEmail.Text))
            {
                lblUsername.Text = "*Please input a Username";
                return;
            }
            if (string.IsNullOrEmpty(txtBxPassword.Text))
            {
                lblPassword.Text = "*Please input a Password";
                return;
            }

            // Try to fetch user from Repo
            User? user = null;
            Exception resultException = null;
            try
            {
                user = new UserService().GetUser(txtBxEmail.Text);
            }
            catch (Exception ex)
            {
                resultException = ex;
            }

            // Display the result
            if (resultException != null)
            {
                lblUsername.Text = $"*Exception: {resultException.Message}";
                return;
            }
            if (user == null)
            {
                lblUsername.Text = "*User not found";
                return;
            }

            // Check permissions
            if (user.Role != "Administrator")
            {
                lblUsername.Text = "*User account lacks permissions";
                return;
            }

            // Check password
            if (!user.VerifyPassword(txtBxPassword.Text))
            {
                lblPassword.Text = "*Incorrect Password";
                return;
            }
            else
            {
                // New form with User logged in
                Dashboard_Form dashboardForm = new(user);
                dashboardForm.Tag = user;
                dashboardForm.Owner = this;
                dashboardForm.Show();
                this.Hide();
            }
        }

    }
}
