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
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            // Define primary key for Feedback table
            builder.HasKey(f => f.Id);

            // Define relationship with Order
            builder.HasOne(f => f.Order) // A Feedback belongs to one Order
                   .WithMany(o => o.Feedbacks) // An Order can have many Feedbacks
                   .HasForeignKey(f => f.OrderId); // Feedback contains OrderId as foreign key
                 //.OnDelete(DeleteBehavior.Cascade); // Optional: Feedback will be deleted if Order is deleted

            // Define relationship with Client (Feedback is provided by a Client)
            builder.HasOne(f => f.Client) // A Feedback is given by one Client
                   .WithMany() // A Client can give many Feedbacks (if needed)
                   .HasForeignKey(f => f.FeedbackByClientId); // Feedback contains FeedbackByClientId as foreign key
                 //.OnDelete(DeleteBehavior.Restrict); // Optional: SetNull, Cascade, etc.

            // Define relationship with Freelancer (Feedback is for a Freelancer)
            builder.HasOne(f => f.Freelancer) // A Feedback is given to one Freelancer
                   .WithMany() // A Freelancer can receive many Feedbacks (if needed)
                   .HasForeignKey(f => f.FeedbackOnFreelancerId); // Feedback contains FeedbackOnFreelancerId as foreign key
                   //.OnDelete(DeleteBehavior.Restrict); // Optional: SetNull, Cascade, etc.

            // Configure properties
            //builder.Property(f => f.Rating)
            //       .IsRequired() // Rating is required
            //       .HasDefaultValue(1) // Default value for Rating (optional)
            //       .HasRange(1, 5); // Rating is limited to values between 1 and 5

            builder.Property(f => f.Comment)
                   .HasMaxLength(1000); // Optional: You can set a maximum length for the Comment

            builder.Property(f => f.CreatedAt)
                   .HasDefaultValueSql("GETDATE()"); // Default to current date if not set

            builder.Property(f => f.UpdatedAt)
                   .HasDefaultValueSql("GETDATE()"); // Default to current date if not set

        }
    }
}
