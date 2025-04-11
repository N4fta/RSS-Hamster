using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Data.DTOs;
using static System.Net.Mime.MediaTypeNames;
using Data.Interfaces;

namespace Data.DatabaseConnections
{
    public class DbConnectionReviews : DbConnection, IDbConnectionReviews
    {
        public DBResult InsertReview(ReviewDTO reviewDTO)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;

            // ReviewDTO may contain empty User and Feed IDs since they are not needed in Update,
            // this is a safeguard
            if (reviewDTO.FeedID == 0 || reviewDTO.UserID == 0)
            {
                return new DBResult(false, $"FeedID or UserID invalid");
            }
            // If Id is 0 it means its a newly created Feed and I let the DB create one
            if (reviewDTO.Id != 0)
            {
                cmd.CommandText = @"SET IDENTITY_INSERT [Review] ON
                                  INSERT INTO Review (ID, Title, MainBody, FeedID, UserID)
                                 VALUES(@param0,@param1,@param2,@param3,@param4);";
                cmd.Parameters.AddWithValue("@param0", reviewDTO.Id);
            }
            else
            {
                cmd.CommandText = @"INSERT INTO Review (Title, MainBody, FeedID, UserID)
                            VALUES(@param1,@param2,@param3,@param4);";
            }

            cmd.Parameters.AddWithValue("@param1", reviewDTO.Title);
            cmd.Parameters.AddWithValue("@param2", reviewDTO.MainBody);
            cmd.Parameters.AddWithValue("@param3", reviewDTO.FeedID);
            cmd.Parameters.AddWithValue("@param4", reviewDTO.UserID);

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

        public List<ReviewDTO> LoadReviews(int count = 100, OrderByReviews? orderBy = null, int userID = 0, int feedID = 0, bool active = true)
        {
            List<ReviewDTO> listReviews = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT TOP ({count}) ID, Title, MainBody, FeedID, UserID, (SELECT COUNT(*) FROM User_LikedReviews WHERE ReviewID=[Review].ID) as Likes " +
                             "FROM [Review] WHERE Active=1 ";
            if (userID != 0)
            {
                cmd.CommandText+=" AND UserID = @param1 ";
                cmd.Parameters.AddWithValue("@param1", userID);
            }
            if (feedID != 0)
            {
                cmd.CommandText += " AND FeedID = @param2 ";
                cmd.Parameters.AddWithValue("@param2", feedID);
            }

            if (active) cmd.CommandText += " AND [Review].Active=1 ";
            else cmd.CommandText += " AND [Review].Active=0 ";

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

        public DBResult UpdateReview(ReviewDTO reviewDTO)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Review SET Title = @param1, MainBody = @param2
                                WHERE ID=@param0";

            cmd.Parameters.AddWithValue("@param0", reviewDTO.Id);
            cmd.Parameters.AddWithValue("@param1", reviewDTO.Title);
            cmd.Parameters.AddWithValue("@param2", reviewDTO.MainBody);

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

        public DBResult DeleteReview(int reviewID)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Review SET Active = 0 WHERE ID=@param1";

            cmd.Parameters.AddWithValue("@param1", reviewID);

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

    }
}
