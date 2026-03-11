using System.Threading.Tasks;
using EmployeeManagement.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Infrastructure.Identity
{
    public static class IdentitySeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "User" };

            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                {
                    await roleManager.CreateAsync(new IdentityRole(r));
                }
            }

            if (await userManager.FindByNameAsync("admin") == null)
            {
                var adminUser = new ApplicationUser { UserName = "admin", Email = "admin@local" };
                var res = await userManager.CreateAsync(adminUser, "Admin123!");
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}
