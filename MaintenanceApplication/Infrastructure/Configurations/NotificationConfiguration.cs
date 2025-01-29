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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            // Set the table name (optional)
            builder.ToTable("Notifications");

            // Primary Key Configuration
            builder.HasKey(n => n.Id);

            // Property Configurations
            builder.Property(n => n.Message)
                .IsRequired() // Make the Message required
                .HasMaxLength(500); // Limit the message length to 500 characters

            //builder.Property(n => n.IsRead)
            //    .HasDefaultValue(false); // Default value for IsRead is false (notification unread by default)

            //builder.Property(n => n.CreatedAt)
            //    .HasDefaultValueSql("GETUTCDATE()") // Automatically set CreatedAt to UTC now if not set
            //    .IsRequired();

            // Relationships Configuration

            // One-to-many relationship between Freelancer and Notification (Freelancer can have many Notifications)
            builder.HasOne(n => n.Freelancer) // Each notification is related to one freelancer (nullable)
                .WithMany() // Freelancer can have many notifications (no back reference from Freelancer to Notifications)
                .HasForeignKey(n => n.FreelancerId); // FreelancerId is the foreign key in the Notification entity
                //.OnDelete(DeleteBehavior.SetNull); // If Freelancer is deleted, set the FreelancerId to null (no cascade delete)
        }
    }
}
