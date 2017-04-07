using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class ElectionConfigurationService
    {
        #region Voters
        public static int AddVoters(List<Voter> voters)
        {
            return  new ElectionBaseRepository().InsertBulk(voters);
        }
        public static Voter LoginVoter(Voter voter)
        {
          return  new ElectionBaseRepository().Login(voter);
        }
        #endregion

        #region Election
        public static Settings ConfigureElection()
        {
            return new ElectionBaseRepository().GetAll<Settings>().Single();
        }
        public static void NewElection(Settings settings)
        {
            new ElectionBaseRepository().Insert(settings);
        }
        #endregion
        #region Candidate
        public void AddCandidate(Candidate candidate)
        {
            new ElectionBaseRepository().Insert(candidate);
        }
       
        public  Task<IEnumerable<Candidate>> GetAllCandidates()
        {
            return new ElectionBaseRepository().GetAllAsync<Candidate>();
        }
        public Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return new ElectionBaseRepository().GetAllAsync<Candidate>();
        }
        public static Candidate SaveCandidate(Candidate candidate)
        {
            if (candidate.Id==0)
            {
              return  new ElectionBaseRepository().Insert(candidate);
            }
            else
            {
            return new ElectionBaseRepository().Update(candidate);

            }
        }
       
        public void RemoveCandidate(Candidate candidate)
        {
            new ElectionBaseRepository().Delete(candidate);
        }
        #endregion
        #region Position
        public static Position AddPosition(Position position)
        {
          var data=  new ElectionBaseRepository().Insert(position);
            return data;
        }
        public static Position GetPosition(int positionid)
        {
          return new ElectionBaseRepository().GetById<Position>(positionid);
           
        }
        public static IEnumerable<Position> GetAllPositions()
        {
            return new ElectionBaseRepository().GetPositionsWithDetails();
        }public static IEnumerable<Position> GetPositionsSlim()
        {
            return new ElectionBaseRepository().GetAll<Position>();
        }
        public Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return new ElectionBaseRepository().GetAllAsync<Position>();
        }
        public static void UpdatePosition(Position position)
        {
            new ElectionBaseRepository().Update(position);
        }
       
        public static void RemovePosition(Position position)
        {
            new ElectionBaseRepository().Delete(position);
        }
        #endregion   
        #region User
        public void AddUser(User user)
        {
            new ElectionBaseRepository().Insert(user);
        }
        #endregion
        
    }
}