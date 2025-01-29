using Maintenance.Domain.Entity.ClientEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{
    public class OffereServiceConfiguration : IEntityTypeConfiguration<OfferedService>
    {
        public void Configure(EntityTypeBuilder<OfferedService> builder)
        {
            // Define the table name
            builder.ToTable("OfferedServices");

            // Define the primary key
            builder.HasKey(os => os.Id);

            // Define properties and their constraints
            builder.Property(os => os.Title)
                   .IsRequired()  // Title is required
                   .HasMaxLength(200);  // You can specify a max length for the title

            builder.Property(os => os.Description)
                   .IsRequired()  // Description is required
                   .HasMaxLength(1000);  // Optional: Set a maximum length for description

            builder.Property(os => os.Location)
                   .HasMaxLength(500);  // Optional: Set a maximum length for the location

            builder.Property(os => os.Building)
                   .HasMaxLength(100);  // Optional: Set a max length for the building

            builder.Property(os => os.Apartment)
                   .HasMaxLength(100);  // Optional: Set a max length for the apartment

            builder.Property(os => os.Floor)
                   .HasMaxLength(100);  // Optional: Set a max length for the floor

            builder.Property(os => os.Street)
                   .HasMaxLength(100);  // Optional: Set a max length for the street

            builder.Property(os => os.SetAsCurrentHomeAddress)
                   .IsRequired();  // Whether this service is set as the current home address

            builder.Property(os => os.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")  // Set the default to the current date/time
                   .IsRequired();  // CreatedAt is required

            builder.Property(os => os.UpdatedAt)
                   .IsRequired(false);  // UpdatedAt is nullable

            // Define relationships

            // One-to-many relationship between OfferedService and Orders
            builder.HasMany(os => os.Orders)
                   .WithOne(o => o.Service)  // Each Order has one OfferedService
                   .HasForeignKey(o => o.ServiceId);  // Order contains a foreign key for OfferedService
                   //.OnDelete(DeleteBehavior.Cascade);  // If an OfferedService is deleted, its related orders are also deleted

            // One-to-many relationship between OfferedService and Bids
            builder.HasMany(os => os.Bids)
                   .WithOne(b => b.OfferedService)  // Each Bid is related to one OfferedService
                   .HasForeignKey(b => b.OfferedServiceId);  // Bid contains a foreign key for OfferedService
                   //.OnDelete(DeleteBehavior.Cascade);  // If an OfferedService is deleted, its related bids are also deleted

            // Many-to-one relationship with OfferedServiceCategory
            builder.HasOne(os => os.Category)
                   .WithMany()  // One OfferedServiceCategory can have multiple OfferedServices
                   .HasForeignKey(os => os.CategoryID);
                   //.OnDelete(DeleteBehavior.SetNull);  // If a category is deleted, the CategoryId is set to null

            // Many-to-one relationship with ApplicationUser (Client)
            builder.HasOne(os => os.Client)
                   .WithMany()  // One ApplicationUser (Client) can offer multiple services
                   .HasForeignKey(os => os.ClientId);
                   //.OnDelete(DeleteBehavior.Cascade);  // If a client is deleted, their related services are also deleted

        }
    }
}
