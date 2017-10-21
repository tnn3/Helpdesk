using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Interfaces;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class ProjectTaskRepository : EFRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(IDataContext dbContext) : base(dbContext)
        {

        }

        public Task<List<ProjectTask>> AllWithReferencesAsync()
        {
            return RepositoryDbSet
                .Include(p => p.Status)
                .Include(p => p.Priority)
                .Include(p => p.TaskUsers)
                    .ThenInclude(userTask => userTask.User)
                .Include(p => p.Assignee)
                .Include(p => p.ChangeSets)
                .Include(p => p.CustomFieldValues)
                .Include(p => p.CustomFields)
                .Include(p => p.TaskUsers)
                    .ThenInclude(u => u.User)
                .ToListAsync();
        }

        public Task<ProjectTask> FindWithReferencesAsync(int id)
        {
            return RepositoryDbSet
                .Include(p => p.Status)
                .Include(p => p.Priority)
                .Include(p => p.TaskUsers)
                    .ThenInclude(userTask => userTask.User)
                .Include(p => p.Assignee)
                .Include(p => p.ChangeSets)
                .Include(p => p.CustomFieldValues)
                .Include(p => p.CustomFields)
                .Include(p => p.TaskUsers)
                    .ThenInclude(u => u.User)
                .Include(p => p.ChangeSets)
                    .ThenInclude(p => p.ModifiedBy)
                .Include(p => p.ChangeSets)
                    .ThenInclude(cs => cs.Changes)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<ProjectTask> FindWithReferencesNoTrackingAsync(int id)
        {
            return RepositoryDbSet
                .Include(p => p.Status)
                .Include(p => p.Priority)
                .Include(p => p.TaskUsers)
                    .ThenInclude(userTask => userTask.User)
                .Include(p => p.Assignee)
                .Include(p => p.ChangeSets)
                .Include(p => p.CustomFieldValues)
                .Include(p => p.CustomFields)
                .Include(p => p.TaskUsers)
                    .ThenInclude(u => u.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<List<ProjectTask>> AllBefore(DateTime dateTime)
        {
            return RepositoryDbSet
                .Where(p => p.ModifiedAt > dateTime)
                .ToListAsync();
        }
    }
}
