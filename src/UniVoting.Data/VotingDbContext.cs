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
        public DbSet<Comissioner> Commissioners { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Voter
            modelBuilder.Entity<Voter>()
                .HasKey(v => v.Id);
            modelBuilder.Entity<Voter>()
                .HasMany(v => v.Votes)
                .WithOne(vt => vt.Voter)
                .HasForeignKey(vt => vt.VoterId);

            // Vote
            modelBuilder.Entity<Vote>()
                .HasKey(v => v.Id);
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Voter)
                .WithMany(vt => vt.Votes)
                .HasForeignKey(v => v.VoterId);
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Candidate)
                .WithMany(c => c.Votes)
                .HasForeignKey(v => v.CandidateId);
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Position)
                .WithMany(p => p.Votes)
                .HasForeignKey(v => v.PositionId);

            // SkippedVotes
            modelBuilder.Entity<SkippedVotes>()
                .HasKey(sv => sv.Id);
            modelBuilder.Entity<SkippedVotes>()
                .HasOne(sv => sv.Voter)
                .WithMany()
                .HasForeignKey(sv => sv.VoterId);
            modelBuilder.Entity<SkippedVotes>()
                .HasOne(sv => sv.Position)
                .WithMany(p => p.SkippedVotes)
                .HasForeignKey(sv => sv.Positionid);

            // Position
            modelBuilder.Entity<Position>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Position>()
                .HasMany(p => p.Candidates)
                .WithOne(c => c.Position)
                .HasForeignKey(c => c.PositionId);
            modelBuilder.Entity<Position>()
                .HasMany(p => p.SkippedVotes)
                .WithOne(sv => sv.Position)
                .HasForeignKey(sv => sv.Positionid);
            modelBuilder.Entity<Position>()
                .HasMany(p => p.Votes)
                .WithOne(v => v.Position)
                .HasForeignKey(v => v.PositionId);

            // Candidate
            modelBuilder.Entity<Candidate>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Position)
                .WithMany(p => p.Candidates)
                .HasForeignKey(c => c.PositionId);
            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Rank)
                .WithMany(r => r.Candidates)
                .HasForeignKey(c => c.RankId);
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.Votes)
                .WithOne(v => v.Candidate)
                .HasForeignKey(v => v.CandidateId);

            // Rank
            modelBuilder.Entity<Rank>()
                .HasKey(r => r.id);
            modelBuilder.Entity<Rank>()
                .HasMany(r => r.Candidates)
                .WithOne(c => c.Rank)
                .HasForeignKey(c => c.RankId);

            // Comissioner
            modelBuilder.Entity<Comissioner>()
                .HasKey(c => c.Id);

            // Setting
            modelBuilder.Entity<Setting>()
                .HasKey(s => s.Id);
        }
    }
}
