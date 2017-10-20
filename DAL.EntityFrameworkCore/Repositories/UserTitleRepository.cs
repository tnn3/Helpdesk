using System.Threading.Tasks;
using Domain;
using Interfaces;
using Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class UserTitleRepository : EFRepository<UserTitle>, IUserTitleRepository
    {
        public UserTitleRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public Task<UserTitle> FindWithReferencesAsync(int id)
        {
            return RepositoryDbSet
                .Include(p => p.Users)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
