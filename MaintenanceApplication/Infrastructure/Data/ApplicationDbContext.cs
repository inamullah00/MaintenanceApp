
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.Client;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.Freelancer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DbSets for all your models
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserOtp> UserOtps { get; set; }
        public DbSet<OfferedService> OfferedServices { get; set; }
        public DbSet<OfferedServiceCategory> OfferedServiceCategories { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<Notification> Notifications { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        //public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Ensure to call the base method



            // Cascade delete from Order to Client (no change)
            builder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(o => o.ClientOrders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cascade delete from Order to Freelancer (no change)
            builder.Entity<Order>()
                .HasOne(o => o.Freelancer)
                .WithMany(o => o.FreelancerOrders)
                .HasForeignKey(o => o.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Restrict cascade delete from Order to OfferedService to avoid cycle
            builder.Entity<Order>()
                .HasOne(o => o.Service)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.ServiceId)
                .OnDelete(DeleteBehavior.Cascade); // Changed to Restrict

            // Cascade delete from Bid to Freelancer
            builder.Entity<Bid>()
                .HasOne(b => b.Freelancer)
                .WithMany()
                .HasForeignKey(b => b.FreelancerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Restrict cascade delete from Bid to OfferedService to avoid cycle
            builder.Entity<Bid>()
                .HasOne(b => b.OfferedService)
                .WithMany()
                .HasForeignKey(b => b.OfferedServiceId)
                .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict


            // Relationship between Order and Dispute
            builder.Entity<Dispute>()
                .HasOne(d => d.Order)         // A Dispute belongs to an Order
                .WithMany(o => o.Disputes)    // An Order can have many Disputes
                .HasForeignKey(d => d.OrderId) // Foreign Key: Dispute's OrderId
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete from Order to Dispute (disputes should remain even if the order is deleted)


            // Relationship between Dispute and ApplicationUser (ResolvedBy)
            builder.Entity<Dispute>()
                .HasOne(d => d.ResolvedByUser)         // A Dispute has one ResolvedBy (Admin)
                .WithMany()                            // Admin can resolve many Disputes (no need for a navigation property in ApplicationUser)
                .HasForeignKey(d => d.ResolvedBy)      // Foreign Key: Dispute's ResolvedBy (AdminId)
                .OnDelete(DeleteBehavior.SetNull);     // If an admin is deleted, set ResolvedBy to null (dispute can still exist without the admin)



            //------------------------ Set precision for HourlyRate (DECIMAL(18, 2) in SQL Server)
            builder.Entity<ApplicationUser>()
                .Property(u => u.HourlyRate)
                .HasColumnType("DECIMAL(18,2)");

            builder.Entity<Bid>()
              .Property(u => u.BidAmount)
              .HasColumnType("DECIMAL(18,2)");


            builder.Entity<PerformanceReport>()
              .Property(u => u.TotalEarnings)
              .HasColumnType("DECIMAL(18,2)");

            builder.Entity<Payment>()
              .Property(u => u.ClientPaymentAmount)
              .HasColumnType("DECIMAL(18,2)");

            builder.Entity<Payment>()
              .Property(u => u.FreelancerEarning)
              .HasColumnType("DECIMAL(18,2)");   
            
            builder.Entity<Payment>()
              .Property(u => u.PlatformCommission)
              .HasColumnType("DECIMAL(18,2)");
                      
            builder.Entity<Order>()
              .Property(u => u.Budget)
              .HasColumnType("DECIMAL(18,2)"); 
            
            builder.Entity<Order>()
              .Property(u => u.TotalAmount)
              .HasColumnType("DECIMAL(18,2)");   
            
            builder.Entity<Order>()
              .Property(u => u.FreelancerAmount)
              .HasColumnType("DECIMAL(18,2)");



            // Seed roles
            var roleManager = builder.Entity<IdentityRole>();

            roleManager.HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Freelancer", NormalizedName = "FREELANCER" },
                new IdentityRole { Name = "Client", NormalizedName = "CLIENT" }
            );
        }
    }
}
