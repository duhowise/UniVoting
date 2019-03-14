using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Core;

namespace UniVoting.Services
{
    public interface IElectionConfigurationService
	{
		Task<Candidate> AddCandidate(Candidate candidate);
		Task<Position> AddPosition(Position position);
		Task<ElectionConfiguration> AddSettings(ElectionConfiguration electionConfiguration);
		Task<List<Voter>> AddVotersAsync(List<Voter> voters);
		Task<ElectionConfiguration> ConfigureElection();
		Task<IEnumerable<Candidate>> GetAllCandidates();
		Task<List<Position>> GetAllPositionsAsync();
		Task<IEnumerable<Voter>> GetAllVotersAsync();
		Task<IEnumerable<Candidate>> GetCandidateWithDetails();
		Task<Position> GetPosition(int positionid);
		Task<Commissioner> Login(Commissioner commissioner);
		Task<Voter> LoginVoter(Voter voter);
		void RemoveCandidate(Candidate candidate);
		Task RemovePosition(Position position);
		Task<Candidate> SaveCandidate(Candidate candidate);
		Task<Commissioner> SaveComissioner(Commissioner commissioner);
		Task<Position> UpdatePosition(Position position);
	}
}