using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Core;

namespace Univoting.Services
{
    public interface IElectionConfigurationService
    {
        Task<Candidate> AddCandidateAsync(Candidate candidate);
        Task<Position> AddPositionAsync(Position position);
        Task<ElectionConfiguration> AddElectionConfigurationsAsync(ElectionConfiguration electionConfiguration);
        Task<List<Voter>> AddVotersAsync(List<Voter> voters);
        Task<ElectionConfiguration> ConfigureElectionAsync();
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
        Task<IEnumerable<Rank>> GetAllRanksAsync();
        Task<List<Position>> GetAllPositionsAsync(bool includeFaculty);
        Task<List<Position>> GetAllPositionsAsync();
        Task<IEnumerable<Voter>> GetAllVotersAsync();
        Task<IEnumerable<Candidate>> GetCandidateWithDetailsAsync();
        Task<Position> GetPositionAsync(int positionId);
        Task<Commissioner> LoginAsync(Commissioner commissioner);
        Task<Voter> LoginVoterAsync(Voter voter);
        void RemoveCandidateAsync(Candidate candidate);
        Task RemovePositionAsync(Position position);
        Task<Candidate> SaveCandidateAsync(Candidate candidate);
        Task<Commissioner> SaveCommissionerAsync(Commissioner commissioner);
         Task<List<Faculty>> GetFacultiesAsync();
         Task<IList<Rank>> GetAllRanks();
    }
}