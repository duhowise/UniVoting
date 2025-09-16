using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVoting.Model;

namespace UniVoting.Data.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasOne(c => c.Position)
                   .WithMany(p => p.Candidates)
                   .HasForeignKey(c => c.PositionId);
            builder.HasOne(c => c.Rank)
                   .WithMany(r => r.Candidates)
                   .HasForeignKey(c => c.RankId);
            builder.HasMany(c => c.Votes)
                   .WithOne(v => v.Candidate)
                   .HasForeignKey(v => v.CandidateId);
        }
    }
}
