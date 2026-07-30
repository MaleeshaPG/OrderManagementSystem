using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Models;
using static OrderManagementSystem.Data.Enum;

namespace OrderManagementSystem.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<OMSDbContext>();

            await context.Database.MigrateAsync();

            await SeedRolesAndAdminAsync(serviceProvider);
        }

        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var context = scope.ServiceProvider.GetRequiredService<OMSDbContext>();

            string[] roles = { "Admin", "Manager", "Employee", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string adminEmail = "maleeshgunasekera99@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var adminEmployee = await context.Employees.FirstOrDefaultAsync(e => e.Email == adminEmail);
                if (adminEmployee == null)
                {
                    adminEmployee = new Employee
                    {
                        FirstName = "System",
                        LastName = "Admin",
                        FullName = "System Admin",
                        TelNo = "0000000000",
                        Email = adminEmail,
                        Status = EmployeeStatus.Active,
                        IsDeleted = RecordDeleteStatus.Active,
                        CreatedBy = 0,
                        CreatedDate = DateTime.UtcNow
                    };
                    context.Employees.Add(adminEmployee);
                    await context.SaveChangesAsync();
                }

                var newAdmin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin",
                    EmployeeID = adminEmployee.EmployeeID,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, "Admin@123456!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
            }
        }
    }
}
