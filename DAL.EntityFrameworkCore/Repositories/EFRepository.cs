using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext RepositoryDbContext { get; set; }
        protected DbSet<TEntity> RepositoryDbSet { get; set; }

        public EFRepository(IDataContext dataContext)
        {
            RepositoryDbContext = dataContext as DbContext ?? throw new ArgumentNullException(nameof(dataContext));
            RepositoryDbSet = RepositoryDbContext.Set<TEntity>() ?? throw new NullReferenceException($"DbSet for {nameof(TEntity)} not found!");
        }

        public virtual IEnumerable<TEntity> All() => RepositoryDbSet.ToList();

        public virtual async Task<IEnumerable<TEntity>> AllAsync() => await RepositoryDbSet.ToListAsync();

        public virtual TEntity Find(params object[] id)
        {
            return RepositoryDbSet.Find(id);
        }

        public virtual Task<TEntity> FindAsync(params object[] id)
        {
            return RepositoryDbSet.FindAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            if (entity == null) throw new InvalidOperationException("Unable to add a null entity to the repository.");
            RepositoryDbSet.Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (entity == null) throw new InvalidOperationException("Unable to add a null entity to the repository.");
            await RepositoryDbSet.AddAsync(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepositoryDbSet.Update(entity).Entity;
        }

        public virtual void Remove(TEntity entity)
        {
            RepositoryDbSet.Attach(entity);
            RepositoryDbContext.Entry(entity).State = EntityState.Deleted;
            RepositoryDbSet.Remove(entity);
        }

        public bool Exists(int id)
        {
            return RepositoryDbSet.Find(id) != null;
        }
    }
}
