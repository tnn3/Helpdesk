using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Interfaces;
using Interfaces.Base;

namespace DAL.EntityFrameworkCore.Repositories
{
    public class ProjectTaskRepository : EFRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(IDataContext dbContext) : base(dbContext)
        {

        }
    }
}
