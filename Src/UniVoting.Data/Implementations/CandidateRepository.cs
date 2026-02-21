using UniVoting.Data.Interfaces;
using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class CandidateRepository : Repository<Candidate>, ICandidateRepository
	{

		public CandidateRepository():base("VotingSystem")
		{
			
		}
	}
}