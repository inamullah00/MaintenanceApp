using Maintenance.Domain.Entity.SettingEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maintenance.Infrastructure.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Key).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Value).IsRequired();
            builder.HasIndex(a => a.Key).IsUnique();
        }
    }
}
