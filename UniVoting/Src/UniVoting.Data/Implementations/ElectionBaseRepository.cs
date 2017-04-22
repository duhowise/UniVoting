using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
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
	   
		public  T Insert<T>(T member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.OpenAsync();

				}
				var insert =(int) connection.Insert(member);
				 return GetById<T>(insert);
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
		public  T GetById<T>(int member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.OpenAsync();

				}
				return connection.Get<T>(member);
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
		public  T Update<T>(T member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{
					connection.OpenAsync();

				}
				var id=  connection.Update(member);
				return GetById<T>(id);
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
				return
					(int)
					connection.Execute(@"INSERT INTO dbo.Vote(  VoterID ,CandidateID ,PositionID)VALUES(@VoterID,@CandidateID,@PositionID)", member);
			}
		}
		public  int InsertBulk<T>(List<Voter> member)
		{
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State!= ConnectionState.Open)
				{
					connection.OpenAsync();

				}
				return
					(int)
					connection.Execute(@"INSERT INTO dbo.Voter(  VoterName ,VoterCode ,IndexNumber)
				VALUES(@VoterName,@VoterCode,@IndexNumber)", member);
			}
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