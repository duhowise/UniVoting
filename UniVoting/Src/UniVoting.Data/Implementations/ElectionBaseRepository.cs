using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class ElectionBaseRepository<T> :IRepository<T> where T: class 
	{
		
		public  IEnumerable<T> GetAll()
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
		public async Task<IEnumerable<T>> GetAllAsync()
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

		public T Insert()
		{
			throw new NotImplementedException();
		}

		public Task<T> InsertAsync(T member)
		{
			throw new NotImplementedException();
		}

		public T GetById(int member)
		{
			throw new NotImplementedException();
		}
//todo fix code
		//public async Task<IEnumerable<Candidate>> GetAllCandidates()
		//{
		//	var candidates=new List<Candidate>();
		//	using (var connection = new SqlConnection(new ConnectionHelper().Connection))
		//	{
		//		if (connection.State != ConnectionState.Open)
		//		{
				   
		//		  await  connection.OpenAsync();

		//		}
		//		candidates=	(List<Candidate>) await connection.GetListAsync<Candidate>();
		//		foreach (var candidate in candidates)
		//		{
		//			candidate.Position = await GetByIdAsync<Position>(Convert.ToInt32(candidate.PositionId));
		//		}
		//	}

		//	return candidates;
		//}
	   
			//todo fix code
		//public async Task<T> Insert(T member)
		//{
		//	using (var connection = new SqlConnection(new ConnectionHelper().Connection))
		//	{
		//		if (connection.State != ConnectionState.Open)
		//		{
		//			await connection.OpenAsync();

		//		}
		//		var insert = Convert.ToInt32(await connection.InsertAsync(member));
		//		 return await GetByIdAsync<T>(insert);
		//	}

		//}
		public async Task Insert( Setting setting)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
				await connection.ExecuteAsync(@"UPDATE dbo.Settings SET  ElectionName = @ElectionName ,EletionSubTitle = @EletionSubTitle ,logo = @logo  ,Colour = @Colour WHERE  id = 1", setting);
			}

		}
		public async Task<Voter> GetVoterPass(Voter member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					await connection.OpenAsync();

				}
			return	await connection.QueryFirstOrDefaultAsync<Voter>(@"SELECT * FROM dbo.Voter WHERE IndexNumber=@IndexNumber", member);
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
		}
		public async Task<Comissioner> LoginAdmin(Comissioner comissioner )
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
		public IEnumerable<Position> GetPositionsWithDetails()
		{
			try
			{
				IEnumerable<Position> positions = new List<Position>();
				using (var connection = new SqlConnection(new ConnectionHelper().Connection))
				{
					if (connection.State != ConnectionState.Open)
					{
						 connection.Open();

					}
					positions = connection.Query<Position>(@"SELECT * FROM Position p  ORDER BY p.ID DESC");
					foreach (var position in positions)
					{
						position.Candidates = connection.Query<Candidate>(@"SELECT  ID ,PositionID ,CandidateName
						,CandidatePicture,RankId FROM dbo.Candidate C WHERE c.PositionID=@Id ORDER BY C.RankId ASC", position);
					}
				}
				return positions;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		  
		}
		public async Task<IEnumerable<Position>> GetPositionsWithDetailsAsync()
		{
			try
			{
				IEnumerable<Position> positions = new List<Position>();
				using (var connection = new SqlConnection(new ConnectionHelper().Connection))
				{
					if (connection.State != ConnectionState.Open)
					{
						await connection.OpenAsync();

					}
					positions = await connection.QueryAsync<Position>(@"SELECT * FROM Position p  ORDER BY p.ID DESC");
					foreach (var position in positions)
					{
						position.Candidates = await connection.QueryAsync<Candidate>(@"SELECT  ID ,PositionID ,CandidateName
						,CandidatePicture,RankId FROM dbo.Candidate C WHERE c.PositionID=@Id ORDER BY C.RankId ASC", position);
					}
				}
				return positions;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		  
		}
		public async Task<IEnumerable<Position>> GetPositionsAsync()
		{
			try
			{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
				{
					if (connection.State != ConnectionState.Open)
					{
						await connection.OpenAsync();

					}
				return	 await connection.QueryAsync<Position>(@"SELECT * FROM Position p  ORDER BY p.ID ASC");
					
				}
				
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		  
		}

		public  async Task<int> VoteSkipCount(Position position)
		{
			try
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
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			} 
			
		   
		}
		public   Setting ConfigureElection()
		{
			try
			{
				using (var connection = new SqlConnection(new ConnectionHelper().Connection))
				{
					if (connection.State != ConnectionState.Open)
					{
						 connection.Open();

					}
					return connection.QueryFirstOrDefault<Setting>(@"SELECT TOP 1  s.id ,s.ElectionName ,s.EletionSubTitle ,s.logo,s.Colour FROM Settings s WHERE s.id=1");

				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			} 
			
		   
		}
		//public async Task GetById(int member)
		//{
		//	using (var connection = new SqlConnection(new ConnectionHelper().Connection))
		//	{
		//		if (connection.State != ConnectionState.Open)
		//		{
		//		await	connection.OpenAsync();

		//		}
		//		return await connection.GetAsync(member);
		//	}
		//}
		public async Task<T> GetByIdAsync(int member)
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

		public T Update(T member)
		{
			throw new NotImplementedException();
		}

		public Task<T> UpdateAsync(T member)
		{
			throw new NotImplementedException();
		}

		//ToDo move to candidate repository
		//public async Task<Candidate> Update(T member)
		//{
		//	using (var connection = new SqlConnection(new ConnectionHelper().Connection))
		//	{
		//		if (connection.State != ConnectionState.Open)
		//		{
		//		await	connection.OpenAsync();

		//		}
		//		var id= Convert.ToInt32(await connection.UpdateAsync(member));
		//		return await GetByIdAsync<Candidate>(id);
		//	}
		//}
		public async Task ResetVoter(Voter member)
		{
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
					var voter= await connection.QueryFirstOrDefaultAsync<Voter>(@"SELECT  ID ,VoterName ,VoterCode ,IndexNumber ,Voted ,VoteInProgress FROM dbo.Voter WHERE IndexNumber=@IndexNumber",member);
					await connection.ExecuteAsync(@"DELETE FROM SkippedVotes WHERE VoterId=@Id", voter);
					await connection.ExecuteAsync(@"DELETE FROM Vote WHERE VoterId=@Id", voter);
					await connection.ExecuteAsync(@"UPDATE Voter SET VoteInProgress=0,Voted=0 WHERE IndexNumber=@IndexNumber", voter);
					transaction.Complete();
					}
					catch (Exception)
					{
						// ignored
					}

				}



			}
		}
		public async Task<int> InsertBulkVotes(List<Vote> votes,Voter voter,List<SkippedVotes> skippedVotes)
		{
			var task = 0;
			using (var transaction=new TransactionScope())
			{
				try
				{
					using (var connection = new SqlConnection(new ConnectionHelper().Connection))
					{
						if (connection.State != ConnectionState.Open)
						{
							await connection.OpenAsync();

						}
						//save votes
						task=(int) await connection.ExecuteAsync(@"INSERT INTO dbo.Vote(VoterID ,CandidateID ,PositionID)VALUES(@VoterID,@CandidateID,@PositionID)", votes);
						//save skipped
						await connection.ExecuteAsync(@"INSERT INTO dbo.SkippedVotes(Positionid,VoterId)VALUES(@Positionid,@VoterId)", skippedVotes);
						//update voter					
						await connection.ExecuteAsync(@"UPDATE Voter SET VoteInProgress=0,Voted=1 WHERE ID=@Id", voter);

						transaction.Complete();
					}
				}
				catch (Exception)
				{
					// ignored
				await ResetVoter(voter);
				}
			}
			return task;
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
					count=await connection.ExecuteAsync(@"INSERT INTO dbo.Voter(VoterName ,VoterCode ,IndexNumber) VALUES(@VoterName,@VoterCode,@IndexNumber)", member);
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
		public  void Delete(T member)
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

		public Task DeleteAsync(T member)
		{
			throw new NotImplementedException();
		}
	}
}