using Data.DatabaseConnections;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using Domain.Algorithms;
using Domain.Interfaces;
using Domain.Algorithms.Modules;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using System.Linq;

namespace WebApp.Pages
{
    public class AllFeedsModel : PageModel
    {
        // Pagination Page
        // Based on https://www.mikesdotnetting.com/Article/328/simple-paging-in-asp-net-core-razor-pages

        // Separted the Pagination Service so we Inject the depency
        // and swap it with another IFeed Interface
        private readonly FeedService _feedService;
        private readonly UserService _userService;

        public AllFeedsModel(FeedService feedService, UserService userService)
        {
            _feedService = feedService;
            _userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; } = 2000;
        public int PageSize { get; set; } = 16;

        public int TotalPages = 5; //(int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public string FeedServiceException { get; set; } = string.Empty;
        public List<Feed> FeedList { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                FeedList = await _feedService.LoadFeedsAsync(count: Count);
                Count = FeedList.Count;
                TotalPages = (int)Math.Ceiling(decimal.Divide(Count, PageSize));
                if (Count > PageSize * CurrentPage) FeedList = FeedList.GetRange(PageSize * (CurrentPage - 1), PageSize);
                else FeedList = FeedList.GetRange(PageSize * (CurrentPage - 1), Count - PageSize * (CurrentPage - 1));
            }
            catch (Exception exFeedService)
            {
                if (exFeedService.InnerException is SqlException) FeedServiceException = "Problem with the database connection";
                else FeedServiceException = "Unexpected error, please contact the website mantainer";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string searchBox)
        {
            try
            {
                FeedList = await _feedService.LoadFeedsAsync(searchBox, Count);
            }
            catch (Exception exFeedService)
            {
                if (exFeedService.InnerException is SqlException) FeedServiceException = "Problem with the database connection";
                else FeedServiceException = "Unexpected error, please contact the website mantainer";
            }

            Count = FeedList.Count;
            if (Count == 1) return Redirect($"/FeedDetails?feedID={FeedList[0].Id}");

            TotalPages = (int)Math.Ceiling(decimal.Divide(Count, PageSize));
            if (Count > PageSize * CurrentPage) FeedList = FeedList.GetRange(PageSize * (CurrentPage - 1), PageSize);
            else FeedList = FeedList.GetRange(PageSize * (CurrentPage - 1), Count - PageSize * (CurrentPage - 1));

            return Page();
        }

        [Authorize]
        public async Task<IActionResult> OnPostMyList()
        {
            try
            {
                FeedList = await _userService.LoadLikedFeeds(int.Parse(User.FindFirst("Id").Value), count: Count);
            }
            catch (Exception exFeedService)
            {
                if (exFeedService.InnerException is SqlException) FeedServiceException = "Problem with the database connection";
                else FeedServiceException = "Unexpected error, please contact the website mantainer";
            }

            Count = FeedList.Count;
            // I display all in one page since I dont know how to paginate only these results
            TotalPages = 1;

            return Page();
        }

        [Authorize]
        public async Task<IActionResult> OnPostAlgorithm(List<string> checkedFilters)
        {
            try
            {
                List<AlgoModule> modules = checkedFilters.Select(f => (AlgoModule)Enum.Parse(typeof(AlgoModule), f)).ToList();
                User user = _userService.GetUser(User.FindFirst("Email").Value);
                List<IAlgoModule> filters = AlgoModuleFactory.GetAlgoModule(modules, user, _feedService, _userService);

                FeedList = new Algorithm(filters).GenerateRecommendation();
            }
            catch (Exception exFeedService)
            {
                if (exFeedService.InnerException is SqlException)
                {
                    FeedServiceException = "Problem with the database connection";
                }
                else if (exFeedService.InnerException is InvalidOperationException)
                {
                    FeedServiceException = "Could not create algorithm";
                }
                else if (exFeedService.InnerException is ArgumentNullException)
                {
                    FeedServiceException = "Error, something was missing";
                }
                else FeedServiceException = "Unexpected error, please contact the website mantainer";
            }

            Count = FeedList.Count;
            // I display all feeds in one page, no need to paginate (always returns max 20 Feeds)
            TotalPages = 1;

            return Page();
        }
    }
}
