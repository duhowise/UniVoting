
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class VoterRepository
	{
		private readonly VotingDbContext _context;

		public VoterRepository(VotingDbContext context)
		{
			_context = context;
		}
		public async Task ResetVoter(Voter member)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				var voter = await _context.Voters.FirstOrDefaultAsync(v => v.IndexNumber == member.IndexNumber);
				if (voter != null)
				{
					var skipped = _context.SkippedVotes.Where(sv => sv.VoterId == voter.Id);
					var votes = _context.Votes.Where(v => v.VoterId == voter.Id);
					_context.SkippedVotes.RemoveRange(skipped);
					_context.Votes.RemoveRange(votes);
					voter.VoteInProgress = false;
					voter.Voted = false;
					await _context.SaveChangesAsync();
					await transaction.CommitAsync();
				}
			}
			catch (Exception)
			{
				// ignored
			}
		}
		public async Task<int> InsertBulkVoters(List<Voter> members)
		{
			await _context.Voters.AddRangeAsync(members);
			var count = await _context.SaveChangesAsync();
			return count;
		}
		public async Task<int> VoteSkipCount(Position position)
		{
			// Assuming vw_LiveViewSkipped is mapped as a DbQuery or view
			// If not, this will need a FromSqlRaw call
			var count = await _context.Database.ExecuteSqlInterpolatedAsync($@"SELECT `Count` FROM vw_LiveViewSkipped WHERE PositionName = {position.PositionName}");
			return count;
		}
		public async Task<int> InsertBulkVotes(IEnumerable<Vote> votes, Voter voter, IEnumerable<SkippedVotes> skippedVotes)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				await _context.Votes.AddRangeAsync(votes);
				await _context.SkippedVotes.AddRangeAsync(skippedVotes);
				var dbVoter = await _context.Voters.FindAsync(voter.Id);
				if (dbVoter != null)
				{
					dbVoter.VoteInProgress = false;
					dbVoter.Voted = true;
				}
				var result = await _context.SaveChangesAsync();
				await transaction.CommitAsync();
				return result;
			}
			catch (Exception)
			{
				await ResetVoter(voter);
				return 0;
			}
		}
		public async Task<Voter> Login(Voter voter)
		{
			return await _context.Voters.FirstOrDefaultAsync(v => v.VoterCode == voter.VoterCode);
		}
		public async Task<Voter> GetVoterPass(Voter member)
		{
			return await _context.Voters.FirstOrDefaultAsync(v => v.IndexNumber == member.IndexNumber);
		}
		public async Task<int> VoteCount(Position position)
		{
			// Assuming vw_LiveView is mapped as a view or use FromSqlRaw
			var count = await _context.Database.ExecuteSqlInterpolatedAsync($@"SELECT `Count` FROM vw_LiveView WHERE PositionName = {position.PositionName}");
			return count;
		}

		public async Task<int> InsertSkippedVotes(SkippedVotes skipped)
		{
			await _context.SkippedVotes.AddAsync(skipped);
			return await _context.SaveChangesAsync();
		}

		// Basic CRUD operations
		public async Task<IEnumerable<Voter>> GetAllAsync()
		{
			return await _context.Voters.ToListAsync();
		}

		public async Task<Voter> GetByIdAsync(int id)
		{
			return await _context.Voters.FindAsync(id);
		}

		public async Task<Voter> AddAsync(Voter voter)
		{
			_context.Voters.Add(voter);
			await _context.SaveChangesAsync();
			return voter;
		}

		public async Task<Voter> UpdateAsync(Voter voter)
		{
			_context.Voters.Update(voter);
			await _context.SaveChangesAsync();
			return voter;
		}

		public async Task DeleteAsync(Voter voter)
		{
			_context.Voters.Remove(voter);
			await _context.SaveChangesAsync();
		}

		

	}
}