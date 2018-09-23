using System.Collections.Generic;
using System.Threading.Tasks;
using UniVoting.Model;

namespace UniVoting.Services
{
	public interface IElectionConfigurationService
	{
		Task<Candidate> AddCandidate(Candidate candidate);
		Task<Position> AddPosition(Position position);
		Task<Setting> AddSettings(Setting setting);
		Task<List<Voter>> AddVotersAsync(List<Voter> voters);
		Task<Setting> ConfigureElection();
		Task<IEnumerable<Candidate>> GetAllCandidates();
		Task<IEnumerable<Position>> GetAllPositionsAsync();
		Task<IEnumerable<Voter>> GetAllVotersAsync();
		Task<IEnumerable<Candidate>> GetCandidateWithDetails();
		Task<Position> GetPosition(int positionid);
		Task<Comissioner> Login(Comissioner comissioner);
		Task<Voter> LoginVoter(Voter voter);
		void RemoveCandidate(Candidate candidate);
		Task RemovePosition(Position position);
		Task<Candidate> SaveCandidate(Candidate candidate);
		Task<Comissioner> SaveComissioner(Comissioner comissioner);
		Task<Position> UpdatePosition(Position position);
	}
}