using System.Collections.Generic;
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
                .Include(p => p.UserInTask)
                    .ThenInclude(userTask => userTask.User)
                .Include(p => p.ChangeSets)
                .Include(p => p.CustomFieldValues)
                .Include(p => p.CustomFields)
                .ToListAsync();
        }

        public Task<ProjectTask> FindWithReferencesAsync(int id)
        {
            return RepositoryDbSet
                .Include(p => p.Status)
                .Include(p => p.Priority)
                .Include(p => p.UserInTask)
                    .ThenInclude(userTask => userTask.User)
                .Include(p => p.ChangeSets)
                .Include(p => p.CustomFieldValues)
                .Include(p => p.CustomFields)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        public Task<ProjectTask> FindWithReferencesNoTrackingAsync(int id)
        {
            return RepositoryDbSet
                .Include(p => p.Status)
                .Include(p => p.Priority)
                .Include(p => p.UserInTask)
                .ThenInclude(userTask => userTask.User)
                .Include(p => p.ChangeSets)
                .Include(p => p.CustomFieldValues)
                .Include(p => p.CustomFields)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
