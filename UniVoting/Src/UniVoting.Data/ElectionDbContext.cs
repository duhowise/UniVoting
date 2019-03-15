using System.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UniVoting.Core;

namespace Univoting.Data
{
    public class ElectionDbContext : DbContext
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

            modelBuilder.Entity<Voter>().HasMany(c => c.SkippedVotes).WithOne(x => x.Voter).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Voter>().HasMany(c => c.Votes).WithOne(x => x.Voter).OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Vote> Votes { get; set; }
        public virtual DbSet<Voter> Voters { get; set; }
        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<SkippedVote> SkippedVotes { get; set; }
        public virtual DbSet<ElectionConfiguration> ElectionConfigurations { get; set; }
        public virtual DbSet<Commissioner> Commissioners { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<PollingStation> PollingStations { get; set; }

    }
}