using System.Collections.Generic;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class VotingService
    {
        public static void SkipVote(SkippedVote skipped)
        {
            new ElectionBaseRepository().Insert(skipped);
        }
        public static void CastVote(IEnumerable<Vote> votes)
        {
            ElectionBaseRepository.InsertBulk(votes);
        }
    }
}