using System;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;
        TRepository GetCustomRepository<TRepository>() where TRepository : class;

        IProjectTaskRepository ProjectTasks { get; }
        IChangeRepository Changes { get; }
        ICustomFieldRepository CustomFields { get; }
        IRepository<Status> Statuses { get; }
        IRepository<ChangeSet> ChangeSets { get; }
        IRepository<CustomFieldValue> CustomFieldValues { get; }
        IRepository<Priority> Priorities { get; }
        IRepository<UserTitle> UserTitles { get; }
        IRepository<CustomFieldInTasks> CustomFieldInTasks { get; }
        IRepository<TaskUsers> TaskUsers { get; }
        IApplicationUserRepository Users { get; }
    }
}
