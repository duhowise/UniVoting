using UniVoting.Data.Interfaces;

namespace UniVoting.Data.Implementations
{
	public class ElectionService : IService
	{
		private CandidateRepository _candidates;
		private VoterRepository _voters;
		private PositionRepository _positions;
		private ComissionerRepository _comissioners;

		public CandidateRepository Candidates => _candidates ?? (_candidates = new CandidateRepository());

		public VoterRepository Voters => _voters ?? (_voters = new VoterRepository());


		public PositionRepository Positions => _positions ?? (_positions = new PositionRepository());
		

		public ComissionerRepository Comissioners=>_comissioners??(_comissioners=new ComissionerRepository());
		
	}
}