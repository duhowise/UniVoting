using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class CandidateRepository:Repository<Candidate>
	{

		public CandidateRepository():base(new DbManager("VotingSystem"))
		{
			
		}
		public async Task<IEnumerable<Candidate>> GetAllCandidatesWithPositions()
		{
			var candidates = new List<Candidate>();
			using (var connection = new SqlConnection(new ConnectionHelper().Connection))
			{
				if (connection.State != ConnectionState.Open)
				{

					await connection.OpenAsync();

				}
				candidates = (List<Candidate>)await connection.GetListAsync<Candidate>();
				foreach (var candidate in candidates)
				{
					//todo get positionid from position repository
					//candidate.Position = await GetByIdAsync<Position>(Convert.ToInt32(candidate.PositionId));
				}
			}

			return candidates;
		}


		
		//public async Task<Candidate>  Update(Candidate member)
		//{
		//	using (var connection = new DbManager("VotingSystem").Connection)
		//	{
		//		if (connection.State != ConnectionState.Open)
		//		{
		//			await connection.OpenAsync();

		//		}
		//		var id = Convert.ToInt32(await connection.UpdateAsync(member));
		//		return await GetByIdAsync<Candidate>(id);
		//	}
		//}
	}
}