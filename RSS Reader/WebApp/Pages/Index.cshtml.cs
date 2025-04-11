using CodeHollow.FeedReader;
using Data;
using Domain;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using System.Data.SqlClient;
using System.Reflection;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly FeedService _feedService;
        private readonly UserService _userService;

        public IndexModel(FeedService feedService, ILogger<IndexModel> logger, UserService userService)
        {
            _feedService = feedService;
            _userService = userService;
            _logger = logger;
        }

        public string MyListException { get; set; } = string.Empty;
        public List<Domain.Feed> MyList { get; set; } = new();

        public string NewAdditionsException { get; set; } = string.Empty;
        public List<Domain.Feed> NewAdditions { get; set; } = new();

        public string MostPopularException { get; set; } = string.Empty;
        public List<Domain.Feed> MostPopular { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated && User.HasClaim(c => c.Type == "Id"))
            {
                try
                {
                    MyList = await _userService.LoadLikedFeeds(count: 80, userID: int.Parse(User.FindFirst("Id").Value));
                }
                catch (Exception exMyList)
                {
                    if (exMyList.InnerException is SqlException) MyListException = "Problem with the database connection";
                    else MyListException = "Unexpected error, please contact the website mantainer";
                }
            }

            // Load Popular feeds and new additions 
            try
            {
                NewAdditions = await _feedService.LoadFeedsAsync(count: 80, orderBy: OrderByFeeds.ID_DESC);
            }
            catch (Exception exNewAdditions)
            {
                if (exNewAdditions.InnerException is SqlException) NewAdditionsException = "Problem with the database connection";
                else NewAdditionsException = "Unexpected error, please contact the website mantainer";
            }

            try
            {
                MostPopular = await _feedService.LoadFeedsAsync(count: 80, orderBy: OrderByFeeds.Popularity);
            }
            catch (Exception exMostPopular)
            {
                if (exMostPopular.InnerException is SqlException) MostPopularException = "Problem with the database connection";
                else MostPopularException = "Unexpected error, please contact the website mantainer";
            }

            return Page();
        }
    }
}
