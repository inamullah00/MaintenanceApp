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
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {

            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);


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

            // Optional: Adding constraints for unique fields like Email or PhoneNumber
            builder.HasIndex(c => c.Email).IsUnique();  // Email should be unique
            builder.HasIndex(c => c.PhoneNumber).IsUnique();  // PhoneNumber should be unique
        }

    }
}
