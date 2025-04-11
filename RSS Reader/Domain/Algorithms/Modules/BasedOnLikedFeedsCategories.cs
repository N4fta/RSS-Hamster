using CodeHollow.FeedReader;
using Data;
using Data.DatabaseConnections;
using Domain.Interfaces;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Algorithms.Modules
{
    public class BasedOnLikedFeedsCategories : BaseAlgoModule
    {
        private User user;
        public override string Name { get; set; } = "Based On Liked Feeds Categories";

        public BasedOnLikedFeedsCategories(User user, FeedService feedService, UserService userService) : base(feedService, userService)
        {
            this.user = user;
        }

        protected override List<Feed> GetRecommendedFeeds(List<Feed> feedsToFilter)
        {
            // Category and intensity
            Dictionary<string, int> categories = new();

            var likedFeeds = userService.LoadLikedFeeds(user.Id).Result;

            // Get Favourite Categories
            foreach (var likedFeed in likedFeeds)
            {
                foreach (var category in likedFeed.Categories)
                {
                    if (categories.ContainsKey(category)) categories[category]++;
                    else categories.Add(category, 1);
                }
            }

            // Get Average as baseline
            double averageIntensity = categories.Values.Average();

            // Calculate how feeds per category ratios
            int[] feedsPerCategory = new int[categories.Count];
            var catList = categories.ToList();
            for (int i = 0; i < categories.Count; i++)
            {
                // By comparing the number of times a category appears to the average we get the number of feeds of that category to fetch
                // Eg. If your Liked Feeds categories include 5 Science, 3 Educational and 1 News this will fetch TOP 2 most popular Science feeds, TOP 1 Edu and TOP 1 News
                int numberOfFeeds = (int)Math.Ceiling(Convert.ToDouble(catList[i].Value) / averageIntensity);
                feedsPerCategory[i] = numberOfFeeds;
            }

            // Multiply ratios by 2 until we get 50+ feeds total
            while (feedsPerCategory.Sum() < 50) feedsPerCategory = feedsPerCategory.Select(i => i * 2).ToArray();


            // Filtering
            List<Feed> resultFeeds = new List<Feed>();

            for (int i = 0; i < categories.Count; i++)
            {
                string category = catList[i].Key;
                int numberOfFeeds = feedsPerCategory[i];

                // Find feeds with category
                var tempFeeds = feedsToFilter.Where(newFeed => newFeed.Categories.Contains(category)).ToList();

                // Remove Duplicates
                tempFeeds = tempFeeds.Where(newFeed => resultFeeds.Find(oldFeed => oldFeed.Name == newFeed.Name) == null).ToList();

                resultFeeds.AddRange(tempFeeds);
            }

            return resultFeeds;
        }
    }
}
