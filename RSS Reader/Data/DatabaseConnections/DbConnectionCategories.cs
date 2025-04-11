using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Data.Interfaces;

namespace Data.DatabaseConnections
{
    public class DbConnectionCategories : DbConnection, IDbConnectionCategories
    {
        public DBResult InsertCategory(string category)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"INSERT INTO Category (Category)
                            VALUES(@param1)";

            cmd.Parameters.AddWithValue("@param1", category);

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

        public List<string> LoadCategories()
        {
            List<string> categories = new();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Category FROM [Category] WHERE Active = 1 ORDER BY Category;";

            try
            {
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(reader.GetString(0));
                }

            }
            catch (SqlException ex)
            {
                return null;
            }
            return categories;
        }

        public DBResult DeleteCategory(string category)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Category SET Active=0 WHERE Category=@param1";

            cmd.Parameters.AddWithValue("@param1", category);

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
