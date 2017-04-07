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
            return new ElectionBaseRepository().VoteCount(new Position {PositionName = position});
        }public static Task<int> VotesSkipppedCountAsync(string position)
        {
            return new ElectionBaseRepository().VoteSkipCount(new Position {PositionName = position});
        }

        public static IEnumerable<Position> Positions()
        {
            return new ElectionBaseRepository().GetAll<Position>();
        }

    }
}