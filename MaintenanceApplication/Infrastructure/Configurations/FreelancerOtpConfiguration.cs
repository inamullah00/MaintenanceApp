using Maintenance.Domain.Entity.UserEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{

    public class FreelancerOtpConfiguration : IEntityTypeConfiguration<FreelancerOtp>
    {
        public void Configure(EntityTypeBuilder<FreelancerOtp> builder)
        {
            // Map to "FreelancerOtps" table
            builder.ToTable("FreelancerOtps");

            // Primary Key
            builder.HasKey(f => f.Id);

            // Properties configuration
            builder.Property(f => f.Email)
                .IsRequired()
                .HasMaxLength(255); // Email is required and should not exceed 255 characters

            builder.Property(f => f.OtpCode)
                .IsRequired()
                .HasMaxLength(6); // OTP code should have a max length of 6 characters

            builder.Property(f => f.CreatedAt)
                .IsRequired(); // Ensure CreatedAt is required

            builder.Property(f => f.ExpiresAt)
                .IsRequired(); // Ensure ExpiresAt is required

            builder.Property(f => f.IsUsed)
                .IsRequired()
                .HasDefaultValue(false); // Set default value for IsUsed as false

            builder.Property(f => f.FreelancerId)
                .IsRequired(); // Ensure FreelancerId is required

            // Relationship configuration: One-to-One (FreelancerOtp to Freelancer)
            builder.HasOne(f => f.Freelancer)
                .WithMany() // Assuming Freelancer can have many OTPs
                .HasForeignKey(f => f.FreelancerId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete or set null depending on business logic

            // Optional: Index for Email to ensure uniqueness
            builder.HasIndex(f => f.Email).IsUnique(); // Ensure Email is unique within OTP records
        }
    }
}
