using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVoting.Model;

namespace UniVoting.Data.Configurations
{
    public class RankConfiguration : IEntityTypeConfiguration<Rank>
    {
        public void Configure(EntityTypeBuilder<Rank> builder)
        {
            builder.HasKey(r => r.id);
            builder.HasMany(r => r.Candidates)
                   .WithOne(c => c.Rank)
                   .HasForeignKey(c => c.RankId);
        }
    }
}
