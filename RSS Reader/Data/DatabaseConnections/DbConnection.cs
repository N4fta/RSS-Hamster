using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseConnections
{
    public abstract class DbConnection
    {
        private readonly string connectionString = "Server=mssqlstud.fhict.local;Database=dbi540432_feeds;User Id=dbi540432_feeds;Password=FeedsAreTheGoat;";

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
