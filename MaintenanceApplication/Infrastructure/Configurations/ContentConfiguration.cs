using Maintenance.Domain.Entity.Dashboard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenance.Infrastructure.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {

            // Define the table name
            builder.ToTable("Contents");

            // Define the primary key
            builder.HasKey(c => c.Id);

            // Define properties and their constraints
            builder.Property(c => c.Title)
                   .IsRequired()  // Title is required
                   .HasMaxLength(255); // Optional: You can limit the max length of the Title

            builder.Property(c => c.Body)
                   .IsRequired()  // Body is required
                   .HasMaxLength(4000); // Optional: You can limit the max length of the Body

            // Enum mapping (ContentType)
            builder.Property(c => c.ContentType)
                   .IsRequired();  // The ContentType enum should be required

            builder.Property(c => c.IsActive)
                   .IsRequired()  // IsActive is required
                   .HasDefaultValue(true);  // Default value for IsActive is true (active by default)

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")  // Default value for CreatedAt is the current date
                   .IsRequired();  // CreatedAt is required

            // Define UpdatedAt property
            builder.Property(c => c.UpdatedAt)
                   .IsRequired(false);  // UpdatedAt is nullable, so it's not required

            // Optional: Define a unique index for the Title field (if needed)
            builder.HasIndex(c => c.Title)
                   .IsUnique(false);  // Index on Title if required
        }
    }
}
