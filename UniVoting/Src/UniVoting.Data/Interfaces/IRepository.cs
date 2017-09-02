using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniVoting.Data.Interfaces
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>();
       Task<IEnumerable<T>> GetAllAsync<T>();
        T Insert<T>(T member);
       Task<T> InsertAsync<T>(T member);
        T GetById<T>(int member);
       Task<T> GetByIdAsync<T>(int member);
        void Update<T>(T member);
        Task UpdateAsync<T>(T member);
        void Delete<T>(T member);
        Task DeleteAsync<T>(T member);
    }
}