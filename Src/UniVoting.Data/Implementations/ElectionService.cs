using UniVoting.Data.Interfaces;

namespace UniVoting.Data.Implementations
{
	public class ElectionService : IService
	{
		private readonly ICandidateRepository _candidates;
		private readonly IVoterRepository _voters;
		private readonly IPositionRepository _positions;
		private readonly IComissionerRepository _comissioners;

		public ElectionService(ICandidateRepository candidates, IVoterRepository voters,
			IPositionRepository positions, IComissionerRepository comissioners)
		{
			_candidates = candidates;
			_voters = voters;
			_positions = positions;
			_comissioners = comissioners;
		}

		public ICandidateRepository Candidates => _candidates;
		public IVoterRepository Voters => _voters;
		public IPositionRepository Positions => _positions;
		public IComissionerRepository Comissioners => _comissioners;
	}
}