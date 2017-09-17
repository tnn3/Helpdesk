using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Interfaces.Base;

namespace Interfaces
{
    public interface IProjectTaskRepository : IRepository<ProjectTask>
    {
        Task<List<ProjectTask>> AllWithReferencesAsync();
        Task<ProjectTask> FindWithReferencesAsync(int id);
    }
}
