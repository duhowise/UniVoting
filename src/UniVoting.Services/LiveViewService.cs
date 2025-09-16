using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Data;
using UniVoting.Model;

namespace UniVoting.Services
{
	public class LiveViewService
	{
		private readonly VotingDbContext _dbContext;

		public LiveViewService(VotingDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		
		public async Task<int> VoteCountAsync(string position)
		{
			try
			{
				return await _dbContext.Votes
					.Include(v => v.Position)
					.CountAsync(v => v.Position.PositionName == position);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		
		public async Task<int> VotesSkipppedCountAsync(string position)
		{
			try
			{
				return await _dbContext.SkippedVotes
					.Include(sv => sv.Position)
					.CountAsync(sv => sv.Position.PositionName == position);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public async Task<IEnumerable<Position>> Positions()
		{
			try
			{
				return await _dbContext.Positions.ToListAsync();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}
}