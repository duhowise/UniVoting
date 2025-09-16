using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVoting.Model;

namespace UniVoting.Data.Configurations
{
    public class VoterConfiguration : IEntityTypeConfiguration<Voter>
    {
        public void Configure(EntityTypeBuilder<Voter> builder)
        {
            builder.HasKey(v => v.Id);
            builder.HasMany(v => v.Votes)
                   .WithOne(vt => vt.Voter)
                   .HasForeignKey(vt => vt.VoterId);
        }
    }
}
