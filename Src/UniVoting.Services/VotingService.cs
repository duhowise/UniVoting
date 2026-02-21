using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class VotingService : IVotingService
    {
        private readonly ILogger _logger;
        private readonly IService _electionservice;

        public VotingService(IService electionService, ILogger logger)
        {
            _electionservice = electionService;
            _logger = logger;
        }

        public async Task SkipVote(SkippedVotes skipped)
        {
            try
            {
                await _electionservice.Voters.InsertSkippedVotes(skipped);
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
                throw;
            }
        }

        public async Task CastVote(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVotes> skippedVotes)
        {
            try
            {
                await _electionservice.Voters.InsertBulkVotes(votes, voter, skippedVotes);
            }
            catch (Exception exception)
            {
                await ResetVoter(voter);
                _logger.Log(exception);
                throw;
            }
        }

        public async Task UpdateVoter(Voter voter)
        {
            try
            {
                await _electionservice.Voters.UpdateAsync(voter);
            }
            catch (Exception e)
            {
                _logger.Log(e);
                throw;
            }
        }

        public async Task ResetVoter(Voter voter)
        {
            try
            {
                await _electionservice.Voters.ResetVoter(voter);
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
            }
        }

        public async Task<Voter> GetVoterPass(Voter voter)
        {
            try
            {
                return await _electionservice.Voters.GetVoterPass(voter);
            }
            catch (Exception exception)
            {
                _logger.Log(exception);
                throw;
            }
        }
    }
}