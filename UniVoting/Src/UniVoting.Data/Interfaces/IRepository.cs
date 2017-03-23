using System.Collections.Generic;

namespace UniVoting.Data.Interfaces
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>();
        T Insert<T>(T member);
        T GetById<T>(int member);
        void Update<T>(T member);
        void Delete<T>(T member);
    }
}