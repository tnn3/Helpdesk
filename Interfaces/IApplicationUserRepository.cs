using Domain;
using Interfaces.Base;

namespace Interfaces
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        bool IsDisabled(string id);
    }
}
