using System;
using System.Collections.Generic;
using System.Text;
using DAL.EntityFrameworkCore.Repositories;
using Interfaces;
using Interfaces.Base;

namespace DAL.EntityFrameworkCore.Helpers
{
    public class EFRepositoryFactory : IRepositoryFactory
    {
        private readonly IDictionary<Type, Func<IDataContext, object>> _repositoryFactories;

        public EFRepositoryFactory()
        {
            _repositoryFactories = GetCustomFactories();
        }

        //constructor only for testing
        public EFRepositoryFactory(IDictionary<Type, Func<IDataContext, object>> factories)
        {
            _repositoryFactories = factories;
        }


        //repositories with custom interfaces are registered here
        private static IDictionary<Type, Func<IDataContext, object>> GetCustomFactories()
        {
            return new Dictionary<Type, Func<IDataContext, object>>
            {
                {typeof(IProjectTaskRepository), dbContext => new ProjectTaskRepository(dbContext)},

            };
        }

        public Func<IDataContext, object> GetRepositoryFactoryForType<T>() where T : class
        {
            return GetCustomRepositoryFactory<T>() ?? GetStandardRepositoryFactory<T>();
        }

        public Func<IDataContext, object> GetCustomRepositoryFactory<T>() where T : class
        {
            _repositoryFactories.TryGetValue(typeof(T), out var factory);

            return factory;
        }

        // return factory for creation of standard repositories
        public Func<IDataContext, object> GetStandardRepositoryFactory<TEntity>() where TEntity : class
        {
            // create new instance of EFRepository<TEntity>
            return dataContext => new EFRepository<TEntity>(dataContext);
        }
    }
}
