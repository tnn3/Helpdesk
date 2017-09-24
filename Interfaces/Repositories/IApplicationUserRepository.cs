using Domain;

namespace Interfaces.Repositories
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        bool IsDisabled(string id);
    }
}
