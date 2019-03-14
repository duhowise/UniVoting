using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Services
{
    public interface ILiveViewService
    {
        Task<IEnumerable<Position>> Positions();
        Task<int> VoteCountAsync(int positionId);
        Task<int> VotesSkipppedCountAsync(int positionId);
    }
}