
using Domain.Entity.UserEntities;
using Maintenance.Domain.Entity.ClientEntities;
using Maintenance.Domain.Entity.Dashboard;
using Maintenance.Domain.Entity.FreelancerEntites;
using Maintenance.Domain.Entity.FreelancerEntities;
using Maintenance.Domain.Entity.UserEntities;
using Maintenance.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Maintenance.Infrastructure.Persistance.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<FreelancerService> FreelancerServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<FreelancerOtp> FreelancerOtps { get; set; }
        public DbSet<ClientOtp> ClientOtps { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<OfferedService> OfferedServices { get; set; }
        public DbSet<OfferedServiceCategory> OfferedServiceCategories { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<BidPackage> BidPackages { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Order> Orders { get; set; }

        //public DbSet<Notification> Notifications { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Country> Countries { get; set; }
        //public DbSet<Payment> Payments { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            builder.ApplyConfiguration(new ClientConfiguration());
            builder.ApplyConfiguration(new FreelancerConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new BidConfiguration());
            builder.ApplyConfiguration(new OffereServiceConfiguration());
            builder.ApplyConfiguration(new OfferedServiceCategoryConfiguration());
            builder.ApplyConfiguration(new FeedbackConfiguration());
            builder.ApplyConfiguration(new DisputeConfiguration());
            builder.ApplyConfiguration(new ContentConfiguration());
            builder.ApplyConfiguration(new ServiceConfiguration());
            builder.ApplyConfiguration(new FreelancerServiceConfiguration());
            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new ClientOtpConfiguration());
            builder.ApplyConfiguration(new FreelancerOtpConfiguration());
            builder.ApplyConfiguration(new BidPackageConfiguration());
            builder.ApplyConfiguration(new PackageConfiguration());

            #region Fluent Model

            // Cascade delete from Order to Client
            //builder.Entity<Order>()
            //    .HasOne(o => o.Client)
            //    .WithMany(u => u.ClientOrders)
            //    .HasForeignKey(o => o.ClientId)
            //    .OnDelete(DeleteBehavior.SetNull);  // If Client is deleted, set ClientId to null (Order remains, but Client is removed)

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
            //    .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of OfferedService if there are any associated Orders

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
            //builder.Entity<Bid>()
            //    .HasOne(b => b.Freelancer)
            //    .WithMany()
            //    .HasForeignKey(b => b.FreelancerId)
            //    .OnDelete(DeleteBehavior.Cascade); // If Freelancer is deleted, delete all associated Bids

            //// Restrict cascade delete from Bid to OfferedService to avoid cycle
            //builder.Entity<Bid>()
            //    .HasOne(b => b.OfferedService)
            //    .WithMany()
            //    .HasForeignKey(b => b.OfferedServiceId)
            //    .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of OfferedService if Bids exist

            //// Relationship between Order and Dispute
            //builder.Entity<Dispute>()
            //    .HasOne(d => d.Order)         // A Dispute belongs to an Order
            //    .WithMany(o => o.Disputes)    // An Order can have many Disputes
            //    .HasForeignKey(d => d.OrderId) // Foreign Key: Dispute's OrderId
            //    .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete from Order to Dispute

            // Relationship between Dispute and ApplicationUser (ResolvedBy)
            //builder.Entity<Dispute>()
            //    .HasOne(d => d.ResolvedByUser)         // A Dispute has one ResolvedBy (Admin)
            //    .WithMany()                            // Admin can resolve many Disputes
            //    .HasForeignKey(d => d.ResolvedBy)      // Foreign Key: Dispute's ResolvedBy (AdminId)
            //    .OnDelete(DeleteBehavior.SetNull);     // If an admin is deleted, set ResolvedBy to null

            // Relationship between Order and Feedback
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Order)             // A Feedback is associated with one Order
            //    .WithMany(o => o.Feedbacks)       // An Order can have many Feedbacks
            //    .HasForeignKey(f => f.OrderId)    // Foreign Key: Feedback's OrderId
            //    .OnDelete(DeleteBehavior.SetNull); // If an Order is deleted, set OrderId to null in Feedback

            //// Relationship between Feedback and ApplicationUser (Client)
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Client)                  // A Feedback is given by one Client
            //    .WithMany(u => u.FeedbackGivenByClient) // A Client can give many Feedbacks
            //    .HasForeignKey(f => f.FeedbackByClientId)         // Foreign Key: ClientId
            //    .OnDelete(DeleteBehavior.NoAction);      // If Client is deleted, set ClientId to null

            //// Relationship between Feedback and ApplicationUser (Freelancer)
            //builder.Entity<Feedback>()
            //    .HasOne(f => f.Freelancer)              // A Feedback is given by one Freelancer
            //    .WithMany(u => u.FeedbackGivenByFreelancer) // A Freelancer can give many Feedbacks
            //    .HasForeignKey(f => f.FeedbackOnFreelancerId)     // Foreign Key: FreelancerId
            //    .OnDelete(DeleteBehavior.NoAction);      // If Freelancer is deleted, set FreelancerId to null

            //// Relationship between Order and ApplicationUser (Client)
            //builder.Entity<Order>()
            //    .HasOne(o => o.Client)                  // An Order has one Client
            //    .WithMany(u => u.ClientOrders)          // A Client can have many Orders
            //    .HasForeignKey(o => o.ClientId)         // Foreign Key: ClientId
            //    .OnDelete(DeleteBehavior.SetNull);      // If Client is deleted, set ClientId to null

            #endregion


            #region Data Seeding

            //// Seeding roles

            //var AdminRoleId = Guid.NewGuid().ToString();
            //var FreelancerRoleId = Guid.NewGuid().ToString();
            //var ClientRoleId = Guid.NewGuid().ToString();


            //builder.Entity<IdentityRole>().HasData(
            //    new IdentityRole
            //    {
            //        Id = AdminRoleId,
            //        Name = "Admin",
            //        NormalizedName = "ADMIN"
            //    },
            //    new IdentityRole
            //    {
            //        Id = ClientRoleId,
            //        Name = "Client",
            //        NormalizedName = "CLIENT"
            //    },
            //    new IdentityRole
            //    {
            //        Id = FreelancerRoleId,
            //        Name = "Freelancer",
            //        NormalizedName = "FREELANCER"
            //    }
            //);


            //var adminId = "21bc9b2f-6401-40c1-9440-72e293f41a12"; // FIXED GUID


            //// Seeding For Admin
            //var Admin = new ApplicationUser
            //{
            //    Id = adminId,
            //    UserName = "admin",
            //    NormalizedUserName = "ADMIN",
            //    Email = "admin@gmail.com",
            //    NormalizedEmail = "ADMIN@GMAIL.COM",
            //    EmailConfirmed = true,
            //    FullName = "System Administrator",
            //    SecurityStamp = Guid.NewGuid().ToString(),
            //};

            //var AdminPassword = "Admin@123";
            //var PasswordHasher = new PasswordHasher<ApplicationUser>();
            //Admin.PasswordHash = PasswordHasher.HashPassword(Admin, AdminPassword);

            //builder.Entity<ApplicationUser>().HasData(Admin);

            //// Assign Admin role to the Admin user
            //builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            //{
            //    RoleId = AdminRoleId,
            //    UserId = adminId
            //});
            //builder.Entity<IdentityUserRole<string>>().HasKey(iur => new { iur.UserId, iur.RoleId });
            #endregion
        }
    }
}
