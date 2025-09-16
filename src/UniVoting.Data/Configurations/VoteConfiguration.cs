using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVoting.Model;

namespace UniVoting.Data.Configurations
{
    public class VoteConfiguration : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(v => v.Id);
            builder.HasOne(v => v.Voter)
                   .WithMany(vt => vt.Votes)
                   .HasForeignKey(v => v.VoterId);
            builder.HasOne(v => v.Candidate)
                   .WithMany(c => c.Votes)
                   .HasForeignKey(v => v.CandidateId);
            builder.HasOne(v => v.Position)
                   .WithMany(p => p.Votes)
                   .HasForeignKey(v => v.PositionId);
        }
    }
}
