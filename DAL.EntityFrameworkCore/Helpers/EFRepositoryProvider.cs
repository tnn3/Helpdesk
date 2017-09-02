using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.Base;

namespace DAL.EntityFrameworkCore.Helpers
{
    public class EFRepositoryProvider<TContext> : IRepositoryProvider where TContext : IDataContext
    {
        private readonly IDataContext _context;
        private readonly IRepositoryFactory _repositoryFactory;

        // cache for repositories
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public EFRepositoryProvider(TContext context, IRepositoryFactory repositoryFactory)
        {
            _context = context;
            if (_context == null)
            {
                throw new NullReferenceException(nameof(context));
            }

            _repositoryFactory = repositoryFactory;
        }

        // return standard repository
        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            return GetOrMakeRepository<IRepository<TEntity>>(_repositoryFactory.GetStandardRepositoryFactory<TEntity>());
        }

        // interface based custom repos
        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            return GetOrMakeRepository<TRepository>(_repositoryFactory.GetCustomRepositoryFactory<TRepository>());
        }


        private TRepository GetOrMakeRepository<TRepository>(Func<IDataContext, object> factory = null) where TRepository : class
        {
            // Look for T dictionary cache under typeof(T).
            _repositories.TryGetValue(typeof(TRepository), out var repoObj);
            if (repoObj != null)
            {
                return (TRepository)repoObj;
            }

            // repsoitory was not found in cache. try to create it.
            return MakeRepository<TRepository>(factory);
        }

        private TRepository MakeRepository<TRepository>(Func<IDataContext, object> factory) where TRepository : class
        {
            var repositoryFactory = factory ?? _repositoryFactory.GetCustomRepositoryFactory<TRepository>();

            if (repositoryFactory == null)
            {
                throw new NotImplementedException($"No factory for repository type {typeof(TRepository).FullName}");
            }
            var repo = (TRepository)repositoryFactory(_context);
            _repositories[typeof(TRepository)] = repo;
            return repo;
        }
    }
}
