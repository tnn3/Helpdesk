using System;
using System.Threading.Tasks;

namespace Interfaces.Base
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;
        TRepository GetCustomRepository<TRepository>() where TRepository : class;

        IProjectTaskRepository ProjectTasks { get; }
    }
}
