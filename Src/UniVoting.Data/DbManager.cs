using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace UniVoting.Data
{
	/// <inheritdoc />
	/// <summary>
	/// A simple database connection manager
	/// </summary>
	public class DbManager : IDisposable
	{
		private IDbConnection _conn { get; set; }
		
		/// <summary>
		/// Return open connection
		/// </summary>
		public IDbConnection Connection
		{
			get
			{
				if (_conn.State == ConnectionState.Closed)
					_conn.Open();

				return _conn;
			}
		}

		

		/// <summary>
		/// Create a new Sql database connection
		/// </summary>
		/// <param name="connString">The raw connection string</param>
		public DbManager(string connString)
		{
		   _conn = new SqlConnection(connString);
		}

		/// <inheritdoc />
		/// <summary>
		/// Close and dispose of the database connection
		/// </summary>
		public void Dispose()
		{
			if (_conn != null)
			{
				if (_conn.State == ConnectionState.Open)
				{
					_conn.Close();
					_conn.Dispose();
				}
				_conn = null;
			}
		}
	}

}
