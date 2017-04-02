using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
    public class ElectionBaseRepository
    {
        private static string Connectionstring => ConnectionHelper.Connection;
        public static IEnumerable<T> GetAll<T>()
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.GetList<T>();
            }

        }
        public static Task<IEnumerable<T>> GetAllAsync<T>()
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.GetListAsync<T>();
            }

        }
       
        public  T Insert<T>(T member)
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var insert =(int) connection.Insert(member);
                 return GetById<T>(insert);
            }

        }

        public static async Task<int> VoteCount(Position position)
        {
           
            using (var connection = new SqlConnection(Connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return await connection.ExecuteScalarAsync<int>(@"SELECT [Count] FROM dbo.vw_LiveView
                                        WHERE PositionName = @positionName", position);
              
            }
           
        }
        
        public static Voter Login(Voter voter )
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return connection.QueryFirstOrDefault<Voter>(@"SELECT  ID ,VoterName,VoterCode,IndexNumber,Voted ,VoteInProgress
                        FROM dbo.Voter v WHERE v.VoterCode=@VoterCode", voter);
            }
        }
        public static IEnumerable<Position> GetPositionsWithDetails()
        {
           IEnumerable<Position>positions=new List<Position>();
            using (var connection = new SqlConnection(Connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                positions = connection.GetList<Position>();
                foreach ( var position in positions)
                {
                    position.Candidates = connection.Query<Candidate>(@"SELECT  ID ,PositionID ,CandidateName ,CandidatePicture
                     ,RankId FROM dbo.Candidate C WHERE c.PositionID=@Id", position);
                }
            }
            return positions ;
        }

        public static async Task<int> VoteSkipCount(Position position)
        {
           
            using (var connection = new SqlConnection(Connectionstring))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                return await connection.ExecuteScalarAsync<int>(@"SELECT [Count] FROM dbo.vw_LiveViewSkipped
                                        WHERE PositionName = @positionName", position);
               
            }
           
        }
        public static T GetById<T>(int member)
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
                return connection.Get<T>(member);
            }
        }
        public static T Update<T>(T member)
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
             var id=  connection.Update(member);
                return GetById<T>(id);
            }
        }
        public static int InsertBulk<T>(List<T> member)
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
                return
                    (int)
                    connection.Execute(@"INSERT INTO dbo.Vote(  VoterID ,CandidateID ,PositionID)VALUES(@VoterID,@CandidateID,@PositionID)", member);
            }
        }
        public static int InsertBulk<T>(List<Voter> member)
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
                return
                    (int)
                    connection.Execute(@"INSERT INTO dbo.Voter(  VoterName ,VoterCode ,IndexNumber)
                VALUES(@VoterName,@VoterCode,@IndexNumber)", member);
            }
        }

        public static void Delete<T>(T member)
        {
            using (var connection = new SqlConnection(Connectionstring))
            {
                connection.Delete(member);
            }
        }
    }
}