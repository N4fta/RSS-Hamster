using Data;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Algorithms.Modules
{
    public class BasedOnPublishers : BaseAlgoModule
    {
        private User user;
        public override string Name { get; set; } = "Based On Publishers";

        public BasedOnPublishers(User user, FeedService feedService, UserService userService) : base(feedService, userService)
        { 
            this.user = user;
        }

        protected override List<Feed> GetRecommendedFeeds(List<Feed> feedsToFilter)
        {
            // Publisher and intensity
            Dictionary<string, int> publishers = new();
            // Fetch of List of Liked feeds
            var likedFeeds = userService.LoadLikedFeeds(user.Id).Result;

            // Get popular publishers
            foreach (var likedFeed in likedFeeds)
            {
                string publisher;
                if (likedFeed.Source.Contains("https://")) publisher = likedFeed.Source.Split("https://")[1].Split("/")[0];
                else publisher = likedFeed.Source.Split("/")[0];

                if (publishers.ContainsKey(publisher)) publishers[publisher]++;
                else publishers.Add(publisher, 1);
            }

            // Figure average out and get top 10 Publishers
            double averageIntensity = publishers.Values.Average();
            List<string> topPublishers;
            if (publishers.Count > 10) topPublishers = publishers.Where(p => p.Value >= averageIntensity).ToDictionary().Keys.ToList();
            else topPublishers = publishers.Keys.ToList();

            // Making sure we got enough publishers
            if (topPublishers.Count > 10) topPublishers = topPublishers.GetRange(0, 10); // Too many
            if (topPublishers.Count < 10) // Too Little
            {
                var tempPublishers = publishers.Keys.ToList().Where(p => !topPublishers.Contains(p)).ToList(); // Removes Duplicates
                int missing = 10 - topPublishers.Count;
                if (tempPublishers.Count > missing) // Checks if we have enough to reach 10
                {
                    topPublishers.AddRange(tempPublishers.GetRange(0, missing - 1));
                }
                else topPublishers.AddRange(tempPublishers); // Else we take what we can
            }

            //
            // Filtering
            //
           var resultFeeds = new List<Feed>();
            
            foreach (var publisher in topPublishers)
            {
                // Find same Publishers
                var tempFeeds = feedsToFilter.Where(newFeed => newFeed.Source.Contains(publisher)).ToList();

                // Remove Duplicates
                tempFeeds = tempFeeds.Where(newFeed => resultFeeds.Find(oldFeed => oldFeed.Name == newFeed.Name) == null).ToList();

                resultFeeds.AddRange(tempFeeds);
            }
            // Ideally this would be done in the call to the DB but I didnt have the time or expertise in SQL for this

            return resultFeeds;
        }
    }
}
