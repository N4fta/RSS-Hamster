using Data;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Algorithms.Modules
{
    public class BasedOnLikedReviews : BaseAlgoModule
    {

        private User user;
        public override string Name { get; set; } = "Based On Liked Reviews";

        public BasedOnLikedReviews(User user, FeedService feedService, UserService userService) : base(feedService, userService)
        {
            this.user = user;
        }

        protected override List<Feed> GetRecommendedFeeds(List<Feed> feedsToFilter)
        {
            // Fetch of List of Liked Users
            // Already Sorted by likes
            List<int> likedUserIDs = userService.LoadLikedUsers(user.Id);

            // Get popular feeds from top 10 users
            Dictionary<Feed, int> popularFeeds = new();
            foreach (var likedUserID in likedUserIDs)
            {
                var tempFeeds = userService.LoadLikedFeeds(likedUserID).Result;

                foreach (var feed in tempFeeds)
                {
                    var keyFeed = popularFeeds.Keys.Where(key => key.Id == feed.Id).FirstOrDefault();
                    if (keyFeed != null) popularFeeds[keyFeed]++;
                    else popularFeeds.Add(feed, 1);
                }
            }

            // Look for those those feeds in given list
            var resultFeeds = new List<Feed>();
            foreach (var popularFeed in popularFeeds.Keys.ToList())
            {
                if (feedsToFilter.Find(f => f.Id == popularFeed.Id) != null) resultFeeds.Add(popularFeed);
            }

            return resultFeeds;
        }
    }
}
