using UniVoting.Data.Interfaces;

namespace UniVoting.Data.Implementations
{
	public class ElectionService : IService
	{
		public CandidateRepository Candidates { get; }
		public VoterRepository Voters { get; }
		public PositionRepository Positions { get; }
		public ComissionerRepository Comissioners { get; }

		// Constructor injection
		public ElectionService(
			CandidateRepository candidates,
			VoterRepository voters,
			PositionRepository positions,
			ComissionerRepository comissioners)
		{
			Candidates = candidates;
			Voters = voters;
			Positions = positions;
			Comissioners = comissioners;
		}
	}
}