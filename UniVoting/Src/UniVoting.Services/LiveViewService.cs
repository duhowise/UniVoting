using System.Collections.Generic;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class LiveViewService
    {
        public static int VoteCount(string position)
        {
            return ElectionBaseRepository.VoteCount(new Position {PositionName = position});
        }public static int VotesSkipppedCount(string position)
        {
            return ElectionBaseRepository.VoteSkipCount(new Position {PositionName = position});
        }

        public static IEnumerable<Position> Positions()
        {
            return ElectionBaseRepository.GetAll<Position>();
        }

    }
}