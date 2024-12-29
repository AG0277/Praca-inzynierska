using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace api
{
    public static class ApplicationDbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<WorkerAccount> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new[] { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                var user = new WorkerAccount
                {
                    UserName = "adminuser",
                    Email = "admin@example.com"
                };
                var result = await userManager.CreateAsync(user, "asdQWE123!@#");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
