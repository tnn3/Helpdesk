using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Interfaces.Repositories
{
    public interface IProjectTaskRepository : IRepository<ProjectTask>
    {
        Task<List<ProjectTask>> AllWithReferencesAsync();
        Task<ProjectTask> FindWithReferencesAsync(int id);
    }
}
