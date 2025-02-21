using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class CandidateRepository:Repository<Candidate>
	{

		public CandidateRepository():base("VotingSystem")
		{
			
		}
	}
}