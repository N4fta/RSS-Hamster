using Data;
using Domain.Interfaces;
using Domain.Services;

namespace Domain.Algorithms.Modules
{
    public abstract class BaseAlgoModule : IAlgoModule
    {
        protected readonly FeedService feedService;
        protected readonly UserService userService;

        public abstract string Name { get; set; }

        public BaseAlgoModule(FeedService feedService, UserService userService)
        {
            this.feedService = feedService;
            this.userService = userService;
        }

        public List<Feed> Apply(List<Feed> feedsToFilter)
        {
            if (feedsToFilter == null || feedsToFilter.Count == 0)
            {
                feedsToFilter = GetFeedsToFilter();
            }

            var recommendedFeeds = GetRecommendedFeeds(feedsToFilter.ToList());

            // If we dont have enough feeds add old ones at the end
            if (recommendedFeeds.Count < 40)
            {
                foreach (var feed in feedsToFilter)
                {
                    // Add feeds we didnt add already at the end
                    if (recommendedFeeds.Find(f => f.Id == feed.Id) == null) recommendedFeeds.Add(feed);

                    // If we have enough leave
                    if (recommendedFeeds.Count >= 40) break;
                }
            }

            return recommendedFeeds;
        }

        protected virtual List<Feed> GetFeedsToFilter()
        {
            // If there are no Feeds to Filter then load some to start off
            return feedService.LoadFeedsAlgo(3000, orderBy: OrderByFeeds.Popularity);
        }

        // Gets recommended Feeds based on previous Feed List
        // And something else
        protected abstract List<Feed> GetRecommendedFeeds(List<Feed> feedsToFilter);

    }
}
