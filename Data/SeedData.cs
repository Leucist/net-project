using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Educational_platform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Data
{
    public class SeedData
    {
        private static string adminRole = "admin";
        private static string adminUserName = "root";
        private static string adminPass = "!QAZ2wsx";
        public static async Task Initialize(IServiceProvider serviceProvider) {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            await FillRoles(roleManager);
            await SetAdmins(serviceProvider);
        }

        private static async Task FillRoles(RoleManager<Models.Role> roleManager) {
            if (!await roleManager.RoleExistsAsync("student"))
            {
                await roleManager.CreateAsync(new Models.Role { Name = "student"});
            }

            if (!await roleManager.RoleExistsAsync(adminRole))
            {
                await roleManager.CreateAsync(new Models.Role { Name = adminRole});
            }
            
        }

        private static async Task SetAdmins(IServiceProvider serviceProvider) {
            var userManager = serviceProvider.GetRequiredService<UserManager<Users>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            var role = await roleManager.FindByNameAsync(adminRole);
            var usersInRole = await userManager.GetUsersInRoleAsync(role.Name);

            if (!usersInRole.Any()) {
                var context = new UsersContext(serviceProvider.GetRequiredService<DbContextOptions<UsersContext>>());

                Users newAdmin = new Users { 
                    Name = adminRole,
                    Surname = adminRole,
                    Email = adminRole + "@mail",
                    UserName = adminUserName,
                };

                var result = await userManager.CreateAsync(newAdmin, adminPass);

                if (result.Succeeded) {
                    await context.SaveChangesAsync();
                    await userManager.AddToRoleAsync(newAdmin, adminRole);
                }
            }
        }
    }
}