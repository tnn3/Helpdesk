using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repositories;
using Interfaces.Services;

namespace Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        protected IRepository<TEntity> EntityRepository { get;set; }

        //for unit tests
        public BaseService()
        {
            
        }

        public BaseService(IRepository<TEntity> entityRepository)
        {
            EntityRepository = entityRepository;
        }

        public virtual IEnumerable<TEntity> All()
        {
            return EntityRepository.All();
        }

        public virtual Task<IEnumerable<TEntity>> AllAsync()
        {
            return EntityRepository.AllAsync();
        }

        public virtual TEntity Find(params object[] id)
        {
            return EntityRepository.Find(id);
        }

        public virtual Task<TEntity> FindAsync(params object[] id)
        {
            return EntityRepository.FindAsync(id);
        }

        public virtual void Add(TEntity entity)
        {
            EntityRepository.Add(entity);
        }

        public virtual Task AddAsync(TEntity entity)
        {
            return EntityRepository.AddAsync(entity);
        }

        public virtual Task AddAsync(TEntity entity, ApplicationUser signedInUser)
        {
            return EntityRepository.AddAsync(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            return EntityRepository.Update(entity);
        }

        public virtual TEntity Update(TEntity entity, ApplicationUser signedInUser)
        {
            return EntityRepository.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            EntityRepository.Remove(entity);
        }

        public virtual bool Exists(int id)
        {
            return EntityRepository.Exists(id);
        }

        public virtual int SaveChanges()
        {
            return EntityRepository.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return EntityRepository.SaveChangesAsync();
        }
    }
}
