using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniVoting.Model;

namespace UniVoting.Data.Configurations
{
    public class CommissionerConfiguration : IEntityTypeConfiguration<Commissioner>
    {
        public void Configure(EntityTypeBuilder<Commissioner> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
