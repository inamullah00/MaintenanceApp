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

            // Property Configurations
            builder.Property(b => b.CustomPrice)
                .HasColumnType("decimal(18, 2)") // Optional: Ensure that the price is stored as a decimal with 2 decimal places
                .IsRequired(); // Make CustomPrice a required field

            builder.Property(b => b.CurrentRating)
                .HasColumnType("float") // Configure as a float for average rating
                .IsRequired(false); // Rating is not required, it can be null initially

            builder.Property(b => b.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()") // Ensure that the CreatedAt column is automatically set to UTC now if not set
                .IsRequired();

            builder.Property(b => b.ArrivalTime)
                .IsRequired(false); // ArrivalTime is optional

            builder.Property(b => b.Message)
                .HasMaxLength(500) // Optional: Add max length to Message field
                .IsRequired(false); // Message is optional

            builder.Property(b => b.BidDate)
                .IsRequired(false); // BidDate is optional

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
                //.OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior if necessary
        }
    }
}
