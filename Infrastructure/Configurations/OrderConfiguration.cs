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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            // Define relationship with Client
            builder.HasOne(o => o.Client) // An Order has one Client
                   .WithMany(c => c.ClientOrders) // A Client can have many Orders
                   .HasForeignKey(o => o.ClientId); // Order contains ClientId as foreign key
                                                    //.OnDelete(DeleteBehavior.Restrict); // Optional: You can choose delete behavior (SetNull, Cascade, etc.)

            // Define relationship with Freelancer
            builder.HasOne(o => o.Freelancers) // An Order has one Freelancer
                   .WithMany(f => f.FreelancerOrders) // A Freelancer can have many Orders
                   .HasForeignKey(o => o.FreelancerId); // Order contains FreelancerId as foreign key
                                                        //.OnDelete(DeleteBehavior.Restrict); // Optional: SetNull, Cascade, etc.

            // Define relationship with OfferedService
            builder.HasOne(o => o.Service) // An Order is associated with one OfferedService
                   .WithMany() // You can specify the inverse relationship if needed
                   .HasForeignKey(o => o.ServiceId); // Order contains ServiceId as foreign key
                                                     //.OnDelete(DeleteBehavior.Restrict); // Optional: SetNull, Cascade, etc.

            // Define relationship with Feedback (one-to-many)
            builder.HasMany(o => o.Feedbacks) // An Order has many Feedbacks
                   .WithOne(f => f.Order) // A Feedback is for one Order
                   .HasForeignKey(f => f.OrderId); // Feedback contains OrderId as foreign key
                                                   //.OnDelete(DeleteBehavior.Cascade); // Optional: Feedback will be deleted if Order is deleted

            // Define relationship with Dispute (one-to-many)
            builder.HasMany(o => o.Disputes) // An Order can have many Disputes
                   .WithOne(d => d.Order) // A Dispute is associated with one Order
                   .HasForeignKey(d => d.OrderId); // Dispute contains OrderId as foreign key
                   //.OnDelete(DeleteBehavior.Cascade); // Optional: Disputes will be deleted if Order is deleted

            // Set up additional indexes or constraints if needed
            builder.HasIndex(o => o.ClientId); // Optional: Index on ClientId
            builder.HasIndex(o => o.FreelancerId); // Optional: Index on FreelancerId
            builder.HasIndex(o => o.ServiceId); // Optional: Index on ServiceId



            builder.Property(o => o.Budget)
                   .HasColumnType("decimal(18,2)") // Set column type for budget
                   .IsRequired();


            builder.Property(o => o.Status)
                   .IsRequired(); // Set Status as required

            builder.Property(o => o.CreatedAt)
                   .HasDefaultValueSql("GETDATE()"); // Default to current date if not set

            builder.Property(o => o.UpdatedAt)
                   .HasDefaultValueSql("GETDATE()"); // Default to current date if not set

        }
    }
}
