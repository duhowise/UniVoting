using UniVoting.Data.Implementations;
using UniVoting.Model;

namespace UniVoting.Data.Interfaces
{
	public interface IService
	{
		CandidateRepository Candidates { get; }
		VoterRepository Voters { get; }
		PositionRepository Positions { get; }
		ComissionerRepository Comissioners { get; }
		
	}
}