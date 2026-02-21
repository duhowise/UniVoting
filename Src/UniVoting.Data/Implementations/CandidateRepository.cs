using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly IDbContextFactory<ElectionDbContext> _dbFactory;

        public CandidateRepository(IDbContextFactory<ElectionDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Candidates.ToListAsync();
        }

        public async Task<Candidate> InsertAsync(Candidate candidate)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            await db.Candidates.AddAsync(candidate);
            await db.SaveChangesAsync();
            return candidate;
        }

        public async Task<Candidate> UpdateAsync(Candidate candidate)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            db.Candidates.Update(candidate);
            await db.SaveChangesAsync();
            return candidate;
        }

        public void Delete(Candidate candidate)
        {
            using var db = _dbFactory.CreateDbContext();
            db.Candidates.Remove(candidate);
            db.SaveChanges();
        }
    }
}
