using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Services
{
    public interface IProjectTaskService : IBaseService<ProjectTask>
    {
        void LogChanges(ProjectTask newTask, ProjectTask oldTask, ApplicationUser signedInUser);

        Task<List<ProjectTask>> AllWithReferencesAsync();
        Task<ProjectTask> FindWithReferencesAsync(int id);
        Task<ProjectTask> FindWithReferencesNoTrackingAsync(int id);
    }
}
