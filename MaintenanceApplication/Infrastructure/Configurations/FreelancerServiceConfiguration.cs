using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maintenance.Infrastructure.Configurations
{
    public class FreelancerServiceConfiguration : IEntityTypeConfiguration<FreelancerService>
    {
        public void Configure(EntityTypeBuilder<FreelancerService> builder)
        {
            // Primary Key Configuration
            builder.HasKey(fts => fts.Id);

            // Configure Freelancer relationship (Many-to-One)
            builder.HasOne(fts => fts.Freelancer)
                .WithMany(f => f.FreelancerServices)
                .HasForeignKey(fts => fts.FreelancerId);

            // Configure Service relationship (Many-to-One)
            builder.HasOne(fts => fts.Service)
                .WithMany(s => s.FreelancerServices)
                .HasForeignKey(fts => fts.ServiceId);

            // BaseEntity properties (if applicable)
            builder.Property(fts => fts.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(fts => fts.UpdatedAt)
                .IsRequired(false);
        }
    }
}
