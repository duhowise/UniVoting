using UniVoting.Data.Implementations;

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