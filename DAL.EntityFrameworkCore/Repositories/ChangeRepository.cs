using Domain;
using Interfaces;
using Interfaces.Repositories;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class ChangeRepository : EFRepository<Change>, IChangeRepository
    {
        public ChangeRepository(IDataContext dbContext) : base(dbContext)
        {
            
        }
    }
}
