using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(IDbContextFactory<ElectionDbContext> dbFactory) : base(dbFactory) { }

        public override async Task<IEnumerable<Position>> GetAllAsync()
        {
            await using var db = await DbFactory.CreateDbContextAsync();
            return await db.Positions.OrderBy(p => p.Id).ToListAsync();
        }

        public async Task<IEnumerable<Position>> GetPositionsWithDetailsAsync()
        {
            await using var db = await DbFactory.CreateDbContextAsync();
            return await db.Positions
                .Include(p => p.Candidates.OrderBy(c => c.RankId))
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public IEnumerable<Position> GetPositionsWithDetails()
        {
            using var db = DbFactory.CreateDbContext();
            return db.Positions
                .Include(p => p.Candidates.OrderBy(c => c.RankId))
                .OrderByDescending(p => p.Id)
                .ToList();
        }
    }
}
