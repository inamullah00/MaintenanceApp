using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{
    public class FreelancerTopServicesConfiguration : IEntityTypeConfiguration<FreelancerTopServices>
    {
        public void Configure(EntityTypeBuilder<FreelancerTopServices> builder)
        {
            // Primary Key Configuration
            builder.HasKey(fts => fts.Id);

            // Configure Freelancer relationship (Many-to-One)
            builder.HasOne(fts => fts.Freelancer)
                .WithMany(f => f.FreelancerTopServices)
                .HasForeignKey(fts => fts.FreelancerId);
            
            // Configure Service relationship (Many-to-One)
            builder.HasOne(fts => fts.Service)
                .WithMany(s => s.FreelancerTopServices)
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
