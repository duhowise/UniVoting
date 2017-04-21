using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class LiveViewService
    {
        public static Task<int> VoteCountAsync(string position)
        {
            try
            {
                return new ElectionBaseRepository().VoteCount(new Position { PositionName = position });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static Task<int> VotesSkipppedCountAsync(string position)
        {
            try
            {
                return new ElectionBaseRepository().VoteSkipCount(new Position { PositionName = position });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static Task<IEnumerable<Position>> Positions()
        {
            try
            {
                return new ElectionBaseRepository().GetAllAsync<Position>();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}