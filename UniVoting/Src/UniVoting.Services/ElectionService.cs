using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class ElectionService
    {
        #region Voters
        public static int AddVoters(IEnumerable<Voter> voters)
        {
          return  ElectionBaseRepository.InsertBulk(voters);
        }
        #endregion

        #region Election
        public static Settings ConfigureElection(Settings settings)
        {
            return ElectionBaseRepository.GetAll<Settings>().Single();
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
       

        public static IEnumerable<Candidate> GetAllCandidates()
        {
            return ElectionBaseRepository.GetAll<Candidate>();
        }
        public static Task<IEnumerable<Candidate>> GetAllCandidatesAsync()
        {
            return ElectionBaseRepository.GetAllAsync<Candidate>();
        }
        public static void SaveCandidate(Candidate candidate)
        {
            if (candidate.Id==0)
            {
                new ElectionBaseRepository().Insert(candidate);
            }
            else
            {
            ElectionBaseRepository.Update(candidate);

            }
        }
        public void RemoveCandidate(Candidate candidate)
        {
            ElectionBaseRepository.Delete(candidate);
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
          return ElectionBaseRepository.GetById<Position>(positionid);
           
        }
        public static IEnumerable<Position> GetAllPositions()
        {
            return ElectionBaseRepository.GetAll<Position>();
        }
        public static Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return ElectionBaseRepository.GetAllAsync<Position>();
        }
        public static void UpdatePosition(Position position)
        {
            ElectionBaseRepository.Update(position);
        }
       
        public static void RemovePosition(Position position)
        {
            ElectionBaseRepository.Delete(position);
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