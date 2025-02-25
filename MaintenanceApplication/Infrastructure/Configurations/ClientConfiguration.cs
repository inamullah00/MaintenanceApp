using Maintenance.Domain.Entity.ClientEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maintenance.Infrastructure.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {

            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName).IsRequired().HasMaxLength(255);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(255);
            builder.Property(c => c.PhoneNumber).HasMaxLength(50);
            builder.Property(c => c.Password).IsRequired().HasMaxLength(255);
            builder.Property(c => c.ProfilePicture).IsRequired(false).HasMaxLength(500);
            builder.Property(c => c.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(a => a.IsActive).IsRequired(false).HasDefaultValue(true);

            // One-to-many relationship with Orders
            builder.HasMany(c => c.ClientOrders) // Client has many Orders
                   .WithOne(o => o.Client) // Each Order has one Client
                   .HasForeignKey(o => o.ClientId) // Order contains ClientId as foreign key
                   .OnDelete(DeleteBehavior.SetNull); // Optional: Set null or cascade delete

            // One-to-many relationship with Feedback
            builder.HasMany(c => c.TotalProvidedFeedbacksByClient)
                   .WithOne(f => f.Client) // Each Feedback is given by one Client
                   .HasForeignKey(f => f.FeedbackByClientId) // Feedback contains FeedbackByClientId as foreign key
                   .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete or set null

            // One-to-Many: Freelancer to Country
            builder.HasOne(f => f.Country)
            .WithMany()
                .HasForeignKey(f => f.CountryId)
                .IsRequired(false);

            builder.HasOne(f => f.ActionBy).WithMany().HasForeignKey(f => f.ActionById).OnDelete(DeleteBehavior.NoAction);

            // One - to - many relationship with ClientOtp
            builder.HasMany(c => c.clientOtps) // Client has many OTPs
                   .WithOne(o => o.Client) // Each OTP is linked to one Client
                   .HasForeignKey(o => o.ClientId);

            // One-to-Many relationship with ClientAddress
            builder.HasMany(c => c.ClientAddresses)
                   .WithOne(a => a.Client)
                   .HasForeignKey(a => a.ClientId);


            // Optional: Adding constraints for unique fields like Email or PhoneNumber
            builder.HasIndex(c => c.Email).IsUnique();  // Email should be unique
            builder.HasIndex(c => c.PhoneNumber).IsUnique();  // PhoneNumber should be unique
        }

    }
}
