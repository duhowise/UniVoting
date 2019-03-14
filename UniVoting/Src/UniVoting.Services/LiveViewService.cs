using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Core;
using UniVoting.Data;

namespace UniVoting.Services
{
	public class LiveViewService : ILiveViewService
    {
	    private readonly ElectionDbContext _context;
	    //private static readonly IService _context=new _context();

	    public LiveViewService(ElectionDbContext context)
	    {
	        _context = context;
	    }
		public  async Task<int> VoteCountAsync(int positionId)
		{
			try
			{
				//return _context.Voters.Where(new Position { PositionName = positionId?.Trim() });
				return await _context.Votes.AsNoTracking().Where(x=>x.PositionId==positionId).CountAsync();

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
		public  async Task<int> VotesSkipppedCountAsync(int positionId)
		{
			try
			{
				return await _context.SkippedVoteses.AsNoTracking().Where(x=>x.Positionid==positionId).CountAsync();

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		public  async Task<IEnumerable<Position>> Positions()
		{
			try
			{
				return await _context.Positions.AsNoTracking().ToListAsync();

			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

	}
}