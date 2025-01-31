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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            // Primary Key Configuration
            builder.HasKey(c => c.Id);

            // Property Configurations
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.CountryCode)
                .HasMaxLength(10) // Optional: Can adjust based on country code length
                .IsRequired(false);

            builder.Property(c => c.FlagCode)
                .HasMaxLength(10)
                .IsRequired(false);

            builder.Property(c => c.DialCode)
                .HasMaxLength(10)
                .IsRequired(false);

            // BaseEntity properties (if applicable)
            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(c => c.UpdatedAt)
                .IsRequired(false);
        }
    }
}
