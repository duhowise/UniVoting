using System.Collections.Generic;
using System.Threading.Tasks;
using Univoting.Core;

namespace Univoting.Services
{
    public interface ILiveViewService
    {
        Task<IEnumerable<Position>> Positions();
        Task<int> VoteCountAsync(int positionId);
        Task<int> VotesSkippedCountAsync(int positionId);
    }
}