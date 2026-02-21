using Microsoft.EntityFrameworkCore;
using UniVoting.Model;

namespace UniVoting.Data
{
    public class ElectionDbContext : DbContext
    {
        public ElectionDbContext(DbContextOptions<ElectionDbContext> options) : base(options) { }

        public DbSet<Vote> Votes { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<SkippedVotes> SkippedVoteses { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Comissioner> Comissioners { get; set; }
        public DbSet<LiveViewCount> LiveViewCounts { get; set; }
        public DbSet<LiveViewSkippedCount> LiveViewSkippedCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map entities to their actual SQL Server table names
            modelBuilder.Entity<Comissioner>().ToTable("Comissioner");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<Candidate>().ToTable("Candidate");
            modelBuilder.Entity<Voter>().ToTable("Voter");
            modelBuilder.Entity<Vote>().ToTable("Vote");
            modelBuilder.Entity<SkippedVotes>().ToTable("SkippedVotes");
            modelBuilder.Entity<Setting>().ToTable("Settings");
            modelBuilder.Entity<Rank>().ToTable("Rank").HasKey(r => r.id);

            // Keyless view entities
            modelBuilder.Entity<LiveViewCount>().HasNoKey().ToView("vw_LiveView");
            modelBuilder.Entity<LiveViewSkippedCount>().HasNoKey().ToView("vw_LiveViewSkipped");
        }
    }

    /// <summary>Keyless entity mapped to dbo.vw_LiveView (columns: PositionName, Count).</summary>
    public class LiveViewCount
    {
        public string PositionName { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    /// <summary>Keyless entity mapped to dbo.vw_LiveViewSkipped (columns: PositionName, Count).</summary>
    public class LiveViewSkippedCount
    {
        public string PositionName { get; set; } = string.Empty;
        public int Count { get; set; }
    }
}
