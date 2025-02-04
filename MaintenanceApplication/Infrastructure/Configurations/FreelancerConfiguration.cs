using Maintenance.Domain.Entity.FreelancerEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(f => f.Bio)
                .HasMaxLength(500) // Limit bio to 500 characters
                .IsRequired(false); // Bio is optional

            builder.Property(f => f.DateOfBirth)
                .IsRequired(); // DateOfBirth is required

            builder.Property(f => f.CivilID)
                .HasMaxLength(100) // Optional: max length for Civil ID
                .IsRequired(false); // Optional: civil ID

            
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


            // Relationships

            // One-to-Many: Freelancer to Bids
            builder.HasMany(f => f.Bids)
                .WithOne(b => b.Freelancer)
                .HasForeignKey(b => b.FreelancerId);

            // One-to-Many: Freelancer to Orders
            builder.HasMany(f => f.FreelancerOrders)
                .WithOne(o => o.Freelancers)
                .HasForeignKey(o => o.FreelancerId);

            // One-to-Many: Freelancer to Feedbacks
            builder.HasMany(f => f.ClientFeedbacks)
                .WithOne(feedback => feedback.FeedbackOnFreelancer)
                .HasForeignKey(feedback => feedback.FeedbackOnFreelancerId);

            // Many-to-Many: Freelancer to Services
            builder.HasMany(f => f.FreelancerServices)
                .WithOne(fts => fts.Freelancer)
                .HasForeignKey(fts => fts.FreelancerId);

            // One-to-Many: Freelancer to Country
            builder.HasOne(f => f.Country)
                .WithMany()
                .HasForeignKey(f => f.CountryId)
                .IsRequired(false);

        }
    }
}
