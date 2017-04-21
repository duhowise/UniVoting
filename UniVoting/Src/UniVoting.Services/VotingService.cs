using System;
using System.Collections.Generic;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class VotingService
    {
        public static void SkipVote(SkippedVotes skipped)
        {
            try
            {
                new ElectionBaseRepository().Insert(skipped);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static void CastVote(List<Vote> votes)
        {
            try
            {
                new ElectionBaseRepository().InsertBulk(votes);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static void UpdateVoter(Voter voter)
        {
            try
            {
                new ElectionBaseRepository().Update(voter);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}