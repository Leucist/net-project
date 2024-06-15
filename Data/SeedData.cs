using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider) {
            using (var context = new UsersContext(
                serviceProvider.GetRequiredService<DbContextOptions<UsersContext>>()))
            {
                FillRoles(context);
            }
        }

        private static void FillRoles(UsersContext context) {
            if (context.Roles.Any()) { return; }

            context.Roles.AddRange(
                new Models.Role { Name = "student" },
                new Models.Role { Name = "admin" }
            );

            context.SaveChanges();
        }
    }
}