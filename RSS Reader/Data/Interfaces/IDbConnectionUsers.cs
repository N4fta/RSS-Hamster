using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDbConnectionUsers
    {
        // User CRUD
        DBResult InsertUser(UserDTO userDTO);

        UserDTO? LoadUser(string email, bool active = true);

        List<UserDTO> LoadUsers(string email = "", string username = "", string role = "", int count = 100, OrderByUsers? orderBy = null, bool active = true);

        DBResult UpdateUser(UserDTO userDTO);

        DBResult DeleteUser(int userID);

        // User Checks
        DBResult CheckIfUserExists(string email, string username);

        DBResult CheckIfFeedLiked(int feedId, int userId);

        // Categories
        List<string> LoadUserCategories(int userID, int count = 100, OrderByCategories? orderBy = null, bool active = true);

        DBResult UpdateUserCategory(List<string> categories, int userID);

        // Liked Feeds
        DBResult AddLikedFeed(int feedId, int userID);

        DBResult UpdateLikedFeeds(List<int> feedIds, int userID);

        Task<List<FeedDTO>> LoadLikedFeedsAsync(int userID, string filter = "", int count = 100, OrderByFeeds? orderBy = null, bool active = true);

        DBResult RemoveLikedFeed(int feedId, int userID);

        // Reviews
        DBResult AddLikedReview(int reviewId, int userID);

        DBResult UpdateLikedReviews(List<int> reviewIds, int userID);

        List<ReviewDTO> LoadLikedReviews(int userID, int count = 100, OrderByReviews? orderBy = null, int feedID = 0, bool active = true);

        DBResult RemoveLikedReview(int reviewId, int userID);

        // Algorithm
        List<int> LoadLikedUsers(int userID, int count = 100);
    }
}
