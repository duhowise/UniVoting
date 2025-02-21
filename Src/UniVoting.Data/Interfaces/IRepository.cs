using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniVoting.Data.Interfaces
{
    public interface IRepository<T> where T: class 
    {
        IEnumerable<T> GetAll();
       Task<IEnumerable<T>> GetAllAsync();
        T Insert(T member);
       Task<T> InsertAsync(T member);
        T GetById(int member);
       Task<T> GetByIdAsync(int member);
        T Update(T member);
        Task<T> UpdateAsync(T member);
        void Delete(T member);
        Task DeleteAsync(T member);
    }
}