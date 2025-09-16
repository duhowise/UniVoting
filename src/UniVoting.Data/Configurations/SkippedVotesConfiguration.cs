using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVoting.Model;

namespace UniVoting.Data.Configurations
{
    public class SkippedVotesConfiguration : IEntityTypeConfiguration<SkippedVotes>
    {
        public void Configure(EntityTypeBuilder<SkippedVotes> builder)
        {
            builder.HasKey(sv => sv.Id);
            builder.HasOne(sv => sv.Voter)
                   .WithMany()
                   .HasForeignKey(sv => sv.VoterId);
            builder.HasOne(sv => sv.Position)
                   .WithMany(p => p.SkippedVotes)
                   .HasForeignKey(sv => sv.Positionid);
        }
    }
}
