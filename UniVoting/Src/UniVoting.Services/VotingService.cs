using System.Collections.Generic;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class VotingService
    {
        public static void SkipVote(SkippedVotes skipped)
        {
            new ElectionBaseRepository().Insert(skipped);
        }
        public static void CastVote(List<Vote> votes)
        {
            new ElectionBaseRepository().InsertBulk(votes);
        }
        public static void UpdateVoter(Voter voter)
        {
            new ElectionBaseRepository().Update(voter);
        }
    }
}