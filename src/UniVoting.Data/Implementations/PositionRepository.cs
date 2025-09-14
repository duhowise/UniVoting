using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class PositionRepository
	{
		private readonly VotingDbContext _context;

		public PositionRepository(VotingDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Position>> GetAllAsync()
		{
			return await _context.Positions
				.OrderBy(p => p.Id)
				.ToListAsync();
		}

		public async Task<IEnumerable<Position>> GetPositionsWithDetailsAsync()
		{
			return await _context.Positions
				.Include(p => p.Candidates.OrderBy(c => c.RankId))
				.OrderByDescending(p => p.Id)
				.ToListAsync();
		}

		public IEnumerable<Position> GetPositionsWithDetails()
		{
			return _context.Positions
				.Include(p => p.Candidates.OrderBy(c => c.RankId))
				.OrderByDescending(p => p.Id)
				.ToList();
		}

		public async Task<Position> GetByIdAsync(int id)
		{
			return await _context.Positions.FindAsync(id);
		}

		public async Task<Position> AddAsync(Position position)
		{
			_context.Positions.Add(position);
			await _context.SaveChangesAsync();
			return position;
		}

		public async Task<Position> UpdateAsync(Position position)
		{
			_context.Positions.Update(position);
			await _context.SaveChangesAsync();
			return position;
		}

		public async Task DeleteAsync(Position position)
		{
			_context.Positions.Remove(position);
			await _context.SaveChangesAsync();
		}
	}
}