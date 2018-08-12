
namespace HallRental.Web.Infrastructure.Extensions
{
    using HallRental.Data;
    using HallRental.Data.Models;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;

    public static class ApplicationBuilderExtensions
    {
        private const string adminEmail = "admin@abv.bg";
        private const string adminUsername = "admin";
        private const string adminPassword = "admin123";

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<HallRentalDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                //To run Async methods in Non-Asyn method
                Task
                 .Run(async () =>
                 {
                     // Seed Roles
                     var roles = new[]
                                     {
                         GlobalConstants.AdminRole,
                     };

                     foreach (var role in roles)
                     {
                         var roleExists = await roleManager.RoleExistsAsync(role);

                         if (!roleExists)
                         {
                             await roleManager.CreateAsync(new IdentityRole
                             {
                                 Name = role
                             });
                         }
                     }

                     // Seed Admin User
                     var adminUser = await userManager.FindByEmailAsync(adminEmail);

                     if (adminUser == null)
                     {
                         // Create Admin User
                         adminUser = new User
                         {
                             UserName = adminUsername,
                             Email = adminEmail,
                         };

                         var result = await userManager.CreateAsync(adminUser, adminPassword);

                         // Add User to Role
                         if (result.Succeeded)
                         {
                             await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdminRole);
                         }
                     }
                    
                 })
                 .GetAwaiter()
                 .GetResult();
            }
            return app;

        }
    }
}
