using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
    public class VoterRepository : IVoterRepository
    {
        private readonly IDbContextFactory<ElectionDbContext> _dbFactory;

        public VoterRepository(IDbContextFactory<ElectionDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<IEnumerable<Voter>> GetAllAsync()
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Voters.ToListAsync();
        }

        public async Task<Voter> UpdateAsync(Voter voter)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            db.Voters.Update(voter);
            await db.SaveChangesAsync();
            return voter;
        }

        public async Task ResetVoter(Voter member)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            var voter = await db.Voters.FirstOrDefaultAsync(v => v.IndexNumber == member.IndexNumber);
            if (voter == null) return;
            await db.SkippedVoteses.Where(sv => sv.VoterId == voter.Id).ExecuteDeleteAsync();
            await db.Votes.Where(v => v.VoterId == voter.Id).ExecuteDeleteAsync();
            await db.Voters.Where(v => v.Id == voter.Id).ExecuteUpdateAsync(s => s
                .SetProperty(x => x.VoteInProgress, false)
                .SetProperty(x => x.Voted, false));
        }

        public async Task<int> InsertBulkVoters(List<Voter> members)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            await db.Voters.AddRangeAsync(members);
            return await db.SaveChangesAsync();
        }

        public async Task<int> VoteSkipCount(Position position)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.LiveViewSkippedCounts
                .Where(v => v.PositionName == position.PositionName)
                .Select(v => v.Count)
                .FirstOrDefaultAsync();
        }

        public async Task InsertBulkVotes(IEnumerable<Vote> votes, Voter voter, IEnumerable<SkippedVotes> skippedVotes)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            await using var tx = await db.Database.BeginTransactionAsync();
            try
            {
                await db.Votes.AddRangeAsync(votes);
                await db.SkippedVoteses.AddRangeAsync(skippedVotes);
                await db.Voters.Where(v => v.Id == voter.Id).ExecuteUpdateAsync(s => s
                    .SetProperty(x => x.VoteInProgress, false)
                    .SetProperty(x => x.Voted, true));
                await db.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch
            {
                await tx.RollbackAsync();
                await ResetVoter(voter);
            }
        }

        public async Task<Voter> Login(Voter voter)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Voters.FirstOrDefaultAsync(v => v.VoterCode == voter.VoterCode) ?? new Voter();
        }

        public async Task<Voter> GetVoterPass(Voter member)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.Voters.FirstOrDefaultAsync(v => v.IndexNumber == member.IndexNumber) ?? new Voter();
        }

        public async Task<int> VoteCount(Position position)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            return await db.LiveViewCounts
                .Where(v => v.PositionName == position.PositionName)
                .Select(v => v.Count)
                .FirstOrDefaultAsync();
        }

        public async Task<int> InsertSkippedVotes(SkippedVotes skipped)
        {
            await using var db = await _dbFactory.CreateDbContextAsync();
            await db.SkippedVoteses.AddAsync(skipped);
            await db.SaveChangesAsync();
            return skipped.Id;
        }
    }
}
