using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Interfaces;
using Interfaces.Repositories;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class CustomFieldRepository : EFRepository<CustomField>, ICustomFieldRepository
    {
        public CustomFieldRepository(IDataContext dbContext) : base(dbContext)
        {
            
        }
    }
}
