using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UniVoting.Data
{
    public class ConnectionHelper
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["VotingSystem"].ConnectionString;
        public IDbConnection Connection;

        public ConnectionHelper()
        {
            if (Connection == null)
            { Connection = new SqlConnection(_connectionString); }
        }


        public void State()
        {
            if (Connection.State==ConnectionState.Closed)
                Connection.Open();
            
        }
    }
}