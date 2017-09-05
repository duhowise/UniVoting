using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace UniVoting.Data
{
	public class ConnectionHelper
	{
	   
		public string Connection;

		public ConnectionHelper()
		{

			Connection = ConfigurationManager.ConnectionStrings["VotingSystem"].ConnectionString;
			

		}

		

	}


}