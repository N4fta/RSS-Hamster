using System.Data.SqlClient;
using System.Data;
using Data.Interfaces;
using System.Reflection.PortableExecutable;
using System.Xml.Linq;
using Data.DTOs;

namespace Data.DatabaseConnections
{
    public class DbConnectionUsers : DbConnection, IDbConnectionUsers
    {
        // User CRUD
        #region User CRUD
        public DBResult InsertUser(UserDTO userDTO)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            if (userDTO.Id != 0)
            {
                cmd.CommandText = @"SET IDENTITY_INSERT [User] on
                                    INSERT INTO [User] (Id, Name, Username, Email, Password, Notes, RoleID)
                                    VALUES(@param0, @param1, @param2, @param3, @param4, @param5, (SELECT (ID) FROM [Role] WHERE Role=@param6));";
                cmd.Parameters.AddWithValue("@param0", userDTO.Id);
            }
            else
            {
                cmd.CommandText = @"INSERT INTO [User] (Name, Username, Email, Password, Notes, RoleID)
                                  VALUES(@param1,@param2,@param3,@param4,@param5,(SELECT (ID) FROM [Role] WHERE Role=@param6));";
            }

            cmd.Parameters.AddWithValue("@param1", userDTO.Name);
            cmd.Parameters.AddWithValue("@param2", userDTO.Username);
            cmd.Parameters.AddWithValue("@param3", userDTO.Email);
            cmd.Parameters.AddWithValue("@param4", userDTO.HashedPassword);
            cmd.Parameters.AddWithValue("@param5", userDTO.Notes);
            cmd.Parameters.AddWithValue("@param6", userDTO.Role);

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

        //Loading
        #region Load
        public UserDTO? LoadUser(string email, bool active = true)
        {
            UserDTO? userDTO = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT [User].ID, Name, Username, Email, Password, Notes, Role , Category.Category FROM [User] " +
                "INNER JOIN Role ON [User].RoleID = Role.ID " +
                "LEFT JOIN User_LikedCategories on User_LikedCategories.UserID = [User].ID " +
                "LEFT JOIN (SELECT ID, Category From Category Where Category.Active = 1) Category on User_LikedCategories.CategoryID = Category.ID " +
                @" WHERE [User].Active=@param0 AND Email=@param1";

            int activeInt = 0;
            if (active) activeInt = 1;
            cmd.Parameters.AddWithValue("@param0", activeInt);
            cmd.Parameters.AddWithValue("@param1", email);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();

                List<string> categories = new();
                if (reader.Read())
                {
                    userDTO = GetUserDTO(reader);

                    if (!reader.IsDBNull(7)) categories.Add(GetCategory(reader));
                }
                else
                {
                    // No User found
                    return null;
                }

                // Categories
                while (reader.Read())
                {
                    if (!reader.IsDBNull(7)) categories.Add(GetCategory(reader));
                }
                userDTO.Categories = categories;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return userDTO;
        }

        public List<UserDTO> LoadUsers(string email = "", string username = "", string role = "", int count = 100, OrderByUsers? orderBy = OrderByUsers.Name, bool active = true)
        {
            List<UserDTO> users = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = $"SELECT TOP ({count}) [User].ID, Name, Username, Email, Password, Notes, Role , Category.Category FROM [User] " +
            "INNER JOIN Role ON [User].RoleID = Role.ID " +
            "LEFT JOIN User_LikedCategories on User_LikedCategories.UserID = [User].ID " +
            "LEFT JOIN (SELECT ID, Category From Category Where Category.Active = 1) Category on User_LikedCategories.CategoryID = Category.ID " +
            $"WHERE Email LIKE '%{email}%' and Username LIKE '%{username}%' and Role LIKE '%{role}%'";

            if (active) cmd.CommandText += " AND [User].Active=1 ";
            else cmd.CommandText += " AND [User].Active=0 ";


            if (orderBy.HasValue)
            {
                cmd.CommandText += $" ORDER BY [User].{orderBy.Value} ";
            }

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();

                UserDTO? currentUser = null;
                List<string> categories = new();

                while (reader.Read())
                {
                    // First entry
                    if (currentUser == null)
                    {
                        currentUser = GetUserDTO(reader);
                        if (!reader.IsDBNull(7)) categories.Add(GetCategory(reader));
                        continue;
                    }

                    // Same user as last cycle
                    if (currentUser.Id == reader.GetInt32(0))
                    {
                        if (!reader.IsDBNull(7)) categories.Add(GetCategory(reader));
                    }
                    // New user
                    else
                    {
                        // Add user to list and reset categories
                        currentUser.Categories = categories;
                        users.Add(currentUser);
                        categories = new();

                        currentUser = GetUserDTO(reader);

                        if (!reader.IsDBNull(7)) categories.Add(GetCategory(reader));
                    }
                }
                currentUser.Categories = categories;
                users.Add(currentUser);

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return users;
        }

        private UserDTO GetUserDTO(SqlDataReader reader)
        {
            UserDTO userDTO = new();

            userDTO.Id = reader.GetInt32(0);
            userDTO.Name = reader.GetString(1);
            userDTO.Username = reader.GetString(2);
            userDTO.Email = reader.GetString(3);
            userDTO.HashedPassword = reader.GetString(4);
            userDTO.Notes = reader.GetString(5);
            userDTO.Role = reader.GetString(6);

            return userDTO;
        }
        private string GetCategory(SqlDataReader reader)
        {
            return reader.GetString(7).Trim();
        }
        #endregion

        public DBResult UpdateUser(UserDTO userDTO)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE [User] SET Name = @param1, Username = @param2, Email = @param3, Password = @param4, Notes = @param5, RoleID = (SELECT (ID) FROM [Role] WHERE Role=@param6) " +
                            "WHERE ID=@param0";

            cmd.Parameters.AddWithValue("@param0", userDTO.Id);
            cmd.Parameters.AddWithValue("@param1", userDTO.Name);
            cmd.Parameters.AddWithValue("@param2", userDTO.Username);
            cmd.Parameters.AddWithValue("@param3", userDTO.Email);
            cmd.Parameters.AddWithValue("@param4", userDTO.HashedPassword);
            cmd.Parameters.AddWithValue("@param5", userDTO.Notes);
            cmd.Parameters.AddWithValue("@param6", userDTO.Role);

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

        public DBResult DeleteUser(int userID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE [User] SET Active=0 WHERE ID=@param1";

            cmd.Parameters.AddWithValue("@param1", userID);

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
        #endregion

        // User Checks
        public DBResult CheckIfUserExists(string email, string username)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT " +
                "(SELECT COUNT(1) FROM User WHERE Email=@param1) " +
                "+ 2*(SELECT COUNT(1) FROM User WHERE Username=@param2)";
            // Table of results
            // 0 = neither are taken
            // 1 = Email is taken
            // 2 = Username is taken
            // 3 = Both are taken

            cmd.Parameters.AddWithValue("@param1", email);
            cmd.Parameters.AddWithValue("@param2", username);

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
                    return new DBResult(true, $"Email is taken");
                }
                if (count == 2)
                {
                    return new DBResult(true, $"Username is taken");
                }
                if (count == 3)
                {
                    return new DBResult(true, $"Both Email and Username are Taken");
                }
                // No Sql Exception but either too many or too little rows were affected
                return new DBResult(true, $"Result unclear: {count}");
            }
            catch (SqlException ex)
            {
                return new DBResult(true, ex.Message, ex);
            }
        }

        public DBResult CheckIfFeedLiked(int feedId, int userId)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT COUNT(1) FROM User_LikedFeeds WHERE FeedID=@param1 and UserID=@param2";

            cmd.Parameters.AddWithValue("@param1", feedId);
            cmd.Parameters.AddWithValue("@param2", userId);

            try
            {
                conn.Open();
                // Count returns either 1 or 0
                int count = (int)cmd.ExecuteScalar();
                if (count == 1)
                {
                    return new DBResult(true, $"Feed liked");
                }
                return new DBResult(false, $"Feed not liked");
            }
            catch (SqlException ex)
            {
                return new DBResult(false, ex.Message, ex);
            }
        }

        // Categories
        #region Categories
        public List<string> LoadUserCategories(int userID, int count = 100, OrderByCategories? orderBy = OrderByCategories.Category, bool active = true)
        {
            List<string> categories = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT TOP ({count}) Category FROM [Category] ORDER BY [id] " +
                " INNER JOIN User_LikedCategories on CategoryID=ID " +
                " WHERE UserID = @param1 ";

            cmd.Parameters.AddWithValue("@param1", userID);

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

        public DBResult UpdateUserCategory(List<string> categories, int userID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "BEGIN " +
                $"DELETE FROM User_LikedCategories WHERE UserID = {userID} ";

            foreach (var category in categories)
            {
                cmd.CommandText += "INSERT INTO User_LikedCategories (UserID, CategoryID) " +
                    $"VALUES ({userID}, (SELECT ID FROM Category WHERE Category='{category}')) ";
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
        #endregion

        // Liked Feeds
        #region Liked Feeds
        public DBResult AddLikedFeed(int feedId, int userID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText += "INSERT INTO User_LikedFeeds (UserID, FeedID) " +
                    $"VALUES ({userID},{feedId}) ";

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

        public DBResult UpdateLikedFeeds(List<int> feedIds, int userID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "BEGIN " +
                $"DELETE FROM User_LikedFeeds WHERE UserID = {userID} ";

            foreach (var feedId in feedIds)
            {
                cmd.CommandText += "INSERT INTO User_LikedFeeds (UserID, FeedID) " +
                    $"VALUES ({userID},{feedId}) ";
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

        public async Task<List<FeedDTO>> LoadLikedFeedsAsync(int userID, string filter = "", int count = 1000, OrderByFeeds? orderBy = OrderByFeeds.Name, bool active = true)
        {
            List<FeedDTO> feeds = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = $"SELECT TOP ({count}) Feed.ID, Source, Name, ItemParserID, ParserID, Category.Category FROM [Feed] " +
                "INNER JOIN User_LikedFeeds ON FeedID = ID " +
                "LEFT JOIN Feed_Category on Feed_Category.FeedID = Feed.ID " +
                "LEFT JOIN (SELECT ID, Category From Category Where Category.Active = 1) Category on Feed_Category.CategoryID = Category.ID " +
                $"Where Name LIKE '%{filter}%' and UserID={userID}";

            if (active) cmd.CommandText += " AND [Feed].Active=1 ";
            else cmd.CommandText += " AND [Feed].Active=0 ";

            if (orderBy.HasValue)
            {
                if (orderBy.Value == OrderByFeeds.Popularity) cmd.CommandText += " ORDER BY (SELECT Count(*) as Follows FROM User_LikedFeeds WHERE FeedID = Feed.ID) ";
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
                        if (!reader.IsDBNull(5)) categories.Add(GetCategoryFeed(reader));
                        continue;
                    }

                    // Same feed as last cycle
                    if (currentFeed.Id == reader.GetInt32(0))
                    {
                        if (!reader.IsDBNull(5)) categories.Add(GetCategoryFeed(reader));
                    }
                    // New feed
                    else
                    {
                        // Add feed to list and reset categories
                        currentFeed.Categories = categories;
                        feeds.Add(currentFeed);
                        categories = new();

                        currentFeed = GetFeedDTO(reader);

                        if (!reader.IsDBNull(5)) categories.Add(GetCategoryFeed(reader));
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
        private string GetCategoryFeed(SqlDataReader reader)
        {
            return reader.GetString(5).Trim();
        }

        public DBResult RemoveLikedFeed(int feedId, int userID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"DELETE FROM User_LikedFeeds WHERE UserID = {userID} and FeedID = {feedId}";

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
        #endregion

        // Liked Reviews
        #region Liked Reviews
        public DBResult AddLikedReview(int reviewId, int userID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText += "INSERT INTO User_LikedReviews (UserID, ReviewID) " +
                    $"VALUES ({userID},{reviewId}) ";

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

        public DBResult UpdateLikedReviews(List<int> reviewIds, int userID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "BEGIN " +
                $"DELETE FROM User_LikedReviews WHERE UserID = {userID} ";

            foreach (var reviewId in reviewIds)
            {
                cmd.CommandText += "INSERT INTO User_LikedReviews (UserID, ReviewID) " +
                    $"VALUES ({userID},{reviewId}) ";
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

        public List<ReviewDTO> LoadLikedReviews(int userID, int count = 100, OrderByReviews? orderBy = null, int feedID = 0, bool active = true)
        {
            List<ReviewDTO> listReviews = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Review.ID, Title, MainBody, FeedID, Review.UserID, (SELECT COUNT(*) FROM User_LikedReviews WHERE ReviewID=Review.ID) as Likes " +
                             "FROM Review " +
                             "INNER JOIN User_LikedReviews LikedReviews on ReviewID=Review.ID " +
                             "WHERE LikedReviews.UserID=@param1 ";

            cmd.Parameters.AddWithValue("@param1", userID);

            if (active) cmd.CommandText += " AND [Review].Active=1 ";
            else cmd.CommandText += " AND [Review].Active=0 ";

            if (feedID != 0)
            {
                cmd.CommandText += $" AND FeedID={feedID} ";
            }

            if (orderBy.HasValue)
            {
                cmd.CommandText += $" ORDER BY {orderBy.Value} ";
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
                throw new Exception(ex.Message, ex);
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

        public DBResult RemoveLikedReview(int reviewId, int userID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"DELETE FROM User_LikedReviews WHERE UserID = {userID} and ReviewID = {reviewId}";

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

        public List<int> LoadLikedUsers(int userID, int count = 100)
        {
            List<int> UserIDs = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"SELECT Review.UserID, COUNT (Review.UserID) as Likes
                                FROM Review
                                LEFT JOIN User_LikedReviews on ReviewID=Review.ID
                                Left Join [User] on [User].ID = Review.UserID
                                where User_LikedReviews.UserID = @param1
                                group by Review.UserID
                                order by Likes DESC";

            cmd.Parameters.AddWithValue("@param1", userID);

            try
            {
                conn.Open();
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Column Example:
                    // Whos review you liked and how many reviews
                    // UserID | Likes
                    // 1      | 5 
                    UserIDs.Add(reader.GetInt32(0));
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return UserIDs;
        }
        #endregion
    }
}
