namespace UniVoting.Data
{
	public class ConnectionHelper
	{
		public string Connection;

		public ConnectionHelper(string connectionString)
		{
			Connection = connectionString;
		}
	}
}