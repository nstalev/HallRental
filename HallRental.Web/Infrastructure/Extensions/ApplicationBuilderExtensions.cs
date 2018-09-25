﻿
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
        private const string adminFirstName = "admin";
        private const string adminLastName = "admin";

        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<HallRentalDbContext>().Database.Migrate();

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var db = serviceScope.ServiceProvider.GetService<HallRentalDbContext>();

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
                             FirstName = adminFirstName,
                             LastName = adminLastName
                         };

                         var result = await userManager.CreateAsync(adminUser, adminPassword);

                         // Add User to Role
                         if (result.Succeeded)
                         {
                             await userManager.AddToRoleAsync(adminUser, GlobalConstants.AdminRole);
                         }
                     }


                     //Seed Hall
                     if (!await db.Halls.AnyAsync())
                     {
                         Hall grandFoyerAndBallRoom = new Hall
                         {
                             Name = "Grand Foyer and BallRoom",
                             MondayFriday8amTo3pm = 750,
                             MondayThursday4pmToMN = 1000,
                             Friday4pmToMN = 1250,
                             Saturday8amTo3pm = 1250,
                             Saturday4pmToMN = 1750,
                             Sunday8amTo3pm = 750,
                             Sunday4pmToMN = 1750,
                             TablesAndChairsCostPerPerson =5,
                             HallCapacity = 375,
                             SecurityGuardCostPerHour = 20,
                             IsHallActive = true

                         };

                         Hall grandFoyerOnly = new Hall
                         {
                             Name = "Grand Foyer Only",
                             MondayFriday8amTo3pm = 250,
                             MondayThursday4pmToMN = 375,
                             Friday4pmToMN = 500,
                             Saturday8amTo3pm = 500,
                             Saturday4pmToMN = 750,
                             Sunday8amTo3pm = 250,
                             Sunday4pmToMN = 750,
                             TablesAndChairsCostPerPerson = 5,
                             HallCapacity = 100,
                             SecurityGuardCostPerHour = 20,
                             IsHallActive = true
                         };

                         Hall conferenceMediaRoom = new Hall
                         {
                             Name = "Conference/Media Room",
                             MondayFriday8amTo3pm = 250,
                             MondayThursday4pmToMN = 375,
                             Friday4pmToMN = 500,
                             Saturday8amTo3pm = 500,
                             Saturday4pmToMN = 750,
                             Sunday8amTo3pm = 250,
                             Sunday4pmToMN = 750,
                             TablesAndChairsCostPerPerson = 5,
                             HallCapacity = 60,
                             SecurityGuardCostPerHour = 20,
                             IsHallActive = true
                         };

                         await db.Halls.AddAsync(grandFoyerAndBallRoom);
                         await db.Halls.AddAsync(grandFoyerOnly);
                         await db.Halls.AddAsync(conferenceMediaRoom);
                         db.SaveChanges();
                     }


                  
                    
                 })
                 .GetAwaiter()
                 .GetResult();
            }
            return app;

        }
    }
}
