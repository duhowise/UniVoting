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
		public async Task<IEnumerable<Candidate>> GetAllCandidates()
		{
			var candidates=new List<Candidate>();
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
				   
				  await  connection.OpenAsync();

				}
				candidates=	(List<Candidate>) await connection.GetListAsync<Candidate>();
				foreach (var candidate in candidates)
				{
					candidate.Position = await GetByIdAsync<Position>(Convert.ToInt32(candidate.PositionId));
				}
			}

			return candidates;
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
		public   Setting ConfigureElection(int id)
		{
			try
			{
				using (var connection = new SqlConnection(new ConnectionHelper().Connection))
				{
					if (connection.State != ConnectionState.Open)
					{
						 connection.Open();

					}
					return connection.QueryFirstOrDefault<Setting>(@"SELECT TOP 1  s.id ,s.ElectionName ,s.EletionSubTitle ,s.logo,s.Colour FROM Settings s WHERE s.id=@id", new Setting {Id = id});

				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
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
					 await connection.ExecuteAsync(@"DELETE FROM Vote WHERE VoterID=@Id", member);
					 await connection.ExecuteAsync(@"DELETE FROM SkippedVotes WHERE VoterId=@Id", member);
					 await connection.ExecuteAsync(@"UPDATE Voter SET VoteInProgress=0,Voted=0 WHERE ID=@Id", member);
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