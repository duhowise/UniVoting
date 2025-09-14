using UniVoting.Model;

namespace UniVoting.Data.Implementations
{
	public class CandidateRepository : Repository<Candidate>
	{
		// Default constructor for DI (uses VotingSystem connection)
		public CandidateRepository() : base("VotingSystem")
		{
		}

		// Constructor for custom connection name
		public CandidateRepository(string connectionName) : base(connectionName)
		{
		}
	}
}