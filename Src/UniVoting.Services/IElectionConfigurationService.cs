using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Services;

public interface IElectionConfigurationService
{
    Task<int> AddVotersAsync(List<Voter> voters);
    Task<IEnumerable<Voter>> GetAllVotersAsync();
    Task<Voter> LoginVoter(Voter voter);
    Setting ConfigureElection();
    Task NewElection(Setting setting);
    Task AddCandidate(Candidate candidate);
    Task SaveComissioner(Comissioner comissioner);
    Task<IEnumerable<Candidate>> GetAllCandidates();
    Task<Candidate> SaveCandidate(Candidate candidate);
    void RemoveCandidate(Candidate candidate);
    Task<Position> AddPosition(Position position);
    Task<Position> GetPosition(int positionid);
    IEnumerable<Position> GetAllPositions();
    IEnumerable<Position> GetPositionsSlim();
    Task<IEnumerable<Position>> GetAllPositionsAsync();
    Task UpdatePosition(Position position);
    void RemovePosition(Position position);
    Task<Comissioner> Login(Comissioner comissioner);
    Task<IEnumerable<Candidate>> GetCandidateWithDetails();
}
