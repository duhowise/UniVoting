using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Interfaces;

public interface IVoterRepository : IRepository<Voter>
{
    Task ResetVoter(Voter member);
    Task<int> InsertBulkVoters(List<Voter> voters);
    Task InsertBulkVotes(IEnumerable<Vote> votes, Voter voter, IEnumerable<SkippedVotes> skippedVotes);
    Task<int> InsertSkippedVotes(SkippedVotes skipped);
    Task<Voter> Login(Voter voter);
    Task<Voter> GetVoterPass(Voter voter);
    Task<int> VoteCount(Position position);
    Task<int> VoteSkipCount(Position position);
}
