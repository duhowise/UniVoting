using System.Configuration;

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