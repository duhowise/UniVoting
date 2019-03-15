using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using UniVoting.Core;
using UniVoting.Data;

namespace UniVoting.Services
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

        public  async Task SkipVote(SkippedVote skipped)
        {
            try
            {
                 await _context.SkippedVotes.AddAsync(skipped);
            }
            catch (Exception exception)
            {
                //Logger<>.Log(exception);
                Console.WriteLine(exception);
            }
        }

        public  async Task CastVote(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVote> skippedVotes)
        {
            try
            {


                using (var transaction=new TransactionScope())
                {

                    voter.Voted = true;
                    voter.VoteInProgress = false;

                 await _context.Votes.AddRangeAsync(votes);
                  _context.Voters.Update(voter);
                 await _context.SkippedVotes.AddRangeAsync(skippedVotes);
                    transaction.Complete();
                }
            }
            catch (Exception exception)
            {
                await ResetVoter(voter);
                throw;
            }
        }

        public  async Task UpdateVoter(Voter voter)
        {
            try
            {
                await Task.FromResult(_context.Voters.Update(voter));
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public  async Task ResetVoter(Voter voter)
        {
            try
            {
                using (var transaction=new TransactionScope())
                {
                    var dbvoter = _context.Voters.First(x => x.Id == voter.Id);
                    dbvoter.VoteInProgress = false;
                    dbvoter.Voted = false;

                    var votes = _context.Votes.Where(x => x.VoterId == voter.Id).ToList();
                    var skippedVotes = _context.SkippedVotes.Where(x => x.VoterId == voter.Id);
                    _context.Votes.RemoveRange(votes);
                    _context.SkippedVotes.RemoveRange(skippedVotes);
                    _context.SkippedVotes.RemoveRange(skippedVotes);

                    _context.Voters.Update(dbvoter);
                    transaction.Complete();
                    await Task.CompletedTask;
                }
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        public  async Task<Voter> GetVoterPass(string indexNumber)
        {
            try
            {
                return await _context.Voters.FirstOrDefaultAsync(x=>x.IndexNumber.Contains(indexNumber));
            }
            catch (Exception exception)
            {
               
                throw;
            }
           }
    }
}