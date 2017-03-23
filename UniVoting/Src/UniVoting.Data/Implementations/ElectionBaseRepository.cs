using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using UniVoting.Data.Interfaces;
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

        public static void Delete<T>(T member)
        {
            using (_connection)
            {
                _connection.Delete(member);
            }
        }
    }
}