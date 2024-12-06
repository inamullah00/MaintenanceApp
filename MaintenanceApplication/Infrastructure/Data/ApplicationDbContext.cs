using Domain.Entity.UserEntities;
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Ensure to call the base method

            // Set precision for HourlyRate (DECIMAL(18, 2) in SQL Server)
            builder.Entity<ApplicationUser>()
                .Property(u => u.HourlyRate)
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
