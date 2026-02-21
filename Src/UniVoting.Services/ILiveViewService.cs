using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Services;

public interface ILiveViewService
{
    Task<int> VoteCountAsync(string position);
    Task<int> VotesSkipppedCountAsync(string position);
    Task<IEnumerable<Position>> Positions();
}
