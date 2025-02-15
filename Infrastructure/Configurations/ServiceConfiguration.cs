using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maintenance.Infrastructure.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.IsUserCreated).IsRequired();
            builder.Property(s => s.IsApproved).IsRequired();
            builder.Property(s => s.IsActive).IsRequired();

            builder.HasMany(s => s.FreelancerServices).WithOne(fts => fts.Service).HasForeignKey(fts => fts.ServiceId);
            builder.HasOne(a => a.ActionBy).WithMany().HasForeignKey(a => a.ActionById).OnDelete(DeleteBehavior.NoAction);

        }
    }
}
