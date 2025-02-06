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

    public class ClientOtpConfiguration : IEntityTypeConfiguration<ClientOtp>
    {
        public void Configure(EntityTypeBuilder<ClientOtp> builder)
        {
            // Map to "ClientOtps" table
            builder.ToTable("ClientOtps");

            // Primary Key
            builder.HasKey(c => c.Id);

            // Properties configuration
            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255); // Email is required and should not exceed 255 characters

            builder.Property(c => c.OtpCode)
                .IsRequired()
                .HasMaxLength(6); // OTP code should have a max length of 6 characters

            builder.Property(c => c.CreatedAt)
                .IsRequired(); // Ensure CreatedAt is required

            builder.Property(c => c.ExpiresAt)
                .IsRequired(); // Ensure ExpiresAt is required

            builder.Property(c => c.IsUsed)
                .IsRequired()
                .HasDefaultValue(false); // Set default value for IsUsed as false

            builder.Property(c => c.ClientId)
                .IsRequired(); // Ensure ClientId is required

            // Relationship configuration: One-to-One (ClientOtp to Client)
            builder.HasOne(c => c.Client)
                .WithMany() // Assuming Client can have many OTPs
                .HasForeignKey(c => c.ClientId);

            // Optional: Index for Email to ensure uniqueness
            builder.HasIndex(c => c.Email).IsUnique(); // Ensure Email is unique within OTP records

        }
    }
}
