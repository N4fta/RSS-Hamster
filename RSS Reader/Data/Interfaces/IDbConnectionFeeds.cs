using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Interfaces
{
    public interface IDbConnectionFeeds
    {
        // Feed CRUD
        DBResult InsertFeed(FeedDTO feedDTO);

        Task<FeedDTO> LoadFeedAsync(int feedID, bool active = true);

        Task<List<FeedDTO>> LoadFeedsAsync(string filter = "", int count = 100, OrderByFeeds? orderBy = null, bool active = true);
        Task<int> CountFeeds(string filter = "", bool active = true);

        DBResult UpdateFeed(FeedDTO feedDTO);

        DBResult DeleteFeed(int feedID);

        // Checks
        DBResult CheckIfFeedExists(string source, string name);

        // Categories
        List<string> LoadFeedCategories(int feedID, int count = 100, OrderByCategories? orderBy = null, bool active = true);

        DBResult UpdateFeedCategory(List<string> categories, int feedID);

        // Categories
        List<ReviewDTO> LoadFeedReviews(int feedID, int count = 100, OrderByReviews? orderBy = null, bool active = true);

        // Algorithm Calls
        List<FeedDTO> LoadFeedsAlgo(int count = 100, List<string>? categoriesFilters = null, OrderByFeeds? orderBy = null, bool active = true);
    }
}
