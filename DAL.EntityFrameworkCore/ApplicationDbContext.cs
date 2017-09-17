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
        public DbSet<Domain.ApplicationUser> AppUsers { get; set; }
        public DbSet<Domain.Change> Changes { get; set; }
        public DbSet<Domain.ChangeSet> ChangeSets { get; set; }
        public DbSet<Domain.CustomField> CustomFields { get; set; }
        public DbSet<Domain.CustomFieldValue> CustomFieldValues { get; set; }
        public DbSet<Domain.Priority> Priorities { get; set; }
        public DbSet<Domain.Status> Statuses { get; set; }
        public DbSet<Domain.UserTitle> UserTitles { get; set; }
        public DbSet<Domain.CustomFieldInTasks> FieldInTasks { get; set; }
        public DbSet<Domain.TaskUsers> UsersInTasks { get; set; }

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
