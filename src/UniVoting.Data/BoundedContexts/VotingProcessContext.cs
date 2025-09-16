using Microsoft.EntityFrameworkCore;
using UniVoting.Model;
using UniVoting.Data.BoundedContexts.VotingProcess;

namespace UniVoting.Data.BoundedContexts
{
    /// <summary>
    /// Voting Process Bounded Context
    /// Handles voter registration, authentication, and the actual voting process
    /// </summary>
    public class VotingProcessContext : BaseContext<VotingProcessContext>
    {
        public VotingProcessContext(DbContextOptions<VotingProcessContext> options) : base(options) { }

        // Core entities for voting process
        public DbSet<VotingVoter> Voters { get; set; }
        public DbSet<VotingPosition> Positions { get; set; }  // Read-only access to positions for voting
        public DbSet<VotingCandidate> Candidates { get; set; }  // Read-only access to candidates for voting
        public DbSet<SkippedVotes> SkippedVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for voting process entities
            modelBuilder.ApplyConfiguration(new Configurations.VoterConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PositionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SkippedVotesConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RankConfiguration());

            // Ignore entities not relevant to voting process
            modelBuilder.Ignore<Vote>();  // Votes are handled in results context
            modelBuilder.Ignore<Commissioner>();
            modelBuilder.Ignore<Setting>();
        }
    }
}