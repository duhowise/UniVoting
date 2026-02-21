using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
    public class PositionRepository : IPositionRepository
    {
        private readonly IDbContextFactory<ElectionDbContext> _dbFactory;

        public PositionRepository(IDbContextFactory<ElectionDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public IEnumerable<Position> GetAll()
        {
            using var db = _dbFactory.CreateDbContext();
            return db.Positions.ToList();
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Positions.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<Position> InsertAsync(Position position)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            await db.Positions.AddAsync(position);
            await db.SaveChangesAsync();
            return position;
        }

        public async Task<Position> UpdateAsync(Position position)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            db.Positions.Update(position);
            await db.SaveChangesAsync();
            return position;
        }

        public void Delete(Position position)
        {
            using var db = _dbFactory.CreateDbContext();
            db.Positions.Remove(position);
            db.SaveChanges();
        }

        public Position GetById(int id)
        {
            using var db = _dbFactory.CreateDbContext();
            return db.Positions.Find(id)!;
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return (await db.Positions.FindAsync(id))!;
        }

        public async Task<IEnumerable<Position>> GetPositionsWithDetailsAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Positions
                .Include(p => p.Candidates.OrderBy(c => c.RankId))
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public IEnumerable<Position> GetPositionsWithDetails()
        {
            using var db = _dbFactory.CreateDbContext();
            return db.Positions
                .Include(p => p.Candidates.OrderBy(c => c.RankId))
                .OrderByDescending(p => p.Id)
                .ToList();
        }
    }
}
