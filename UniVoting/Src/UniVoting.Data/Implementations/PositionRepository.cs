using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class PositionRepository:Repository<Position>
	{

		public PositionRepository() : base(new DbManager("VotingSystem"))
		{
			
		}
		public override async Task<IEnumerable<Position>> GetAllAsync()
		{
			try
			{
				using (var connection = new DbManager("VotingSystem").Connection)
				{
					return await connection.QueryAsync<Position>(@"SELECT * FROM Position p  ORDER BY p.ID ASC");

				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public  async Task<IEnumerable<Position>> GetPositionsWithDetailsAsync()
		{
			try
			{
				IEnumerable<Position> positions = new List<Position>();
				using (var connection = new DbManager("VotingSystem").Connection)
				{
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
		public  IEnumerable<Position> GetPositionsWithDetails()
		{
			try
			{
				IEnumerable<Position> positions = new List<Position>();
				using (var connection = new DbManager("VotingSystem").Connection)
				{
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

	}
}