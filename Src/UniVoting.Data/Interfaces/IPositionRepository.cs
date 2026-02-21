using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Interfaces;

public interface IPositionRepository : IRepository<Position>
{
    IEnumerable<Position> GetPositionsWithDetails();
    Task<IEnumerable<Position>> GetPositionsWithDetailsAsync();
}
