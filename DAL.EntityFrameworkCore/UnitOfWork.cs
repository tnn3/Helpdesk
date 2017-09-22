using Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore
{
    public class ApplicationUnitOfWork<TContext> : IUnitOfWork
        where TContext : IDataContext
    {
        public IProjectTaskRepository ProjectTasks => GetCustomRepository<IProjectTaskRepository>();
        public IChangeRepository Changes => GetCustomRepository<IChangeRepository>();
        public ICustomFieldRepository CustomFields => GetCustomRepository<ICustomFieldRepository>();
        public IApplicationUserRepository Users => GetCustomRepository<IApplicationUserRepository>();

        public IRepository<Status> Statuses => GetEntityRepository<Status>();
        public IRepository<Priority> Priorities => GetEntityRepository<Priority>();
        public IRepository<UserTitle> UserTitles => GetEntityRepository<UserTitle>();
        public IRepository<ChangeSet> ChangeSets => GetEntityRepository<ChangeSet>();
        public IRepository<CustomFieldValue> CustomFieldValues => GetEntityRepository<CustomFieldValue>();
        public IRepository<CustomFieldInTasks> CustomFieldInTasks => GetEntityRepository<CustomFieldInTasks>();
        public IRepository<TaskUsers> TaskUsers => GetEntityRepository<TaskUsers>();

        private DbContext _context;
        private readonly IRepositoryProvider _repositoryProvider;

        public ApplicationUnitOfWork(TContext context, IRepositoryProvider repositoryProvider)
        {
            _context = context as DbContext ?? throw new NullReferenceException(nameof(context));
            _repositoryProvider = repositoryProvider ?? throw new NullReferenceException(nameof(repositoryProvider));
        }

        public int SaveChanges()
        {
            CheckDisposed();
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            CheckDisposed();
            return _context.SaveChangesAsync();
        }

        // get standard repository by entity 
        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            CheckDisposed();
            return _repositoryProvider.GetEntityRepository<TEntity>();
        }

        // get custom repository by interface
        public TRepository GetCustomRepository<TRepository>() where TRepository : class
        {
            CheckDisposed();
            return _repositoryProvider.GetCustomRepository<TRepository>();
        }

        #region IDisposable Implementation

        private bool _isDisposed;

        protected void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException("The UnitOfWork is already disposed and cannot be used anymore.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_context != null)
                    {
                        _context.Dispose();
                        _context = null;
                    }
                }
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ApplicationUnitOfWork()
        {
            Dispose(false);
        }

        #endregion
    }
}
