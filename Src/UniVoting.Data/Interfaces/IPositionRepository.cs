using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Data.Interfaces;

public interface IPositionRepository
{
    IEnumerable<Position> GetAll();
    Task<IEnumerable<Position>> GetAllAsync();
    Task<Position> InsertAsync(Position position);
    Task<Position> UpdateAsync(Position position);
    void Delete(Position position);
    Position GetById(int id);
    Task<Position> GetByIdAsync(int id);
    IEnumerable<Position> GetPositionsWithDetails();
    Task<IEnumerable<Position>> GetPositionsWithDetailsAsync();
}
