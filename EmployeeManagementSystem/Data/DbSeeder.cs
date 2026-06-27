using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementSystem.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Seed Roles
            string[] roles = { "Admin", "Manager", "Employee" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Seed Admin User
            var adminEmail = "admin@ems.com";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "System Admin",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            // Seed Manager User
            var managerEmail = "manager@ems.com";
            if (await userManager.FindByEmailAsync(managerEmail) == null)
            {
                var manager = new ApplicationUser
                {
                    UserName = managerEmail,
                    Email = managerEmail,
                    FullName = "HR Manager",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(manager, "Manager@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(manager, "Manager");
            }
        }
    }
}
