using Domain;
using Interfaces;
using Interfaces.Base;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class ApplicationUserRepository : EFRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IDataContext dbContext) : base(dbContext)
        {
            
        }

        public bool IsDisabled(string id)
        {
            return RepositoryDbSet.Find(id).IsDisabled;
        }
    }
}
