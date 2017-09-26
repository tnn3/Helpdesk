using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();
        Task<IEnumerable<TEntity>> AllAsync();
        TEntity Find(params object[] id);
        Task<TEntity> FindAsync(params object[] id);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        Task AddAsync(TEntity entity, ApplicationUser signedInUser);
        TEntity Update(TEntity entity);
        TEntity Update(TEntity entity, ApplicationUser signedInUser);
        void Remove(TEntity entity);
        bool Exists(int id);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
