using Domain.Entity.UserEntities;
using Maintenance.Infrastructure.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Maintenance.DefaultDataSeeder
{
    public class DataSeeder
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                await context.Database.MigrateAsync(); // Ensure DB is up to date

                // Fixed Role IDs
                var roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "f5c7b1c3-63e0-4c54-a0c2-558d63e6853d", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "c5a8c1a3-91e7-4e43-a9c1-784c72e0d57e", Name = "Freelancer", NormalizedName = "FREELANCER" },
                new IdentityRole { Id = "d9f02f42-a4b1-4c88-9266-0b53c6b4f10e", Name = "Client", NormalizedName = "CLIENT" }
            };

                // Check if roles exist before inserting
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                // Fixed Admin ID
                var adminId = "21bc9b2f-6401-40c1-9440-72e293f41a12";
                var adminUser = await userManager.FindByIdAsync(adminId);

                if (adminUser == null)
                {
                    var admin = new ApplicationUser
                    {
                        Id = adminId,
                        UserName = "admin",
                        NormalizedUserName = "ADMIN",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        EmailConfirmed = true,
                        FullName = "System Administrator"
                    };

                    var password = "Admin@123";
                    await userManager.CreateAsync(admin, password);
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
