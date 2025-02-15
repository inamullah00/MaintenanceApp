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
    public class DisputeConfiguration : IEntityTypeConfiguration<Dispute>
    {
        public void Configure(EntityTypeBuilder<Dispute> builder)
        {
            // Define the table name
            builder.ToTable("Disputes");

            // Define the primary key
            builder.HasKey(d => d.Id);

            // Define properties and their constraints
            builder.Property(d => d.DisputeDescription)
                   .IsRequired()  // Dispute Description is required
                   .HasMaxLength(1000); // Optional: Limit the length of the description

            builder.Property(d => d.ResolutionDetails)
                   .IsRequired()  // Resolution details are required
                   .HasMaxLength(1000); // Optional: Limit the length of resolution details

            builder.Property(d => d.DisputeType)
                   .IsRequired();  // Dispute type (Service, Quality, Payment) is required

            builder.Property(d => d.DisputeStatus)
                   .IsRequired()  // Dispute status (Pending, Resolved, Closed) is required
                   .HasDefaultValue(DisputeStatus.InProgress); // Default value is Pending

            builder.Property(d => d.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")  // Default value for CreatedAt is current date
                   .IsRequired();  // CreatedAt is required

            // ResolvedByAdminId: Foreign Key to ApplicationUser (Admin)
            builder.HasOne(d => d.ResolvedByUser)
                   .WithMany()  // Assuming one admin can resolve multiple disputes
                   .HasForeignKey(d => d.ResolvedByAdminId)
                   .OnDelete(DeleteBehavior.SetNull);  // If admin is deleted, set ResolvedByAdminId to null

            // Define relationship with Order (Dispute to Order)
            builder.HasOne(d => d.Order)
                   .WithMany()  // Assuming one Order can have multiple Disputes
                   .HasForeignKey(d => d.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);  // If an Order is deleted, related Disputes are deleted

            // Optional: Index on the DisputeStatus for better querying by status
            builder.HasIndex(d => d.DisputeStatus);
        }
    }
}
