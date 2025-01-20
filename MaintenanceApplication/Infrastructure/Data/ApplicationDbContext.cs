
using Domain.Entity.UserEntities;
using Domain.Enums;
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

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserOtp> UserOtps { get; set; }
        public DbSet<OfferedService> OfferedServices { get; set; }
        public DbSet<OfferedServiceCategory> OfferedServiceCategories { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<Notification> Notifications { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<Content> Contents { get; set; }
        //public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Fluent Model

            // Cascade delete from Order to Client
            builder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(u => u.ClientOrders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.SetNull);  // If Client is deleted, set ClientId to null (Order remains, but Client is removed)

            // Restrict delete from Order to Freelancer
            builder.Entity<Order>()
                .HasOne(o => o.Freelancer)
                .WithMany(u => u.FreelancerOrders)
                .HasForeignKey(o => o.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent delete of Freelancer if there are any associated Orders

            // Relationship between Order and OfferedService
            builder.Entity<Order>()
                .HasOne(o => o.Service)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of OfferedService if there are any associated Orders

            //// Relationship between Feedback and ApplicationUser (Client)
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Client)
            //    .WithMany(u => u.FeedbackGivenByClient)
            //    .HasForeignKey(f => f.FeedbackByClientId)
            //    .OnDelete(DeleteBehavior.SetNull);  // Set ClientId to null if Client is deleted

            //// Relationship between Feedback and ApplicationUser (Freelancer)
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Freelancer)
            //    .WithMany(u => u.FeedbackGivenByFreelancer)
            //    .HasForeignKey(f => f.FeedbackByFreelancerId)
            //    .OnDelete(DeleteBehavior.SetNull);  // Set FreelancerId to null if Freelancer is deleted

            // Cascade delete from Bid to Freelancer
            builder.Entity<Bid>()
                .HasOne(b => b.Freelancer)
                .WithMany()
                .HasForeignKey(b => b.FreelancerId)
                .OnDelete(DeleteBehavior.Cascade); // If Freelancer is deleted, delete all associated Bids

            // Restrict cascade delete from Bid to OfferedService to avoid cycle
            builder.Entity<Bid>()
                .HasOne(b => b.OfferedService)
                .WithMany()
                .HasForeignKey(b => b.OfferedServiceId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of OfferedService if Bids exist

            // Relationship between Order and Dispute
            builder.Entity<Dispute>()
                .HasOne(d => d.Order)         // A Dispute belongs to an Order
                .WithMany(o => o.Disputes)    // An Order can have many Disputes
                .HasForeignKey(d => d.OrderId) // Foreign Key: Dispute's OrderId
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete from Order to Dispute

            // Relationship between Dispute and ApplicationUser (ResolvedBy)
            builder.Entity<Dispute>()
                .HasOne(d => d.ResolvedByUser)         // A Dispute has one ResolvedBy (Admin)
                .WithMany()                            // Admin can resolve many Disputes
                .HasForeignKey(d => d.ResolvedBy)      // Foreign Key: Dispute's ResolvedBy (AdminId)
                .OnDelete(DeleteBehavior.SetNull);     // If an admin is deleted, set ResolvedBy to null

            // Relationship between Order and Feedback
            builder.Entity<Feedback>()
                .HasOne(f => f.Order)             // A Feedback is associated with one Order
                .WithMany(o => o.Feedbacks)       // An Order can have many Feedbacks
                .HasForeignKey(f => f.OrderId)    // Foreign Key: Feedback's OrderId
                .OnDelete(DeleteBehavior.SetNull); // If an Order is deleted, set OrderId to null in Feedback

            // Relationship between Feedback and ApplicationUser (Client)
            builder.Entity<Feedback>()
                .HasOne(f => f.Client)                  // A Feedback is given by one Client
                .WithMany(u => u.FeedbackGivenByClient) // A Client can give many Feedbacks
                .HasForeignKey(f => f.FeedbackByClientId)         // Foreign Key: ClientId
                .OnDelete(DeleteBehavior.NoAction);      // If Client is deleted, set ClientId to null

            // Relationship between Feedback and ApplicationUser (Freelancer)
            builder.Entity<Feedback>()
                .HasOne(f => f.Freelancer)              // A Feedback is given by one Freelancer
                .WithMany(u => u.FeedbackGivenByFreelancer) // A Freelancer can give many Feedbacks
                .HasForeignKey(f => f.FeedbackOnFreelancerId)     // Foreign Key: FreelancerId
                .OnDelete(DeleteBehavior.NoAction);      // If Freelancer is deleted, set FreelancerId to null

            // Relationship between Order and ApplicationUser (Client)
            builder.Entity<Order>()
                .HasOne(o => o.Client)                  // An Order has one Client
                .WithMany(u => u.ClientOrders)          // A Client can have many Orders
                .HasForeignKey(o => o.ClientId)         // Foreign Key: ClientId
                .OnDelete(DeleteBehavior.SetNull);      // If Client is deleted, set ClientId to null

            #endregion



            //#region Fluent Model


            //// Cascade delete from Order to Client
            //builder.Entity<Order>()
            //    .HasOne(o => o.Client)
            //    .WithMany(u => u.ClientOrders)
            //    .HasForeignKey(o => o.ClientId)
            //    .OnDelete(DeleteBehavior.Cascade);  // If Client is deleted, delete all associated Orders

            //// Restrict delete from Order to Freelancer
            //builder.Entity<Order>()
            //    .HasOne(o => o.Freelancer)
            //    .WithMany(u => u.FreelancerOrders)
            //    .HasForeignKey(o => o.FreelancerId)
            //    .OnDelete(DeleteBehavior.Restrict);  // Prevent delete of Freelancer if there are any associated Orders


            //// Relationship between Order and OfferedService
            //builder.Entity<Order>()
            //    .HasOne(o => o.Service)
            //    .WithMany(o => o.Orders)
            //    .HasForeignKey(o => o.ServiceId)
            //    .OnDelete(DeleteBehavior.Restrict);  // Instead of Cascade, set ServiceId to null when the OfferedService is deleted


            //// Relationship between Feedback and ApplicationUser (Client)
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Client)
            //    .WithMany(u => u.FeedbackGivenByClient)
            //    .HasForeignKey(f => f.FeedbackByClientId)
            //    .OnDelete(DeleteBehavior.SetNull);  // Set ClientId to null if Client is deleted

            //// Relationship between Feedback and ApplicationUser (Freelancer)
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Freelancer)
            //    .WithMany(u => u.FeedbackGivenByFreelancer)
            //    .HasForeignKey(f => f.FeedbackByFreelancerId)
            //    .OnDelete(DeleteBehavior.SetNull);  // Prevent deletion if Freelancer is associated with feedback



            //// Cascade delete from Bid to Freelancer
            //builder.Entity<Bid>()
            //    .HasOne(b => b.Freelancer)
            //    .WithMany()
            //    .HasForeignKey(b => b.FreelancerId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// Restrict cascade delete from Bid to OfferedService to avoid cycle
            //builder.Entity<Bid>()
            //    .HasOne(b => b.OfferedService)
            //    .WithMany()
            //    .HasForeignKey(b => b.OfferedServiceId)
            //    .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict


            //// Relationship between Order and Dispute
            //builder.Entity<Dispute>()
            //    .HasOne(d => d.Order)         // A Dispute belongs to an Order
            //    .WithMany(o => o.Disputes)    // An Order can have many Disputes
            //    .HasForeignKey(d => d.OrderId) // Foreign Key: Dispute's OrderId
            //    .OnDelete(DeleteBehavior.Restrict); // Prevent cascading delete from Order to Dispute (disputes should remain even if the order is deleted)


            //// Relationship between Dispute and ApplicationUser (ResolvedBy)
            //builder.Entity<Dispute>()
            //    .HasOne(d => d.ResolvedByUser)         // A Dispute has one ResolvedBy (Admin)
            //    .WithMany()                            // Admin can resolve many Disputes (no need for a navigation property in ApplicationUser)
            //    .HasForeignKey(d => d.ResolvedBy)      // Foreign Key: Dispute's ResolvedBy (AdminId)
            //    .OnDelete(DeleteBehavior.SetNull);     // If an admin is deleted, set ResolvedBy to null (dispute can still exist without the admin)



            //// Relationship between Order and Feedback
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Order)             // A Feedback is associated with one Order
            //    .WithMany(o => o.Feedbacks)       // An Order can have many Feedbacks
            //    .HasForeignKey(f => f.OrderId)    // Foreign Key: Feedback's OrderId
            //    .OnDelete(DeleteBehavior.Cascade); // If an Order is deleted, delete associated Feedbacks (this can be adjusted based on business needs)



            //// Relationship between Feedback and ApplicationUser (Client)
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Client)                  // A Feedback is given by one Client
            //    .WithMany(u => u.FeedbackGivenByClient) // A Client can give many Feedbacks
            //    .HasForeignKey(f => f.FeedbackByClientId)         // Foreign Key: ClientId
            //    .OnDelete(DeleteBehavior.NoAction);      // If Client is deleted, set ClientId to null (feedback can still exist)

            //// Relationship between Feedback and ApplicationUser (Freelancer)
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Freelancer)              // A Feedback is given by one Freelancer
            //    .WithMany(u => u.FeedbackGivenByFreelancer) // A Freelancer can give many Feedbacks
            //    .HasForeignKey(f => f.FeedbackByFreelancerId)     // Foreign Key: FreelancerId
            //    .OnDelete(DeleteBehavior.NoAction);      // If Freelancer is deleted, set FreelancerId to null (feedback can still exist)

            //// Relationship between Order and ApplicationUser (Client)
            //builder.Entity<Order>()
            //    .HasOne(o => o.Client)                  // An Order has one Client
            //    .WithMany(u => u.ClientOrders)          // A Client can have many Orders
            //    .HasForeignKey(o => o.ClientId)         // Foreign Key: ClientId
            //    .OnDelete(DeleteBehavior.SetNull);      // If Client is deleted, delete all associated Orders


            //#endregion

            #region Set Precision 
            //------------------------ Set precision for HourlyRate (DECIMAL(18, 2) in SQL Server)
            builder.Entity<ApplicationUser>()
                .Property(u => u.HourlyRate)
                .HasColumnType("DECIMAL(18,2)");

            builder.Entity<ApplicationUser>()
            .Property(u => u.TotalEarnings)
            .HasColumnType("DECIMAL(18,2)");

            builder.Entity<Bid>()
              .Property(u => u.CustomPrice)
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

            #endregion

            #region Data Seeding

            // Seeding roles

            var AdminRoleId = Guid.NewGuid().ToString();
            var FreelancerRoleId = Guid.NewGuid().ToString();
            var ClientRoleId = Guid.NewGuid().ToString();


            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = AdminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = ClientRoleId,
                    Name = "Client",
                    NormalizedName = "CLIENT"
                },
                new IdentityRole
                {
                    Id = FreelancerRoleId,
                    Name = "Freelancer",
                    NormalizedName = "FREELANCER"
                }
            );



            // Seeding For Admin
            var Admin = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                FirstName = "System",
                LastName = "Administrator",
                Role = Role.Admin.ToString(),
                Status = UserStatus.Approved,
                Location = "Head Office",
                Address = "123 Admin Street",
                SecurityStamp = Guid.NewGuid().ToString(),

                // Freelancer Fields (set to null for Admin)
                ExpertiseArea = null,
                Rating = 0,
                Bio = null,
                Experience = null,
                ApprovedDate = null, // Admin doesn't have this field
                RegistrationDate = null, // Admin doesn't have this field

                Skills = null,
                HourlyRate = null,
                IsApprove = null, // Admin doesn't have this field
                IsSuspended = false,

                // Freelancer Related Fields (set to null for Admin)
                MonthlyLimit = null,
                OrdersCompleted = null,
                TotalEarnings = null,
                ReportMonth = null, // Admin doesn't have this field

                CurrentRating = 0, // Default value
            };

            var AdminPassword = "Admin@123";
            var PasswordHasher = new PasswordHasher<ApplicationUser>();
           Admin.PasswordHash = PasswordHasher.HashPassword(Admin,AdminPassword);

            builder.Entity<ApplicationUser>().HasData( Admin );

            // Assign Admin role to the Admin user
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = AdminRoleId,
                UserId = Admin.Id.ToString()
            });
            builder.Entity<IdentityUserRole<string>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            #endregion
        }
    }
}
