using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Interfaces;
using Interfaces.Base;
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
    }
}
