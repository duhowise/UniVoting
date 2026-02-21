using Microsoft.EntityFrameworkCore;
using UniVoting.Model;

namespace UniVoting.Core
{
	public class ElectionDbContext : DbContext
	{
		public ElectionDbContext(DbContextOptions<ElectionDbContext> options) : base(options)
		{
		}

		public virtual DbSet<Vote> Votes { get; set; }
		public virtual DbSet<Voter> Voters { get; set; }
		public virtual DbSet<Candidate> Candidates { get; set; }
		public virtual DbSet<Position> Positions { get; set; }
		public virtual DbSet<Rank> Ranks { get; set; }
		public virtual DbSet<SkippedVotes> SkippedVoteses { get; set; }
	}
}