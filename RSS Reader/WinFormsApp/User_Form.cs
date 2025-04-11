using CodeHollow.FeedReader;
using Data;
using Data.DTOs;
using Domain;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class User_Form : Form
    {
        private UserService _userService;
        private RoleService _roleService;
        private CategoryService _categoryService;
        private User? _user;
        public User_Form(UserService userService, RoleService roleService, CategoryService categoryService)
        {
            InitializeComponent();
            ResetErrorLabels();
            _userService = userService;
            _roleService = roleService;
            _categoryService = categoryService;
            // Roles
            cbRole.Items.AddRange(_roleService.GetAll().ToArray());
            // Categories
            clbCategories.Items.AddRange(_categoryService.GetAll().ToArray());
        }
        public User_Form(UserService userService, RoleService roleService, CategoryService categoryService, User user) : this(userService, roleService, categoryService)
        {
            _user = user;

            // Fill in information
            tbName.Text = user.Name;
            tbEmail.Text = user.Email;
            tbUsername.Text = user.Username;
            cbRole.SelectedIndex = cbRole.Items.IndexOf(user.Role);
            rtbNotes.Text = user.Notes;
            foreach (var category in user.Categories)
            {
                clbCategories.SetItemChecked(clbCategories.Items.IndexOf(category), true);
            }
        }

        private void ResetErrorLabels()
        {
            // Input validation
            lblName.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblUsername.Text = string.Empty;
            lblPassword.Text = string.Empty;
            lblRole.Text = string.Empty;
            lblCategories.Text = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ResetErrorLabels();

            // Name validation
            if (string.IsNullOrEmpty(tbName.Text))
            {
                lblName.Text = "*Please input a Name";
                return;
            }
            // Email validation
            if (string.IsNullOrEmpty(tbEmail.Text))
            {
                lblEmail.Text = "*Please input an Email";
                return;
            }
            Regex emailRegex = new Regex("^[\\w\\-\\.]+@([\\w-]+\\.)+[\\w-]{2,}$");
            if (!emailRegex.IsMatch(tbEmail.Text))
            {
                lblEmail.Text = "*Email invalid";
                return;
            }
            // Username validation
            if (string.IsNullOrEmpty(tbUsername.Text))
            {
                lblUsername.Text = "*Please input an Username";
                return;
            }
            // Password validation
            // If _user is != null then we are updating a user, we don't always want to update their password
            if (string.IsNullOrEmpty(tbPassword.Text) && _user == null)
            {
                lblPassword.Text = "*Please input an Password";
                return;
            }
            if (tbPassword.Text.Length < 6 && _user == null)
            {
                lblPassword.Text = "*Password must be more than 6 characters";
                return;
            }
            // Role validation
            if (cbRole.SelectedItem == null)
            {
                lblRole.Text = "*Please choose a role";
                return;
            }
            if (!_roleService.GetAll().Contains(cbRole.SelectedItem))
            {
                lblRole.Text = "*Role invalid";
                return;
            }
            // Categories
            List<string> categories = new();
            if (clbCategories.CheckedItems.Count > 0)
            {
                categories.AddRange(clbCategories.CheckedItems.Cast<string>());
            }

            DBResult dBresult;

            if (_user == null)
            {
                // Create new User
                User newUser = new User(
                    tbName.Text,
                    tbUsername.Text,
                    tbEmail.Text,
                    PasswordHashing.HashPassword(tbPassword.Text),
                    rtbNotes.Text,
                    cbRole.SelectedItem.ToString(),
                    categories
                    );
                dBresult = _userService.Add(newUser);
            }
            else
            {
                // Update User
                string password = _user.HashedPassword;
                if (!string.IsNullOrWhiteSpace(tbPassword.Text))
                {
                    password = PasswordHashing.HashPassword(tbPassword.Text);
                }
                // By using the user ID we can modify the existing DB entry
                User updatedUser = new User(
                    _user.Id,
                    tbName.Text,
                    tbUsername.Text,
                    tbEmail.Text,
                    password,
                    rtbNotes.Text,
                    cbRole.SelectedItem.ToString(),
                    categories,
                    _user.Reviews
                    );

                dBresult = _userService.Update(updatedUser);
            }

            if (!dBresult.Success)
            {
                lblName.Text = dBresult.Message;
            }
            else
            {
                MessageBox.Show("User saved Successfully");
                this.Close();
            }
        }

        private void User_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
