using Data;
using Data.DTOs;
using Data.Interfaces;
using Domain;
using Domain.Services;
using System.Collections.Generic;

namespace UnitTests
{
    internal class MockDbUsers : IDbConnectionUsers
    {
        private Dictionary<int, UserDTO> users = new();
        private Dictionary<int, FeedDTO> feeds = new();
        private Dictionary<int, ReviewDTO> reviews = new();
        private Dictionary<int, string> categories = new();

        private List<KeyValuePair<int, int>> users_LikedFeeds = new();
        private List<KeyValuePair<int, int>> users_LikedReviews = new();
        private List<KeyValuePair<int, int>> users_LikedCategories = new();

        public MockDbUsers(Dictionary<int, UserDTO> users, Dictionary<int, FeedDTO> feeds, Dictionary<int, ReviewDTO> reviews, Dictionary<int, string> categories, List<KeyValuePair<int, int>> users_LikedFeeds, List<KeyValuePair<int, int>> users_LikedReviews, List<KeyValuePair<int, int>> users_LikedCategories)
        {
            this.users = users;
            this.feeds = feeds;
            this.reviews = reviews;
            this.categories = categories;
            this.users_LikedFeeds = users_LikedFeeds;
            this.users_LikedReviews = users_LikedReviews;
            this.users_LikedCategories = users_LikedCategories;
        }

        // User CRUD
        public DBResult InsertUser(UserDTO userDTO)
        {
            users.Add(userDTO.Id, userDTO);
            return new DBResult(true);
        }

        public UserDTO? LoadUser(string email, bool active = true)
        {
            return users.Values.ToList().Find(s => s.Email == email);
        }

        public List<UserDTO> LoadUsers(string email = "", string username = "", string role = "", int count = 100, OrderByUsers? orderBy = null, bool active = true)
        {
            return users.Values.ToList().Where(s => s.Email.Contains(email) && s.Username.Contains(username) && s.Role.Contains(role)).ToList();
        }

        public DBResult UpdateUser(UserDTO userDTO)
        {
            users[userDTO.Id] = userDTO;
            return new DBResult(true);
        }

        public DBResult DeleteUser(int userID)
        {
            users.Remove(userID);
            return new DBResult(true);
        }

        // User Checks
        public DBResult CheckIfUserExists(string email, string username)
        {
            var userList = users.Values.ToList();
            if (userList.Find(u => u.Email == email) != null) return new DBResult(true, "Email is taken");
            if (userList.Find(u => u.Username == username) != null) return new DBResult(true, "Username is taken");
            return new DBResult(false, "Both fields are not in use");
        }

        public DBResult CheckIfFeedLiked(int feedId, int userId)
        {
            var likedFeed = users_LikedFeeds.Where(pair => pair.Key == userId && pair.Value == feedId).FirstOrDefault();

            if (likedFeed.Equals(default(KeyValuePair<int,int>))) return new DBResult(false);
            return new DBResult(true);
        }

        // Categories
        public List<string> LoadUserCategories(int userID, int count = 100, OrderByCategories? orderBy = null, bool active = true)
        {
            List<string> result = new();
            foreach (KeyValuePair<int, int> categoryId in users_LikedCategories.Where(pair => pair.Key == userID))
            {
                result.Add(categories[categoryId.Value]);
            }
            return result;
        }

        public DBResult UpdateUserCategory(List<string> newCategories, int userID)
        {
            // Same code as Load Categories
            List<int> oldCategories = new();
            foreach (KeyValuePair<int, int> categoryIds in users_LikedCategories.Where(pair => pair.Key == userID))
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
                    users_LikedCategories.Remove(new(userID, old));
                }
            }
            // Then add all categories that are left
            foreach (var newCategory in newCategories)
            {
                users_LikedCategories.Add(new(userID, categories.Where(c => c.Value == newCategory).First().Key));
            }
            return new DBResult(true);
        }

        // Liked Feeds
        public DBResult AddLikedFeed(int feedId, int userID)
        {
            users_LikedFeeds.Add(new(userID, feedId));
            return new DBResult(true);
        }

        public DBResult UpdateLikedFeeds(List<int> feedIds, int userID)
        {
            var likedFeeds = users_LikedFeeds.Where(pair => pair.Key == userID);
            foreach (var keyPair in likedFeeds)
            {
                // If values already in "Database" remove them from feedIds
                if (feedIds.Contains(keyPair.Value))
                {
                    feedIds.Remove(keyPair.Value);
                }
                // If values that are in "Database" arent in the list remove them
                else
                {
                    users_LikedFeeds.Remove(keyPair);
                }
            }
            // Then add all feeds that are left
            foreach (var feedId in feedIds) users_LikedFeeds.Add(new(userID, feedId));
            return new DBResult(true);
        }

        public async Task<List<FeedDTO>> LoadLikedFeedsAsync(int userID, string filter = "", int count = 100, OrderByFeeds? orderBy = null, bool active = true)
        {
            var likedFeedIDs = users_LikedFeeds.Where(pair=>pair.Key == userID).ToDictionary().Values.ToList();
            List<FeedDTO> likedFeeds = new();
            foreach (var feedID in likedFeedIDs)
            {
                likedFeeds.AddRange(feeds.Where(pair => pair.Key == feedID).ToDictionary().Values.ToList());
            }
            return likedFeeds;
        }

        public DBResult RemoveLikedFeed(int feedId, int userID)
        {
            users_LikedFeeds.RemoveAll(pair => pair.Key == userID && pair.Value == feedId);
            return new DBResult(true);
        }

        // Reviews Feeds
        public DBResult AddLikedReview(int reviewId, int userID)
        {
            users_LikedReviews.Add(new(userID, reviewId));
            return new DBResult(true);
        }

        public DBResult UpdateLikedReviews(List<int> reviewIds, int userID)
        {
            var likedReviews = users_LikedReviews.Where(pair => pair.Key == userID);
            foreach (var keyPair in likedReviews)
            {
                // If values already in "Database" remove them from reviewIds
                if (reviewIds.Contains(keyPair.Value))
                {
                    reviewIds.Remove(keyPair.Value);
                }
                // If values that are in "Database" arent in the list remove them
                else
                {
                    users_LikedReviews.Remove(keyPair);
                }
            }
            // Then add all reviews that are left
            foreach (var reviewId in reviewIds) users_LikedReviews.Add(new(userID, reviewId));
            return new DBResult(true);
        }

        public List<ReviewDTO> LoadLikedReviews(int userID, int count = 100, OrderByReviews? orderBy = null, int feedID = 0, bool active = true)
        {
            List<int> likedReviewIDs = new();
            if (feedID != 0) likedReviewIDs = users_LikedReviews.Where(pair => pair.Key == userID && pair.Value == feedID).ToDictionary().Values.ToList();
            else likedReviewIDs = users_LikedReviews.Where(pair => pair.Key == userID).ToDictionary().Values.ToList();

            List<ReviewDTO> likedReviews = new();
            foreach (var reviewID in likedReviewIDs)
            {
                likedReviews.AddRange(reviews.Where(pair => pair.Key == reviewID).ToDictionary().Values.ToList());
            }
            return likedReviews;
        }

        public DBResult RemoveLikedReview(int reviewId, int userID)
        {
            users_LikedReviews.RemoveAll(pair => pair.Key == userID && pair.Value == reviewId);
            return new DBResult(true);
        }

        // Algorithm stuff
        public List<int> LoadLikedUsers(int userID, int count = 100)
        {
            var likedReviewIDs = users_LikedReviews.Where(pair => pair.Key == userID).ToDictionary().Values.ToList();

            List<ReviewDTO> reviewDTOs = new();
            foreach (var reviewID in likedReviewIDs)
            {
                reviewDTOs.AddRange(reviews.Where(pair => pair.Key == reviewID).ToDictionary().Values.ToList());
            }

            List<int> likedUserIDs = new();
            foreach (var reviewDTO in reviewDTOs)
            {
                likedUserIDs.Add(reviewDTO.UserID);
            }

            return likedUserIDs;
        }
    }
}
