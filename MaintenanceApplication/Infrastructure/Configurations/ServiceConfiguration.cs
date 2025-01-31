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
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {

            // Primary Key Configuration
            builder.HasKey(s => s.Id);

            // Property Configurations
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd(); // Ensure the ID is auto-generated

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100); // Ensure a reasonable max length for service names

            // Configure relationships (Navigation properties)
            builder.HasMany(s => s.FreelancerTopServices)
                .WithOne(fts => fts.Service)
                .HasForeignKey(fts => fts.ServiceId);
        }
    }
}
