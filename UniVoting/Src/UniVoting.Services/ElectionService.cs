using System.Collections.Generic;
using System.Linq;
using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Services
{
    public class ElectionService
    {
        #region Election
        public Settings ConfigureElection(Settings settings)
        {
            return ElectionBaseRepository.GetAll<Settings>().Single();
        }
        public void NewElection(Settings settings)
        {
            new ElectionBaseRepository().Insert(settings);
        }
        #endregion
        #region Candidate
        public void AddCandidate(Candidate candidate)
        {
            new ElectionBaseRepository().Insert(candidate);
        }
        public void UpdateCandidate(Candidate candidate)
        {
            ElectionBaseRepository.Update(candidate);
        }
        public void RemoveCandidate(Candidate candidate)
        {
            ElectionBaseRepository.Delete(candidate);
        }
        #endregion
        #region Position
        public void AddPosition(Position position)
        {
            new ElectionBaseRepository().Insert(position);
        }
        public void UpdatePosition(Position position)
        {
            ElectionBaseRepository.Update(position);
        }
        public void RemovePosition(Position position)
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