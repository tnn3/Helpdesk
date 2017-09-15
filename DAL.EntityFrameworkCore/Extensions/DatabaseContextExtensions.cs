using System;
using System.Linq;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Extensions
{
    public static class DatabaseContextExtensions
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.Priorities.Any())
            {
                context.Priorities.Add(new Priority { Name = "Urgent" });
                context.Priorities.Add(new Priority { Name = "Normal" });
                context.Priorities.Add(new Priority { Name = "Low" });
                context.Priorities.Add(new Priority { Name = "Immediate" });
                context.SaveChanges();
            }

            if (!context.Statuses.Any())
            {
                context.Statuses.Add(new Status { Name = "Open" });
                context.Statuses.Add(new Status { Name = "Closed" });
                context.Statuses.Add(new Status { Name = "In progress" });
                context.SaveChanges();
            }

            if (!context.UserTitles.Any())
            {
                context.UserTitles.Add(new UserTitle { Title = "Noorliige" });
                context.UserTitles.Add(new UserTitle { Title = "Vanemliige" });
                context.UserTitles.Add(new UserTitle { Title = "Ülemus" });
                context.SaveChanges();
            }

            if (!context.ProjectTasks.Any())
            {
                context.ProjectTasks.Add(new ProjectTask
                {
                    Description = "Mingisugune suits ja imelik lõhn on üleval",
                    Title = "Midagi on katki",
                    Priority = context.Priorities.First(),
                    Status = context.Statuses.First()
                });
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                string[] roles = { "Admin", "User" };
                var roleStore = new RoleStore<IdentityRole>(context);
                foreach (var role in roles)
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }

            if (!context.Users.Any())
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var password = new PasswordHasher<ApplicationUser>();

                var user = new ApplicationUser
                {
                    Title = context.UserTitles.First(),
                    Email = "admin@test.ee",
                    NormalizedEmail = "ADMIN@TEST.EE",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D")
                };
                user.PasswordHash = password.HashPassword(user, "test");
                userStore.CreateAsync(user);
                userStore.AddToRoleAsync(user, "Admin");
            }
            
        }
    }
}
