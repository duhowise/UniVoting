using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVoting.Model;

namespace UniVoting.Data.Configurations
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Candidates)
                   .WithOne(c => c.Position)
                   .HasForeignKey(c => c.PositionId);
            builder.HasMany(p => p.SkippedVotes)
                   .WithOne(sv => sv.Position)
                   .HasForeignKey(sv => sv.Positionid);
            builder.HasMany(p => p.Votes)
                   .WithOne(v => v.Position)
                   .HasForeignKey(v => v.PositionId);
        }
    }
}
