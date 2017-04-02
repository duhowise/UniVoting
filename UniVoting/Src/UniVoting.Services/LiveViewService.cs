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
            return ElectionBaseRepository.VoteCount(new Position {PositionName = position});
        }public static Task<int> VotesSkipppedCountAsync(string position)
        {
            return ElectionBaseRepository.VoteSkipCount(new Position {PositionName = position});
        }

        public static IEnumerable<Position> Positions()
        {
            return ElectionBaseRepository.GetAll<Position>();
        }

    }
}