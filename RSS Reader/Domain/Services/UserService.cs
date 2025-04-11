using CodeHollow.FeedReader;
using Data;
using Data.DatabaseConnections;
using Data.DTOs;
using Data.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class UserService
    {
        private IDbConnectionUsers _dbConnectionUser;

        public UserService()
        {
            _dbConnectionUser = new DbConnectionUsers();
        }
        public UserService(IDbConnectionUsers dbConnectionUser)
        {
            _dbConnectionUser = dbConnectionUser;
        }

        public DBResult CheckIfUserExists(string email, string username)
        {
            return _dbConnectionUser.CheckIfUserExists(email, username);
        }

        public DBResult Add(User user)
        {
            return _dbConnectionUser.InsertUser(ConvertToDTO(user));
        }

        public DBResult Update(User user)
        {
            DBResult dbResult;

            // Save Categories
            dbResult = _dbConnectionUser.UpdateUserCategory(user.Categories, user.Id);
            if (!dbResult.Success) return dbResult;

            // Save Reviews
            dbResult = _dbConnectionUser.UpdateLikedReviews(user.Reviews.Select(review=>review.Id).ToList(), user.Id);
            if (!dbResult.Success) return dbResult;

            return _dbConnectionUser.UpdateUser(ConvertToDTO(user));
        }

        public DBResult Delete(User user)
        {
            return _dbConnectionUser.DeleteUser(user.Id);
        }

        public User? GetUser(string email, bool active = true)
        {
            User? user = ConvertToDomainClass(_dbConnectionUser.LoadUser(email, active));
            return user;
        }

        public List<User> GetAll(string email = "", string username = "", string role = "", int count = 100, OrderByUsers? orderBy = null, bool active = true)
        {
            return ConvertToDomainClass(_dbConnectionUser.LoadUsers(email, username, role, count, orderBy, active));
        }

        #region Liked Feeds
        public DBResult AddLikedFeed(int feedId, int userID)
        {
            return _dbConnectionUser.AddLikedFeed(feedId, userID);
        }

        public DBResult UpdateLikedFeeds(List<int> feedIds, int userID)
        {
            return _dbConnectionUser.UpdateLikedFeeds(feedIds, userID);
        }

        public DBResult CheckIfFeedLiked(int feedId, int userId)
        {
            return _dbConnectionUser.CheckIfFeedLiked(feedId, userId);
        }

        public async Task<List<Feed>> LoadLikedFeeds(int userID, string filter = "", int count = 2000, OrderByFeeds? orderBy = null, bool active = true)
        {
            List<FeedDTO> feedDTOs = new();

            feedDTOs = await _dbConnectionUser.LoadLikedFeedsAsync(userID, filter, count, orderBy, active);

            var feeds = new FeedService().ConvertToDomainClass(feedDTOs);
            return feeds;
        }

        public DBResult RemoveLikedFeed(int feedId, int userID)
        {
            return _dbConnectionUser.RemoveLikedFeed(feedId, userID);
        }
        #endregion

        #region Liked Reviews
        public DBResult AddLikedReview(int reviewId, int userID)
        {
            return _dbConnectionUser.AddLikedReview(reviewId, userID);
        }

        public DBResult UpdateLikedReviews(List<int> reviewIds, int userID)
        {
            return _dbConnectionUser.UpdateLikedReviews(reviewIds, userID);
        }

        public List<Review> LoadLikedReviews(int userID, int count = 100, OrderByReviews? orderBy = null, int feedID = 0, bool active = true)
        {
            return ReviewService.ConvertToDomainClass(_dbConnectionUser.LoadLikedReviews(userID, count, orderBy, feedID, active));
        }

        public DBResult RemoveLikedReview(int reviewId, int userID)
        {
            return _dbConnectionUser.RemoveLikedReview(reviewId, userID);
        }

        public List<int> LoadLikedUsers(int userID, int count = 100)
        {
            return _dbConnectionUser.LoadLikedUsers(userID, count);
        }
        #endregion

        #region Convert Methods
        public static UserDTO ConvertToDTO(User user)
        {
            UserDTO userDTO = new();
            userDTO.Id = user.Id;
            userDTO.Name = user.Name.Trim();
            userDTO.Username = user.Username.Trim();
            userDTO.Email = user.Email.Trim();
            userDTO.HashedPassword = user.HashedPassword.Trim();
            userDTO.Notes = user.Notes.Trim();
            userDTO.Role = user.Role.Trim();
            userDTO.Categories = user.Categories;
            userDTO.Reviews = ReviewService.ConvertToDTO(user.Reviews);

            return userDTO;
        }

        public static List<UserDTO> ConvertToDTO(List<User> users)
        {
            if (users == null || users.Count == 0) return new();

            List<UserDTO> listUserDTOs = new();

            foreach (var user in users)
            {
                listUserDTOs.Add(ConvertToDTO(user));
            }

            return listUserDTOs;
        }

        public static User? ConvertToDomainClass(UserDTO? userDTO)
        {
            if (userDTO == null) return null;

            return new User(
                userDTO.Id,
                userDTO.Name.Trim(),
                userDTO.Username.Trim(),
                userDTO.Email.Trim(),
                userDTO.HashedPassword.Trim(),
                userDTO.Notes.Trim(),
                userDTO.Role.Trim(),
                userDTO.Categories,
                ReviewService.ConvertToDomainClass(userDTO.Reviews)
                );
        }

        public static List<User> ConvertToDomainClass(List<UserDTO> userDTOs)
        {
            if (userDTOs == null || userDTOs.Count == 0) return new();

            List<User> listUsers = new();

            foreach (var userDTO in userDTOs)
            {
                listUsers.Add(ConvertToDomainClass(userDTO));
            }

            return listUsers;
        }
        #endregion
    }
}
