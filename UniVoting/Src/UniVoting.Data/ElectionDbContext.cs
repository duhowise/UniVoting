using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniVoting.Core;

namespace UniVoting.Data
{
    public class ElectionDbContext:DbContext
	{
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["VotingSystem"].ConnectionString);
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (
				var pb in modelBuilder.Model.GetEntityTypes()
					.SelectMany(t => t.GetProperties()
						.Where(p => p.ClrType == typeof(string)))
					.Select(p => modelBuilder.Entity(p.DeclaringEntityType.ClrType).Property(p.Name)))
			{
				pb.IsUnicode(false).HasMaxLength(256);
			}
			base.OnModelCreating(modelBuilder);
		}

		public virtual DbSet<Vote> Votes { get; set; }
		public virtual DbSet<Voter> Voters { get; set; }		
		public virtual DbSet<Candidate> Candidates { get; set; }		
		public virtual DbSet<Position> Positions { get; set; }	
		public virtual DbSet<Rank> Ranks { get; set; }	
		public virtual DbSet<SkippedVote> SkippedVoteses { get; set; }	
		public virtual DbSet<ElectionConfiguration> Settings { get; set; }	
		public virtual DbSet<Commissioner> Comissioners { get; set; }	
		
	}
}