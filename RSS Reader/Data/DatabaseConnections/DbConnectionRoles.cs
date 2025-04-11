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
    public class DbConnectionRoles : DbConnection, IDbConnectionRoles
    {
        public DBResult InsertRole(string role)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"INSERT INTO Role (Role)
                            VALUES(@param1)";

                cmd.Parameters.AddWithValue("@param1", role);

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

        public List<string> LoadRoles()
        {
            List<string> roles = new();

            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Role FROM [Role] WHERE Active = 1 ORDER BY [id];";

                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(reader.GetString(0).Trim());
                        }
                    }

                }
                catch (SqlException ex)
                {
                    return null;
                }
            }
            return roles;
        }

        public DBResult DeleteRole(string role)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"UPDATE Role SET Active = 0 WHERE Role=@param1";

                cmd.Parameters.AddWithValue("@param1", role);

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
}
