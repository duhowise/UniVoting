using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using UniVoting.Data.Implementations;
using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class VotingService
    {
        private static readonly ILogger Logger=new SystemEventLoggerService();
        private static readonly IService Electionservice = new ElectionService();

        public static async Task SkipVote(SkippedVotes skipped)
        {
            try
            {
                await Electionservice.Voters.InsertSkippedVotes(skipped);
            }
            catch (Exception exception)
            {
                Logger.Log(exception);
                throw;
            }
        }

        public static async Task CastVote(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVotes> skippedVotes)
        {
            try
            {
                await Electionservice.Voters.InsertBulkVotes(votes, voter, skippedVotes);
            }
            catch (Exception exception)
            {
                await ResetVoter(voter);
                Logger.Log(exception);
                throw;
            }
        }

        public static async Task UpdateVoter(Voter voter)
        {
            try
            {
                await Electionservice.Voters.UpdateAsync(voter);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                throw;
            }
        }

        public static async Task ResetVoter(Voter voter)
        {
            try
            {
                await Electionservice.Voters.ResetVoter(voter);
            }
            catch (Exception exception)
            {
                Logger.Log(exception);
            }
        }

        public static async Task<Voter> GetVoterPass(Voter voter)
        {
            try
            {
                return await Electionservice.Voters.GetVoterPass(voter);
            }
            catch (Exception exception)
            {
                Logger.Log(exception);
                throw;
            }
           }
    }
}