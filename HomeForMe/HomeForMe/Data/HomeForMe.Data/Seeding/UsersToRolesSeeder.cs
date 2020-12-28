using HomeForMe.Common;
using HomeForMe.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeForMe.Data.Seeding
{
    internal class UsersToRolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            await SeedUserToRoles(dbContext, userManager, roleManager);
        }

        private static async Task SeedUserToRoles(
            ApplicationDbContext dbContext,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            var user = await userManager.FindByNameAsync("admin");
            var role = await roleManager.FindByNameAsync(GlobalConstants.AdministrationRoleName);

            var exists = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exists)
            {
                return;
            }

            await dbContext.UserRoles.AddAsync(new AppUserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            });
        }
    }
}
