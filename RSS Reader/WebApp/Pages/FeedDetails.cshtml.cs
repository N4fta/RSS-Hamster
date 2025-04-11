using Domain;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class FeedDetailsModel : PageModel
    {
        public Feed? feed { get; set; }
        public ParsedFeed parsedFeed { get; set; }
        private readonly FeedService _feedService;
        private readonly UserService _userService;
        public bool LikedFeed { get; set; } = false;
        public List<Review> likedReviews;

        [BindProperty]
        public CreateReview CreateReview { get; set; }

        public FeedDetailsModel(FeedService feedService, UserService userService)
        {
            _feedService = feedService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(int feedID)
        {
            feed = await _feedService.LoadFeedAsync(feedID);
            if (feed == null)
            {
                return RedirectToPage("/AllFeeds");
            }

            if (User.Identity.IsAuthenticated)
            {
                int userID = int.Parse(User.FindFirst("Id").Value);
                // Checks if feed is in Users liked feeds
                if (_userService.CheckIfFeedLiked(feed.Id, userID).Success)
                {
                    LikedFeed = true;
                }

                likedReviews = _userService.LoadLikedReviews(userID, feedID: feed.Id);
            }

            parsedFeed = await feed.ParseFeedAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostLikeFeed(int feedId)
        {
            _userService.AddLikedFeed(feedId, int.Parse(User.FindFirst("Id").Value));

            return await OnGetAsync(feedId);
        }
        public async Task<IActionResult> OnPostUnlikeFeed(int feedId)
        {
            _userService.RemoveLikedFeed(feedId, int.Parse(User.FindFirst("Id").Value));

            return await OnGetAsync(feedId);
        }

        public async Task<IActionResult> OnPostCreateReview(int feedId)
        {
            if (ModelState.IsValid)
            {
                ReviewService reviewService = new();
                reviewService.Add(new Review(CreateReview.Title, CreateReview.MainBody), feedId, int.Parse(User.FindFirst("Id").Value));
            }

            return await OnGetAsync(feedId);
        }
        public async Task<IActionResult> OnPostLikeReview(int reviewId, int feedId)
        {
            _userService.AddLikedReview(reviewId, int.Parse(User.FindFirst("Id").Value));

            return await OnGetAsync(feedId);
        }
        public async Task<IActionResult> OnPostUnlikeReview(int reviewId, int feedId)
        {
            _userService.RemoveLikedReview(reviewId, int.Parse(User.FindFirst("Id").Value));

            return await OnGetAsync(feedId);
        }
    }
}
