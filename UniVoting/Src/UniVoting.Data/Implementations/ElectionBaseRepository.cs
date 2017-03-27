using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace UniVoting.Data.Implementations
{
    public class ElectionBaseRepository
    {
        private static IDbConnection _connection => new ConnectionHelper().Connection;
        public static IEnumerable<T> GetAll<T>()
        {
            using (_connection)
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection.GetList<T>();
            }

        }
        public static Task<IEnumerable<T>> GetAllAsync<T>()
        {
            using (_connection)
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection.GetListAsync<T>();
            }

        }
       
        public  T Insert<T>(T member)
        {
            using (_connection)
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                var insert =(int) _connection.Insert(member);
                 return GetById<T>(insert);
            }

        }

        public static T GetById<T>(int member)
        {
            using (_connection)
            {
                return _connection.Get<T>(member);
            }
        }
        public static void Update<T>(T member)
        {
            using (_connection)
            {
                _connection.Update(member);
            }
        }
        public static int InsertBulk<T>(IEnumerable<T> member)
        {
            using (_connection)
            {
             return   (int)_connection.Insert(member);
            }
        }

        public static void Delete<T>(T member)
        {
            using (_connection)
            {
                _connection.Delete(member);
            }
        }
    }
}