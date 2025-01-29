using Domain.Entity.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Map to the Users table (handled by IdentityUser)
            builder.ToTable("AspNetUsers");

            // Map the FullName property
            builder.Property(u => u.FullName)
                   .HasMaxLength(200)  // Optional: Set a max length for the FullName
                   .IsRequired(false);  // Set to false if FullName is not required (can be null)

            // Configure relationships

            // One-to-many relationship between ApplicationUser and Dispute
            builder.HasMany(u => u.Disputes)
                   .WithOne(d => d.ResolvedByUser)  // Assuming the Dispute has a ResolvedByUser navigation property
                   .HasForeignKey(d => d.ResolvedByAdminId)  // Dispute contains the ResolvedByAdminId as a foreign key
                   .OnDelete(DeleteBehavior.SetNull);  // If a user is deleted, set the ResolvedByAdminId to null (optional)

        }
    }
}
