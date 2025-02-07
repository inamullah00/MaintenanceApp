using Maintenance.Domain.Entity.FreelancerEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{

    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            // Set the table name (optional)
            builder.ToTable("Packages");

            // Primary Key Configuration
            builder.HasKey(p => p.Id);

            // Property Configurations
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();

            builder.Property(p => p.OfferDetails)
                .IsRequired(false)
                .HasMaxLength(500);

            // One-to-Many Relationship: Freelancer -> Packages
            builder.HasOne(p => p.Freelancer)
                .WithMany(f => f.Packages) // Ensure Freelancer has a collection of Packages
                .HasForeignKey(p => p.FreelancerId);
            
            // Many-to-Many Relationship: Package <-> Bid (via BidPackage)
            builder.HasMany(p => p.BidPackages)
                .WithOne(bp => bp.Package)
                .HasForeignKey(bp => bp.PackageId);
        }
    }
}
