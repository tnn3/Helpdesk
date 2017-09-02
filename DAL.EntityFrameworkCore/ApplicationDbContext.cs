using DAL.EntityFrameworkCore.Extensions;
using Domain;
using Microsoft.EntityFrameworkCore;
using Interfaces.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DbSet<Domain.ProjectTask> ProjectTasks { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.DisableCascadingDeletes();
        }
    }
}
