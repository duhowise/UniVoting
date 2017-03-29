using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using UniVoting.Model;

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

        public static int VoteCount(Position position)
        {
            using (_connection)
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection.ExecuteScalar<int>(@"SELECT [Count] FROM dbo.vw_LiveView
                                        WHERE PositionName = @positionName", position);
            }
        }
        public static int VoteSkipCount(Position position)
        {
            using (_connection)
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection.ExecuteScalar<int>(@"SELECT [Count] FROM dbo.vw_LiveViewSkipped
                                        WHERE PositionName = @positionName", position);
            }
        }
        public static T GetById<T>(int member)
        {
            using (_connection)
            {
                return _connection.Get<T>(member);
            }
        }
        public static T Update<T>(T member)
        {
            using (_connection)
            {
             var id=  _connection.Update(member);
                return GetById<T>(id);
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