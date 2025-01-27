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
    public class FreelancerConfiguration : IEntityTypeConfiguration<Freelancer>
    {
        public void Configure(EntityTypeBuilder<Freelancer> builder)
        {
            // Set the table name (optional)
            builder.ToTable("Freelancers");

            // Primary Key Configuration
            builder.HasKey(f => f.Id);

            // Property Configurations
            builder.Property(f => f.FullName)
                .HasMaxLength(100) // Set a max length for FullName
                .IsRequired(); // Make FullName required

            builder.Property(f => f.Email)
                .HasMaxLength(100) // Set max length for Email
                .IsRequired(); // Email is required for the freelancer

            builder.Property(f => f.Password)
                .HasMaxLength(100) // Optional: Ensure the password has a max length
                .IsRequired(); // Password is required

            builder.Property(f => f.PhoneNumber)
                .HasMaxLength(20) // Limit phone number length
                .IsRequired(false); // Optional: phone number is not required

            builder.Property(f => f.ProfilePicture)
                .HasMaxLength(500) // Optional: Limit the length of ProfilePicture URL if stored as a string
                .IsRequired(false); // Optional: profile picture

            builder.Property(f => f.AreaOfExpertise)
                .HasMaxLength(100) // Optional: Set max length for AreaOfExpertise
                .IsRequired(false); // Optional: the area of expertise

            builder.Property(f => f.Bio)
                .HasMaxLength(500) // Limit bio to 500 characters
                .IsRequired(false); // Bio is optional

            builder.Property(f => f.DateOfBirth)
                .IsRequired(); // DateOfBirth is required

            builder.Property(f => f.Country)
                .HasMaxLength(100) // Optional: max length for country name
                .IsRequired(false); // Optional: country

            builder.Property(f => f.CivilID)
                .HasMaxLength(100) // Optional: max length for Civil ID
                .IsRequired(false); // Optional: civil ID

            builder.Property(f => f.ExperienceLevel)
                .HasMaxLength(50) // Optional: limit the experience level string
                .IsRequired(false); // Optional: experience level

            builder.Property(f => f.PreviousWork)
                .HasMaxLength(500) // Limit the previous work links to 500 characters
                .IsRequired(false); // Optional: previous work

            builder.Property(f => f.Status)
                .HasMaxLength(20) // Limit status to 20 characters
                .IsRequired(); // Status is required

            builder.Property(f => f.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()") // Automatically set CreatedAt to UTC now if not set
                .IsRequired();

            builder.Property(f => f.UpdatedAt)
                .IsRequired(false); // UpdatedAt is optional

            // Relationships Configuration

            // One-to-many relationship between Freelancer and Bid
            builder.HasMany(f => f.Bids)
                .WithOne(b => b.Freelancer) // Each bid is linked to one freelancer
                .HasForeignKey(b => b.FreelancerId); // FreelancerId is the foreign key in the Bid entity
                //.OnDelete(DeleteBehavior.Cascade); // If freelancer is deleted, delete related bids (optional)

            // One-to-many relationship between Freelancer and Order
            builder.HasMany(f => f.FreelancerOrders)
                .WithOne(o => o.Freelancers) // Each order is related to one freelancer
                .HasForeignKey(o => o.FreelancerId); // FreelancerId is the foreign key in the Order entity
                //.OnDelete(DeleteBehavior.Cascade); // Cascade delete if needed (optional)

            // One-to-many relationship between Freelancer and Feedback (ClientFeedbacks)
            builder.HasMany(f => f.ClientFeedbacks)
                .WithOne(feedback => feedback.Freelancer) // Each feedback is related to one freelancer
                .HasForeignKey(feedback => feedback.FeedbackOnFreelancerId); // FeedbackOnFreelancerId is the foreign key in the Feedback entity
                //.OnDelete(DeleteBehavior.Cascade); // Cascade delete if needed (optional)

        }
    }
}
