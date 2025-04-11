using Data;
using Data.DTOs;
using Data.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace UnitTests.Mock_Classes
{

    internal class MockDbFeeds : IDbConnectionFeeds
    {
        private Dictionary<int, FeedDTO> feeds = new();
        private Dictionary<int, string> categories = new();
        private Dictionary<int, ReviewDTO> reviews = new();

        private List<KeyValuePair<int, int>> feeds_reviews = new();
        private List<KeyValuePair<int, int>> feeds_categories = new();

        public MockDbFeeds(Dictionary<int, FeedDTO> feeds, Dictionary<int, string> categories, List<KeyValuePair<int, int>> feeds_reviews, List<KeyValuePair<int, int>> feeds_categories)
        {
            this.feeds = feeds;
            this.categories = categories;
            this.feeds_reviews = feeds_reviews;
            this.feeds_categories = feeds_categories;
        }

        // Feed CRUD
        public DBResult InsertFeed(FeedDTO feedDTO)
        {
            feeds.Add(feedDTO.Id, feedDTO);
            return new DBResult(true);
        }

        public async Task<FeedDTO> LoadFeedAsync(int feedID, bool active = true)
        {
            return feeds[feedID];
        }

        public async Task<List<FeedDTO>> LoadFeedsAsync(string filter = "", int count = 100, OrderByFeeds? orderBy = null, bool active = true)
        {
            List<FeedDTO> feedRepo = feeds.Values.ToList();
            return feedRepo.Where(feed => feed.Name.Contains(filter)).ToList();
        }

        public async Task<int> CountFeeds(string filter = "", bool active = true)
        {
            List<FeedDTO> feedRepo = feeds.Values.ToList();
            return feedRepo.Where(feed => feed.Name.Contains(filter)).Count();
        }

        public DBResult UpdateFeed(FeedDTO feedDTO)
        {
            feeds[feedDTO.Id] = feedDTO;
            return new DBResult(true);
        }

        public DBResult DeleteFeed(int feedID)
        {
            feeds.Remove(feedID);
            return new DBResult(true);
        }

        // Checks
        public DBResult CheckIfFeedExists(string source, string name)
        {
            if (feeds.Values.Where(feed => feed.Source == source).Count() > 0) return new DBResult(true, "Source is taken");
            else if (feeds.Values.Where(feed => feed.Name == name).Count() > 0) return new DBResult(true, "Name is taken");
            else return new DBResult(false, "Both fields are not in use");
        }

        // Categories
        public List<string> LoadFeedCategories(int feedID, int count = 100, OrderByCategories? orderBy = null, bool active = true)
        {
            List<string> categories = new();
            foreach (KeyValuePair<int, int> categoryId in feeds_categories.Where(pair => pair.Key == feedID))
            {
                categories.Add(categories[categoryId.Value]);
            }
            return categories;
        }

        public DBResult UpdateFeedCategory(List<string> newCategories, int feedID)
        {
            // Same code as Load Categories
            List<int> oldCategories = new();
            foreach (KeyValuePair<int, int> categoryIds in feeds_categories.Where(pair => pair.Key == feedID))
            {
                oldCategories.Add(categoryIds.Value);
            }

            foreach (int old in oldCategories)
            {
                string name = categories[old];
                // If values already in "Database" remove them from categories
                // So we dont add them twice at the end
                if (newCategories.Contains(name))
                {
                    newCategories.Remove(name);
                }
                // If values that are in "Database" arent in the list remove them
                else
                {
                    feeds_categories.Remove(new(feedID, old));
                }
            }
            // Then add all categories that are left
            foreach (var newCategory in newCategories)
            {
                feeds_categories.Add(new(feedID, categories.Where(c => c.Value == newCategory).First().Key));
            }
            return new DBResult(true);
        }

        // Reviews
        public List<ReviewDTO> LoadFeedReviews(int feedID, int count = 100, OrderByReviews? orderBy = null, bool active = true)
        {
            List<int> likedReviewIDs = feeds_reviews.Where(pair => pair.Key == feedID).ToDictionary().Values.ToList();

            List<ReviewDTO> likedReviews = new();
            foreach (var reviewID in likedReviewIDs)
            {
                likedReviews.AddRange(reviews.Where(pair => pair.Key == reviewID).ToDictionary().Values.ToList());
            }
            return likedReviews;
        }

        // Algorithm
        public List<FeedDTO> LoadFeedsAlgo(int count = 100, List<string>? categoriesFilters = null, OrderByFeeds? orderBy = null, bool active = true)
        {
            List<FeedDTO> resultFeeds = new();
            List<FeedDTO> feedRepo = feeds.Values.ToList();
            if (categoriesFilters == null || categoriesFilters.Count == 0) return feedRepo;

            foreach (var feed in feedRepo)
            {
                foreach (var category in feed.Categories)
                {
                    if (categoriesFilters.Contains(category))
                    {
                        resultFeeds.Add(feed);
                        break;
                    }
                }
            }
            return resultFeeds;
        }
    }
}
