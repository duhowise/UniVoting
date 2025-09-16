using Microsoft.EntityFrameworkCore;
using UniVoting.Model;

namespace UniVoting.Data
{
    public class VotingDbContext : DbContext
    {
        public VotingDbContext(DbContextOptions<VotingDbContext> options) : base(options) { }

        public DbSet<Voter> Voters { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<SkippedVotes> SkippedVotes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Commissioner> Commissioners { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.VoterConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.VoteConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SkippedVotesConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.PositionConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CandidateConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RankConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.CommissionerConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SettingConfiguration());
        }
    }
}
