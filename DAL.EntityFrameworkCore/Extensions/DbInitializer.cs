using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Extensions
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Migrate()
        {
            _context?.Database.Migrate();
        }

        public void Seed()
        {
            _context.Database.EnsureCreated();

            if (!_context.Priorities.Any())
            {
                _context.Priorities.Add(new Priority { Name = "Urgent" });
                _context.Priorities.Add(new Priority { Name = "Normal" });
                _context.Priorities.Add(new Priority { Name = "Low" });
                _context.Priorities.Add(new Priority { Name = "Immediate" });
                _context.SaveChanges();
            }

            if (!_context.Statuses.Any())
            {
                _context.Statuses.Add(new Status { Name = "Open" });
                _context.Statuses.Add(new Status { Name = "Closed" });
                _context.Statuses.Add(new Status { Name = "In progress" });
                _context.SaveChanges();
            }

            if (!_context.UserTitles.Any())
            {
                _context.UserTitles.Add(new UserTitle { Title = "Noorliige" });
                _context.UserTitles.Add(new UserTitle { Title = "Vanemliige" });
                _context.UserTitles.Add(new UserTitle { Title = "Ülemus" });
                _context.SaveChanges();
            }

            SeedUsers().Wait();

            if (!_context.ProjectTasks.Any())
            {
                _context.ProjectTasks.Add(new ProjectTask
                {
                    Description = "Mingisugune suits ja imelik lõhn on üleval",
                    Title = "Midagi on katki",
                    Priority = _context.Priorities.First(),
                    Status = _context.Statuses.First(),
                    AmountDone = 50,
                    ClientEmail = "keegi@kusagil.ee",
                    ClientName = "Keegi kusagil",
                    ClientPhone = "52343534",
                    ComponentPrice = 25.25M,
                    PaidWork = true,
                    Price = 45.25M,
                    Assignee = _context.Users.First(),
                    TaskUsers = new List<TaskUser>
                    {
                       new TaskUser
                       {
                           UserId = _context.AppUsers.First().Id
                       }
                    },
                    CreatedAt = DateTime.Now.AddDays(-5),
                    ModifiedAt = DateTime.Now.AddDays(-5),
                    CreatedBy = _context.AppUsers.First(),
                    ModifiedBy = _context.AppUsers.First()
                });
                _context.ProjectTasks.Add(new ProjectTask
                {
                    Description = "Client came and said \"clean mah pc\"",
                    Title = "HP tolmupuhastus",
                    Priority = _context.Priorities.First(),
                    Status = _context.Statuses.First(),
                    AmountDone = 0,
                    ClientEmail = "jefi@jeri.ee",
                    ClientName = "Jefi Jeri",
                    ClientPhone = "99554246",
                    ComponentPrice = 0,
                    PaidWork = true,
                    Price = 15,
                    CreatedAt = DateTime.Now,
                    ModifiedAt = DateTime.Now,
                    CreatedBy = _context.AppUsers.First(),
                    ModifiedBy = _context.AppUsers.First()
                });
                _context.SaveChanges();
            }
        }

        private async Task SeedUsers()
        {
            if (!_context.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            if (!_context.Users.Any())
            {
                string user = "admin";
                string password = "Testing1";
                await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = user,
                    Email = "admin@test.ee",
                    EmailConfirmed = true,
                    Firstname = "Admin",
                    Lastname = "Test"
                }, password);
                await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "Admin");

                user = "user";
                password = "Testing1";
                await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = user,
                    Email = "user@test.ee",
                    EmailConfirmed = true,
                    Firstname = "User",
                    Lastname = "Test"
                }, password);
                await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(user), "User");
            }
        }
    }
}
