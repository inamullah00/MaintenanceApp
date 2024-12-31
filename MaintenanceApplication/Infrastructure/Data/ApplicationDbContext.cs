
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
        //public DbSet<Dispute> Disputes { get; set; }
        //public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Ensure to call the base method



            //// Cascade delete from Order to Client (no change)
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



            //// Cascade delete from Notification to ApplicationUser
            //builder.Entity<Notification>()
            //    .HasOne(n => n.User)  // Notification has one User
            //    .WithMany(u => u.Notifications)  // User has many Notifications
            //    .HasForeignKey(n => n.UserId)  // ForeignKey: UserId in Notification
            //    .OnDelete(DeleteBehavior.Cascade); // Cascade delete when User is deleted

            //// Cascade delete from Dispute to Order
            //builder.Entity<Dispute>()
            //    .HasOne(d => d.Order)  // Dispute has one Order
            //    .WithMany(o => o.Disputes)  // Order has many Disputes
            //    .HasForeignKey(d => d.OrderId)  // ForeignKey: OrderId in Dispute
            //    .OnDelete(DeleteBehavior.Cascade); // Cascade delete when Order is deleted

            //// Cascade delete from Dispute to ApplicationUser (RaisedByUser)
            //builder.Entity<Dispute>()
            //    .HasOne(d => d.RaisedByUser)  // Dispute has one User who raised it
            //    .WithMany(u => u.Disputes)  // User has many Disputes
            //    .HasForeignKey(d => d.RaisedByUserId)  // ForeignKey: RaisedByUserId in Dispute
            //    .OnDelete(DeleteBehavior.Cascade); // Cascade delete when User is deleted



            //// Cascade delete for PerformanceReport when Freelancer is deleted
            //builder.Entity<PerformanceReport>()
            //    .HasOne(pr => pr.Freelancer)  // PerformanceReport has one Freelancer
            //    .WithMany(f => f.PerformanceReports)  // Freelancer has many PerformanceReports
            //    .HasForeignKey(pr => pr.FreelancerId)  // ForeignKey: FreelancerId in PerformanceReport
            //    .OnDelete(DeleteBehavior.Cascade); // Cascade delete when Freelancer is deleted



            //// Cascade delete for OfferedService when Client is deleted
            //builder.Entity<OfferedService>()
            //    .HasOne(os => os.Client)  // OfferedService has one Client
            //    .WithMany(u => u.OfferedServices)  // Client has many OfferedServices
            //    .HasForeignKey(os => os.ClientId)  // ForeignKey: ClientId in OfferedService
            //    .OnDelete(DeleteBehavior.Cascade); // Cascade delete when Client is deleted

            //// Restrict delete for OfferedServiceCategory, don't allow deleting a category if services are linked
            //builder.Entity<OfferedService>()
            //    .HasOne(os => os.Category)  // OfferedService has one Category
            //    .WithMany(c => c.OfferedServices)  // Category has many OfferedServices
            //    .HasForeignKey(os => os.CategoryID)  // ForeignKey: CategoryID in OfferedService
            //    .OnDelete(DeleteBehavior.Restrict); // Restrict delete when Category has services linked





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
