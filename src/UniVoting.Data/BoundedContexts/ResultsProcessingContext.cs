using Microsoft.EntityFrameworkCore;
using UniVoting.Model;
using UniVoting.Data.BoundedContexts.ResultsProcessing;

namespace UniVoting.Data.BoundedContexts
{
    /// <summary>
    /// Results Processing Bounded Context
    /// Handles vote recording, tallying, and results analysis
    /// </summary>
    public class ResultsProcessingContext : BaseContext<ResultsProcessingContext>
    {
        public ResultsProcessingContext(DbContextOptions<ResultsProcessingContext> options) : base(options) { }

        // Core entities for results processing
        public DbSet<ResultsVote> Votes { get; set; }
        public DbSet<SkippedVotes> SkippedVotes { get; set; }
        
        // Read-only access to reference data needed for results
        public DbSet<ResultsPosition> Positions { get; set; }
        public DbSet<ResultsCandidate> Candidates { get; set; }
        public DbSet<ResultsVoter> Voters { get; set; }  // For vote validation and audit trails

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for results processing entities
            modelBuilder.ApplyConfiguration(new Configurations.VoteConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SkippedVotesConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PositionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VoterConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RankConfiguration());

            // Ignore entities not relevant to results processing
            modelBuilder.Ignore<Commissioner>();
            modelBuilder.Ignore<Setting>();
        }
    }
}