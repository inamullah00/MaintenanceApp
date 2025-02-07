using Maintenance.Domain.Entity.FreelancerEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            // Set the table name (optional)
            builder.ToTable("Bids");

            // Primary Key Configuration
            builder.HasKey(b => b.Id);

            
            builder.Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()") // Ensure that the CreatedAt column is automatically set to UTC now if not set
                .IsRequired();

            
            // Enum property configurations (optional)
            builder.Property(b => b.BidStatus)
                .HasConversion<string>() // Convert BidStatus enum to string in the database
                .IsRequired();

            // Configure relationships (Navigation properties)

            // One-to-many relationship between Bid and OfferedService
            builder.HasOne(b => b.OfferedService)
                .WithMany(os => os.Bids) // OfferedService has many bids
                .HasForeignKey(b => b.OfferedServiceId); // Foreign key to OfferedService
                                                         //.OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior if necessary (or use Cascade if needed)

            // One-to-many relationship between Bid and Freelancer
            builder.HasOne(b => b.Freelancer)
                .WithMany(f => f.Bids) // Freelancer has many bids
                .HasForeignKey(b => b.FreelancerId); // Foreign key to Freelancer

            // Many-to-Many Relationship: Bid <-> Package (via BidPackage)
            builder.HasMany(b => b.BidPackages)
                .WithOne(bp => bp.Bid)
                .HasForeignKey(bp => bp.BidId);
        }
    }
}
