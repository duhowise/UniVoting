using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class CandidateRepository
	{
		private readonly VotingDbContext _context;

		public CandidateRepository(VotingDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Candidate>> GetAllAsync()
		{
			return await _context.Candidates
				.Include(c => c.Position)
				.Include(c => c.Rank)
				.ToListAsync();
		}

		public async Task<Candidate> GetByIdAsync(int id)
		{
			return await _context.Candidates
				.Include(c => c.Position)
				.Include(c => c.Rank)
				.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<IEnumerable<Candidate>> GetByPositionIdAsync(int positionId)
		{
			return await _context.Candidates
				.Include(c => c.Rank)
				.Where(c => c.PositionId == positionId)
				.OrderBy(c => c.RankId)
				.ToListAsync();
		}

		public async Task<Candidate> AddAsync(Candidate candidate)
		{
			_context.Candidates.Add(candidate);
			await _context.SaveChangesAsync();
			return candidate;
		}

		public async Task<Candidate> UpdateAsync(Candidate candidate)
		{
			_context.Candidates.Update(candidate);
			await _context.SaveChangesAsync();
			return candidate;
		}

		public async Task DeleteAsync(Candidate candidate)
		{
			_context.Candidates.Remove(candidate);
			await _context.SaveChangesAsync();
		}
	}
}