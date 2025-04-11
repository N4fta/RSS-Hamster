using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DTOs;
using Data.Interfaces;
using System.ComponentModel.Design;

namespace Data.DatabaseConnections
{
    public class DbConnectionFeeds : DbConnection, IDbConnectionFeeds
    {
        // Feed CRUD
        public DBResult InsertFeed(FeedDTO feedDTO)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            // If Id is 0 it means its a newly created Feed and I let the DB generate one
            if (feedDTO.Id != 0)
            {
                cmd.CommandText = @"SET IDENTITY_INSERT [Feed] ON
                                    INSERT INTO Feed (ID, Source, Name, ItemParserID, ParserID)
                                    VALUES(@param0,@param1,@param2,@param3,@param4)";

                cmd.Parameters.AddWithValue("@param0", feedDTO.Id);
            }
            else
            {
                cmd.CommandText = @"INSERT INTO [Feed] (Source, Name, ItemParserID, ParserID)
                                    VALUES(@param1,@param2,@param3,@param4)";
            }

            cmd.Parameters.AddWithValue("@param1", feedDTO.Source);
            cmd.Parameters.AddWithValue("@param2", feedDTO.Name);
            cmd.Parameters.AddWithValue("@param3", feedDTO.ItemParserID);
            cmd.Parameters.AddWithValue("@param4", feedDTO.ParserID);

            try
            {
                conn.Open();
                // Checks number of affected rows and sends success if number equals 1
                int nbrRows = cmd.ExecuteNonQuery();
                if (nbrRows == 1)
                {
                    return new DBResult(true, $"{nbrRows} rows affected");
                }
                // No Sql Exception but either too many or too little rows were affected
                return new DBResult(false, $"{nbrRows} rows affected");
            }
            catch (SqlException ex)
            {
                return new DBResult(false, ex.Message, ex);
            }
        }

        public async Task<FeedDTO> LoadFeedAsync(int feedID, bool active = true)
        {
            FeedDTO feed = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT Feed.ID, Source, Name, ItemParserID, ParserID, Category.Category FROM [Feed]" +
                "LEFT JOIN Feed_Category on Feed_Category.FeedID = Feed.ID " +
                "LEFT JOIN (SELECT ID, Category From Category Where Category.Active = 1) Category on Feed_Category.CategoryID = Category.ID " +
                $"Where Feed.ID = {feedID} ";

            if (active) cmd.CommandText += " AND [Feed].Active=1 ";
            else cmd.CommandText += " AND [Feed].Active=0 ";

            try
            {
                conn.Open();
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                List<string> categories = new();

                if (reader.Read())
                {
                    feed = GetFeedDTO(reader);
                    if (!reader.IsDBNull(5)) categories.Add(GetCategory(reader));
                }
                else
                {
                    // No feed found
                    return null;
                }

                while (reader.Read())
                {
                    if (!reader.IsDBNull(5)) categories.Add(GetCategory(reader));
                }
                feed.Categories = categories;
            }
            catch (SqlException ex)
            {
                throw new Exception("Something went wrong", ex);
            }
            return feed;
        }

        public async Task<List<FeedDTO>> LoadFeedsAsync(string filter = "", int count = 2000, OrderByFeeds? orderBy = OrderByFeeds.Name, bool active = true)
        {
            List<FeedDTO> feeds = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = $"SELECT TOP ({count}) Feed.ID, Source, Name, ItemParserID, ParserID, Category.Category FROM [Feed]" +
                "LEFT JOIN Feed_Category on Feed_Category.FeedID = Feed.ID " +
                "LEFT JOIN (SELECT ID, Category From Category Where Category.Active = 1) Category on Feed_Category.CategoryID = Category.ID " +
                $"Where Name LIKE '%{filter}%'";

            if (active) cmd.CommandText += " AND [Feed].Active=1 ";
            else cmd.CommandText += " AND [Feed].Active=0 ";

            if (orderBy.HasValue)
            {
                if (orderBy.Value == OrderByFeeds.Popularity) cmd.CommandText += " ORDER BY (SELECT Count(*) as Follows FROM User_LikedFeeds WHERE FeedID = Feed.ID) DESC";
                else cmd.CommandText += $" ORDER BY [Feed].{orderBy.Value.ToString().Replace("_", " ")} ";
            }

            try
            {
                conn.Open();
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                FeedDTO? currentFeed = null;
                List<string> categories = new();

                while (reader.Read())
                {
                    // First entry
                    if (currentFeed == null)
                    {
                        currentFeed = GetFeedDTO(reader);
                        if (!reader.IsDBNull(5)) categories.Add(GetCategory(reader));
                        continue;
                    }

                    // Same feed as last cycle
                    if (currentFeed.Id == reader.GetInt32(0))
                    {
                        if (!reader.IsDBNull(5)) categories.Add(GetCategory(reader));
                    }
                    // New feed
                    else
                    {
                        // Add feed to list and reset categories
                        currentFeed.Categories = categories;
                        feeds.Add(currentFeed);
                        categories = new();

                        currentFeed = GetFeedDTO(reader);

                        if (!reader.IsDBNull(5)) categories.Add(GetCategory(reader));
                    }
                }
                if (currentFeed == null) return new();
                currentFeed.Categories = categories;
                feeds.Add(currentFeed);
            }
            catch (SqlException ex)
            {
                throw new Exception($"SqlException: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}", ex);

            }
            return feeds;
        }
        public async Task<int> CountFeeds(string filter = "", bool active = true)
        {
            int numberOfFeeds = 0;

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            int activeInt = 0;
            if (active) activeInt = 1;


            cmd.CommandText = "SELECT Count(*) FROM [Feed] " +
            $"Where Name LIKE '%{filter}%'";

            if (active) cmd.CommandText += " AND [Feed].Active=1 ";
            else cmd.CommandText += " AND [Feed].Active=0 ";



            try
            {
                conn.Open();
                numberOfFeeds = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
            catch (SqlException ex)
            {
                return 0;
            }
            return numberOfFeeds;
        }

        private FeedDTO GetFeedDTO(SqlDataReader reader)
        {
            FeedDTO feedDTO = new();
            feedDTO.Id = reader.GetInt32(0);
            feedDTO.Source = reader.GetString(1);
            feedDTO.Name = reader.GetString(2);
            feedDTO.ItemParserID = reader.GetInt32(3);
            feedDTO.ParserID = reader.GetInt32(4);

            return feedDTO;
        }
        private string GetCategory(SqlDataReader reader)
        {
            return reader.GetString(5).Trim();
        }

        public DBResult UpdateFeed(FeedDTO feedDTO)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Feed SET Source = @param1, Name = @param2, ItemParserID = @param3, ParserID = @param4
                                    WHERE ID=@param0";

            cmd.Parameters.AddWithValue("@param0", feedDTO.Id);
            cmd.Parameters.AddWithValue("@param1", feedDTO.Source);
            cmd.Parameters.AddWithValue("@param2", feedDTO.Name);
            cmd.Parameters.AddWithValue("@param3", feedDTO.ItemParserID);
            cmd.Parameters.AddWithValue("@param4", feedDTO.ParserID);

            try
            {
                conn.Open();
                // Checks number of affected rows and sends success if number equals 1
                int nbrRows = cmd.ExecuteNonQuery();
                if (nbrRows == 1)
                {
                    return new DBResult(true, $"{nbrRows} rows affected");
                }
                // No Sql Exception but either too many or too little rows were affected
                return new DBResult(false, $"{nbrRows} rows affected");
            }
            catch (SqlException ex)
            {
                return new DBResult(false, ex.Message, ex);
            }

        }

        public DBResult DeleteFeed(int feedID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Feed SET Active = 0 WHERE ID=@param1";

            cmd.Parameters.AddWithValue("@param1", feedID);

            try
            {
                conn.Open();
                // Checks number of affected rows and sends success if number equals 1
                int nbrRows = cmd.ExecuteNonQuery();
                if (nbrRows == 1)
                {
                    return new DBResult(true, $"{nbrRows} rows affected");
                }
                // No Sql Exception but either too many or too little rows were affected
                return new DBResult(false, $"{nbrRows} rows affected");
            }
            catch (SqlException ex)
            {
                return new DBResult(false, ex.Message, ex);
            }

        }

        // Checks
        public DBResult CheckIfFeedExists(string source, string name)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT " +
                "(SELECT COUNT(1) FROM Feed WHERE Source=@param1) " +
                "+ 2*(SELECT COUNT(1) FROM Feed WHERE Name=@param2)";
            // Table of results
            // 0 = neither are taken
            // 1 = Source is taken
            // 2 = Name is taken
            // 3 = Both are taken

            cmd.Parameters.AddWithValue("@param1", source);
            cmd.Parameters.AddWithValue("@param2", name);

            try
            {
                conn.Open();
                // Checks number of affected rows and sends success if number equals 1
                int count = (int)cmd.ExecuteScalar();
                if (count == 0)
                {
                    return new DBResult(false, $"Both fields are not in use");
                }
                if (count == 1)
                {
                    return new DBResult(true, $"Source is taken");
                }
                if (count == 2)
                {
                    return new DBResult(true, $"Name is taken");
                }
                if (count == 3)
                {
                    return new DBResult(true, $"Both Source and Name are Taken");
                }
                // No Sql Exception but either too many or too little rows were affected
                return new DBResult(true, $"Result unclear: {count}");
            }
            catch (SqlException ex)
            {
                return new DBResult(true, ex.Message, ex);
            }
        }

        // Categories
        public List<string> LoadFeedCategories(int feedID, int count = 100, OrderByCategories? orderBy = null, bool active = true)
        {
            List<string> categories = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT TOP ({count}) Category FROM [Category] " +
                " INNER JOIN Feed_Category on CategoryID=ID " +
                " WHERE FeedID = @param1 ";

            cmd.Parameters.AddWithValue("@param1", feedID);

            if (active) cmd.CommandText += " AND [Category].Active=1 ";
            else cmd.CommandText += " AND [Category].Active=0 ";

            if (orderBy.HasValue)
            {
                cmd.CommandText += $" ORDER BY [Category].{orderBy.Value} ";
            }

            try
            {
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(reader.GetString(0).Trim());
                }

            }
            catch (SqlException ex)
            {
                return null;
            }
            return categories;
        }
        public DBResult UpdateFeedCategory(List<string> categories, int feedID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "BEGIN " +
                $"DELETE FROM Feed_Category WHERE FeedID = {feedID} ";

            foreach (var category in categories)
            {
                cmd.CommandText += "INSERT INTO Feed_Category (FeedID, CategoryID) " +
                    $"VALUES ({feedID}, (SELECT ID FROM Category WHERE Category='{category}')) ";
            }

            cmd.CommandText += " END";

            try
            {
                conn.Open();

                int nbrRows = cmd.ExecuteNonQuery();

                return new DBResult(true, $"{nbrRows} rows affected");
            }
            catch (SqlException ex)
            {
                return new DBResult(false, ex.Message, ex);
            }
        }

        // Reviews
        public List<ReviewDTO> LoadFeedReviews(int feedID, int count = 100, OrderByReviews? orderBy = null, bool active = true)
        {
            List<ReviewDTO> listReviews = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT TOP ({count}) ID, Title, MainBody, FeedID, UserID, (SELECT COUNT(*) FROM User_LikedReviews WHERE ReviewID=Review.ID) as Likes " +
                " FROM Review WHERE [Review].Active=1 AND FeedID=@param1 ";

            cmd.Parameters.AddWithValue("@param1", feedID);

            if (active) cmd.CommandText += " AND [Review].Active=1 ";
            else cmd.CommandText += " AND [Review].Active=0 ";

            if (orderBy.HasValue)
            {
                cmd.CommandText += $" ORDER BY [Review].{orderBy.Value} ";
            }

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Review Column Example:
                    // ID | Title	| MainBody	                  | FeedID | UserID	| Likes
                    // 1  | Test    | This feed was extremely meh | 1      | 1      | 30
                    ReviewDTO review = GetReviewDTO(reader);

                    listReviews.Add(review);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Something went wrong", ex);
            }
            return listReviews;
        }
        private ReviewDTO GetReviewDTO(SqlDataReader reader)
        {
            ReviewDTO reviewDTO = new();
            reviewDTO.Id = reader.GetInt32(0);
            reviewDTO.Title = reader.GetString(1);
            reviewDTO.MainBody = reader.GetString(2);
            reviewDTO.FeedID = reader.GetInt32(3);
            reviewDTO.UserID = reader.GetInt32(4);
            reviewDTO.Likes = reader.GetInt32(5);

            return reviewDTO;
        }

        // Algorithm Call
        public List<FeedDTO> LoadFeedsAlgo(int count = 100, List<string>? categoriesFilters = null, OrderByFeeds? orderBy = OrderByFeeds.Popularity, bool active = true)
        {
            List<FeedDTO> feeds = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = $"SELECT TOP ({count}) Feed.ID, Source, Name, ItemParserID, ParserID, Category.Category FROM [Feed]" +
                "LEFT JOIN Feed_Category on Feed_Category.FeedID = Feed.ID " +
                "LEFT JOIN (SELECT ID, Category From Category Where Category.Active = 1) Category on Feed_Category.CategoryID = Category.ID ";

            if (active) cmd.CommandText += " Where [Feed].Active=1 ";
            else cmd.CommandText += " Where [Feed].Active=0 ";

            if (categoriesFilters != null && categoriesFilters.Count > 0)
            {
                cmd.CommandText += $" AND ";
                cmd.CommandText += $" Category.Category = '{string.Join("' OR Category.Category = '", categoriesFilters)}' ";
            }

            if (orderBy.HasValue)
            {
                if (orderBy.Value == OrderByFeeds.Popularity) cmd.CommandText += " ORDER BY (SELECT Count(*) as Follows FROM User_LikedFeeds WHERE FeedID = Feed.ID) DESC";
                else cmd.CommandText += $" ORDER BY [Feed].{orderBy.Value.ToString().Replace("_", " ")} ";
            }

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();

                FeedDTO? currentFeed = null;
                List<string> categories = new();

                while (reader.Read())
                {
                    // First entry
                    if (currentFeed == null)
                    {
                        currentFeed = GetFeedDTO(reader);
                        if (!reader.IsDBNull(5)) categories.Add(GetCategory(reader));
                        continue;
                    }

                    // Same feed as last cycle
                    if (currentFeed.Id == reader.GetInt32(0))
                    {
                        if (!reader.IsDBNull(5)) categories.Add(GetCategory(reader));
                    }
                    // New feed
                    else
                    {
                        // Add feed to list and reset categories
                        currentFeed.Categories = categories;
                        feeds.Add(currentFeed);
                        categories = new();

                        currentFeed = GetFeedDTO(reader);

                        if (!reader.IsDBNull(5)) categories.Add(GetCategory(reader));
                    }
                }
                if (currentFeed == null) return new();
                currentFeed.Categories = categories;
                feeds.Add(currentFeed);
            }
            catch (SqlException ex)
            {
                throw new Exception($"SqlException: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Exception: {ex.Message}", ex);

            }
            return feeds;
        }

    }
}
