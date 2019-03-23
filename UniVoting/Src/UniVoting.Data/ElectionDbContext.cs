using System.Collections.Generic;
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

            modelBuilder.Entity<Rank>().HasData(new List<Rank>
            {
                new Rank{Id=1,VoterRank = 1},
                new Rank{Id=2,VoterRank = 2},
                new Rank{Id=3,VoterRank = 3},
                new Rank{Id=4,VoterRank = 4},
                new Rank{Id=5,VoterRank = 5},
                new Rank{Id=6,VoterRank = 6},
                new Rank{Id=7,VoterRank = 7},
                new Rank{Id=8,VoterRank = 8},
                new Rank{Id=9,VoterRank = 9},
                new Rank{Id=10,VoterRank = 10},
            });
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