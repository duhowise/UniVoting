using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Univoting.Data;
using UniVoting.Core;
using UniVoting.Services;

namespace Univoting.Services
{
    public class VotingService : IVotingService
    {
        private readonly ElectionDbContext _context;

        public VotingService(ElectionDbContext context)
        {
            _context = context;
        }
        //private static readonly ILogger Logger=new SystemEventLoggerService();
        //private static readonly IService _context = new _context();

        public async Task SkipVote(SkippedVote skipped)
        {
            try
            {
                await _context.SkippedVotes.AddAsync(skipped);
            }
            catch (Exception exception)
            {
                throw new Exception("Something went wrong. we could not Skip Votes", exception);

            }
        }

        public async Task CastVote(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVote> skippedVotes)
        {
            try
            {


                using (var transaction = _context.Database.BeginTransaction())
                {

                    var result = await _context.Voters.FindAsync(voter.Id);
                    result.Voted = true; 
                    result.VoteInProgress = false;
                    _context.Voters.Update(result);

                    //actual submission of votes and skipped

                    await _context.Votes.AddRangeAsync(votes);
                    await _context.SkippedVotes.AddRangeAsync(skippedVotes);
                   await _context.SaveChangesAsync();
                    transaction.Commit();
                }
            }
            catch (Exception exception)
            {
                await ResetVoter(voter);
                throw new Exception("Something went wrong. we could not submit user votes", exception);

            }
        }

        public async Task UpdateVoter(Voter voter)
        {
            try
            {
                var result = await _context.Voters.FindAsync(voter.Id);
                result.VoteInProgress = voter.VoteInProgress;
                _context.Voters.Update(result);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong. we could not update Voter status", e);

            }
        }

        public async Task ResetVoter(Voter voter)
        {
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var dbvoter =await _context.Voters.FirstOrDefaultAsync(x => x.IndexNumber == voter.IndexNumber);
                    if (dbvoter == null) throw new ArgumentNullException(nameof(dbvoter));
                   

                    var votes = _context.Votes.Where(x => x.VoterId == dbvoter.Id).ToList();
                    var skippedVotes = _context.SkippedVotes.Where(x => x.VoterId == dbvoter.Id);
                    _context.Votes.RemoveRange(votes);
                    _context.SkippedVotes.RemoveRange(skippedVotes);
                    _context.SkippedVotes.RemoveRange(skippedVotes);
                    dbvoter.VoteInProgress = false;
                    dbvoter.Voted = false;
                    _context.Voters.Update(dbvoter);

                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Something went wrong. we could not Reset Voter status", exception);

            }
        }

        public async Task<Voter> GetVoterPass(string indexNumber)
        {
            try
            {
                return await _context.Voters.FirstOrDefaultAsync(x => x.IndexNumber.Contains(indexNumber));
            }
            catch (Exception exception)
            {

                throw new Exception("Something went wrong. we could not retrieve password", exception);

            }
        }
    }
}