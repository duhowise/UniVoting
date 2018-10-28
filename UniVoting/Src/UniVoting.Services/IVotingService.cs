using System.Collections.Concurrent;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Services
{
    public interface IVotingService
    {
        Task CastVote(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVote> skippedVotes);
        Task<Voter> GetVoterPass(string indexNumber);
        Task ResetVoter(Voter voter);
        Task SkipVote(SkippedVote skipped);
        Task UpdateVoter(Voter voter);
    }
}