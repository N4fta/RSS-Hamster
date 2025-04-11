using CodeHollow.FeedReader;
using Data;
using Domain;
using Domain.Interfaces;
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
    public partial class Feed_Form : Form
    {
        private FeedService _feedService;
        private CategoryService _categoryService;
        private ParserService _parserService;
        private Domain.Feed? _feed;
        public Feed_Form(FeedService feedService, CategoryService categoryService, ParserService parserService)
        {
            InitializeComponent();
            ResetErrorLabels();
            _feedService = feedService;
            _categoryService = categoryService;
            _parserService = parserService;
            SetupComboBoxes();
        }
        public Feed_Form(FeedService feedService, CategoryService categoryService, ParserService parserService, Domain.Feed feed) : this(feedService, categoryService, parserService)
        {
            _feed = feed;

            // Fill in
            tbName.Text = feed.Name;
            tbSource.Text = feed.Source;
            cbParser.SelectedIndex = cbParser.FindStringExact(feed.FeedParser.ToString());
            cbItemParser.SelectedIndex = cbItemParser.FindStringExact(feed.FeedItemParser.ToString());
            // Image
            foreach (var category in feed.Categories)
            {
                clbCategories.SetItemChecked(clbCategories.Items.IndexOf(category), true);
            }
        }

        private void SetupComboBoxes()
        {
            // Parsers
            cbParser.Items.AddRange(_parserService.GetAllParsers().ToArray());
            cbParser.DisplayMember = "ToString";
            cbParser.SelectedIndex = 0;
            cbItemParser.Items.AddRange(_parserService.GetAllItemParsers().ToArray());
            cbItemParser.DisplayMember = "ToString";
            cbItemParser.SelectedIndex = 0;

            // Categories
            clbCategories.Items.AddRange(_categoryService.GetAll().ToArray());
        }

        private void ResetErrorLabels()
        {
            // Input validation
            lblName.Text = string.Empty;
            lblSource.Text = string.Empty;
            lblParser.Text = string.Empty;
            lblItemParser.Text = string.Empty;
            lblImage.Text = string.Empty;
            lblCategories.Text = string.Empty;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            ResetErrorLabels();

            // Name Validation
            if (string.IsNullOrEmpty(tbName.Text))
            {
                lblName.Text = "*Please input a Name";
                return;
            }

            // Check source link
            if (string.IsNullOrEmpty(tbSource.Text))
            {
                lblSource.Text = "*Please input a Source";
                return;
            }
            CodeHollow.FeedReader.Feed? feedReader = null;
            try
            {
                feedReader = FeedReader.Read(tbSource.Text);
            }
            catch (UriFormatException ex)
            {
                lblSource.Text = "*Invalid Link";
                return;
            }
            catch { }
            if (feedReader == null)
            {
                lblSource.Text = "*Source invalid";
                return;
            }

            // Check Parsers
            if (!(cbParser.SelectedItem is IFeedParser))
            {
                lblParser.Text = "*Please choose a Parser";
                return;
            }
            if (!(cbItemParser.SelectedItem is IFeedItemParser))
            {
                lblItemParser.Text = "*Please choose an Item Parser";
                return;
            }

            // Categories
            List<string> categories = new();
            if (clbCategories.CheckedItems.Count > 0)
            {
                categories.AddRange(clbCategories.CheckedItems.Cast<string>());
            }

            DBResult dBresult;

            if (_feed == null)
            {
                // Validate if Name or Source Taken
                // If Result is true one is taken
                DBResult result = _feedService.CheckIfFeedExists(tbSource.Text, tbName.Text);

                if (result.Success)
                {
                    lblName.Text = "*" + result.Message;
                }

                // New feed
                Domain.Feed newFeed = new Domain.Feed(
                tbName.Text,
                tbSource.Text,
                new(),
                categories,
                (IFeedParser)cbParser.SelectedItem,
                (IFeedItemParser)cbItemParser.SelectedItem
                );
                dBresult = _feedService.Add(newFeed);
            }
            else
            {
                // Updated Feed
                Domain.Feed updatedFeed = new Domain.Feed(
                    _feed.Id,
                    tbName.Text,
                    tbSource.Text,
                    _feed.Reviews,
                    categories,
                    (IFeedParser)cbParser.SelectedItem,
                    (IFeedItemParser)cbItemParser.SelectedItem
                    );
                dBresult = _feedService.Update(updatedFeed);
            }


            if (!dBresult.Success)
            {
                lblName.Text = dBresult.Message;
            }
            else
            {
                MessageBox.Show("Feed saved Successfully");
                this.Close();
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        private void Feed_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Owner.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
