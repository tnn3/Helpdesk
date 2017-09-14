using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Interfaces;
using Interfaces.Base;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class ChangeRepository : EFRepository<Change>, IChangeRepository
    {
        public ChangeRepository(IDataContext dbContext) : base(dbContext)
        {
            
        }
    }
}
