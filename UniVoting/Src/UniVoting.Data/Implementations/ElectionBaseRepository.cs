using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class ElectionBaseRepository
	{
		
		public  IEnumerable<T> GetAll<T>()
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.OpenAsync();

				}
				return connection.GetList<T>();
			}

		}
		public async Task<IEnumerable<T>> GetAllAsync<T>()
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
				   
				  await  connection.OpenAsync();

				}
			return await connection.GetListAsync<T>();
			}

		}
	   
		public async Task<T> Insert<T>(T member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				var insert = Convert.ToInt32(await connection.InsertAsync(member));
				 return await GetByIdAsync<T>(insert);
			}

		}

		public  async Task<int> VoteCount(Position position)
		{
		   
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				return await connection.ExecuteScalarAsync<int>(@"SELECT [Count] FROM dbo.vw_LiveView
										WHERE PositionName = @positionName", position);
			  
			}
		   
		}
		
		public async Task<Voter> Login(Voter voter )
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				return await connection.QueryFirstOrDefaultAsync<Voter>(@"SELECT  ID ,VoterName,VoterCode,IndexNumber,Voted ,VoteInProgress
						FROM dbo.Voter v WHERE v.VoterCode=@VoterCode", voter);
			}
		}
		public async Task<Comissioner> Login(Comissioner comissioner )
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				return await connection.QueryFirstOrDefaultAsync<Comissioner>(@"select * FROM Comissioner c  WHERE   c.Username=@Username AND c.Password=@Password", comissioner);
			}
		}
		public async Task<Comissioner> LoginPresident(Comissioner comissioner )
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				return await connection.QueryFirstOrDefaultAsync<Comissioner>(@"select * FROM Comissioner c  WHERE   c.Username=@Username AND c.Password=@Password  AND c.IsPresident =1", comissioner);
			}
		}public async Task<Comissioner> LoginAdmin(Comissioner comissioner )
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				return await connection.QueryFirstOrDefaultAsync<Comissioner>(@"select * FROM Comissioner c  WHERE   c.Username=@Username AND c.Password=@Password  AND c.isAdmin=1", comissioner);
			}
		}
		public async Task<Comissioner> LoginChairman(Comissioner comissioner )
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				return await connection.QueryFirstOrDefaultAsync<Comissioner>(@"select * FROM Comissioner c  WHERE   c.Username=@Username AND c.Password=@Password AND c.IsChairman=1", comissioner);
			}
		}
		public  IEnumerable<Position> GetPositionsWithDetails()
		{
		   IEnumerable<Position>positions=new List<Position>();
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.OpenAsync();

				}
				positions = connection.GetList<Position>();
				foreach ( var position in positions)
				{
					position.Candidates = connection.Query<Candidate>(@"SELECT  ID ,PositionID ,CandidateName ,CandidatePicture
					 ,RankId FROM dbo.Candidate C WHERE c.PositionID=@Id", position);
				}
			}
			return positions ;
		}

		public  async Task<int> VoteSkipCount(Position position)
		{
		   
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				return await connection.ExecuteScalarAsync<int>(@"SELECT [Count] FROM dbo.vw_LiveViewSkipped
										WHERE PositionName = @positionName", position);
			   
			}
		   
		}
		public async Task<T> GetById<T>(int member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
				await	connection.OpenAsync();

				}
				return await connection.GetAsync<T>(member);
			}
		}
		public async Task<T> GetByIdAsync<T>(int member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
				 await   connection.OpenAsync();

				}
				return await connection.GetAsync<T>(member);
			}
		}
		public async Task<Candidate> Update<T>(T member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
				await	connection.OpenAsync();

				}
				var id= Convert.ToInt32(await connection.UpdateAsync(member));
				return await GetByIdAsync<Candidate>(id);
			}
		}
		public  int InsertBulk<T>(List<T> member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.OpenAsync();

				}
				return(int)connection.Execute(@"INSERT INTO dbo.Vote(  VoterID ,CandidateID ,PositionID)VALUES(@VoterID,@CandidateID,@PositionID)", member);
			}
		}
		public async Task<int> InsertBulk(List<Voter> member)
		{
			int count = 0;
			using (var transaction = new TransactionScope())
			{
				using (var connection = new SqlConnection(new ConnectionHelper().Connection))
				{
					if (connection.State != ConnectionState.Open)
					{
						await connection.OpenAsync();

					}
					try
					{
					count=await connection.ExecuteAsync(@"INSERT INTO dbo.Voter(  VoterName ,VoterCode ,IndexNumber) VALUES(@VoterName,@VoterCode,@IndexNumber)", member);
						transaction.Complete();
					}
					catch (Exception)
					{
						// ignored
					}
					
				}



			}

			return count;
		}
		public  void Delete<T>(T member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.OpenAsync();

				}
				connection.Delete(member);
			}
		}
	}
}