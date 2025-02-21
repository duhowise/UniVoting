using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniVoting.Model;

namespace UniVoting.Core
{
	public class ElectionDbContext:DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["VotingConnection"].ConnectionString);
			base.OnConfiguring(optionsBuilder);
		}


		public virtual DbSet<Vote> Votes { get; set; }
		public virtual DbSet<Voter> Voters { get; set; }		
		public virtual DbSet<Candidate> Candidates { get; set; }		
		public virtual DbSet<Position> Positions { get; set; }	
		public virtual DbSet<Rank> Ranks { get; set; }	
		public virtual DbSet<SkippedVotes> SkippedVoteses { get; set; }	
		
	}
}