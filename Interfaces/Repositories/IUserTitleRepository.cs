using System.Threading.Tasks;
using Domain;

namespace Interfaces.Repositories
{
    public interface IUserTitleRepository : IRepository<UserTitle>
    {
        Task<UserTitle> FindWithReferencesAsync(int id);
    }
}
