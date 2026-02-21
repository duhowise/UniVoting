using System.Collections.Concurrent;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Services;

public interface IVotingService
{
    Task SkipVote(SkippedVotes skipped);
    Task CastVote(ConcurrentBag<Vote> votes, Voter voter, ConcurrentBag<SkippedVotes> skippedVotes);
    Task UpdateVoter(Voter voter);
    Task ResetVoter(Voter voter);
    Task<Voter> GetVoterPass(Voter voter);
}
