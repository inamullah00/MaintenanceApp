using Maintenance.Domain.Entity.SettingEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Maintenance.Infrastructure.Configurations
{

    public class ContactUsConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            // Set primary key
            builder.HasKey(e => e.Id);

            // Configure properties
            builder.Property(c => c.FullName)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(c => c.PhoneNumber)
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Message)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(c => c.IsRead)
                   .HasDefaultValue(false);

            // Optional relationships
            builder.HasOne(c => c.Freelancer)
                   .WithMany() // No navigation property in Customer
                   .HasForeignKey(c => c.FreelancerId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Client)
                   .WithMany()
                   .HasForeignKey(c => c.ClientId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.MarkAsReadBy)
                 .WithMany()
                 .HasForeignKey(a => a.MarkedAsReadByUser)
                 .OnDelete(DeleteBehavior.NoAction);

        }
    }



}