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
    public class OfferedServiceCategoryConfiguration : IEntityTypeConfiguration<OfferedServiceCategory>
    {
        public void Configure(EntityTypeBuilder<OfferedServiceCategory> builder)
        {
            // Define the table name
            builder.ToTable("OfferedServiceCategories");

            // Define the primary key
            builder.HasKey(osc => osc.Id);

            // Define properties and their constraints
            builder.Property(osc => osc.CategoryName)
                   .IsRequired()  // Category name is required
                   .HasMaxLength(200);  // Optional: Set a maximum length for the category name

            builder.Property(osc => osc.IsActive)
                   .IsRequired()  // Status is required
                   .HasDefaultValue(true);  // Default to active if not specified

            // Define relationships

            // One-to-many relationship between OfferedServiceCategory and OfferedService
            builder.HasMany(osc => osc.OfferedServices)
                   .WithOne(os => os.Category)  // Each OfferedService belongs to one OfferedServiceCategory
                   .HasForeignKey(os => os.CategoryID)  // The OfferedService entity contains the CategoryID as a foreign key
                   .OnDelete(DeleteBehavior.Cascade);  // If an OfferedServiceCategory is deleted, related OfferedServices are deleted
        }
    }
}
