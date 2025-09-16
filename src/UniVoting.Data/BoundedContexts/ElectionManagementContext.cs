using Microsoft.EntityFrameworkCore;
using UniVoting.Model;
using UniVoting.Data.BoundedContexts.ElectionManagement;

namespace UniVoting.Data.BoundedContexts
{
    /// <summary>
    /// Election Management Bounded Context
    /// Handles election setup, position management, candidate registration, and commissioner activities
    /// </summary>
    public class ElectionManagementContext : BaseContext<ElectionManagementContext>
    {
        public ElectionManagementContext(DbContextOptions<ElectionManagementContext> options) : base(options) { }

        // Core entities for election management
        public DbSet<ElectionPosition> Positions { get; set; }
        public DbSet<ElectionCandidate> Candidates { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Commissioner> Commissioners { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply configurations for election management entities
            modelBuilder.ApplyConfiguration(new Configurations.PositionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RankConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CommissionerConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SettingConfiguration());

            // Ignore entities not relevant to election management
            modelBuilder.Ignore<Voter>();
            modelBuilder.Ignore<Vote>();
            modelBuilder.Ignore<SkippedVotes>();
        }
    }
}