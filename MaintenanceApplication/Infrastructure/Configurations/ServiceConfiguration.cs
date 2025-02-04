using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maintenance.Infrastructure.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {

            // Primary Key Configuration
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100); // Ensure a reasonable max length for service names

            // Configure relationships (Navigation properties)
            builder.HasMany(s => s.FreelancerServices)
                .WithOne(fts => fts.Service)
                .HasForeignKey(fts => fts.ServiceId);
        }
    }
}
