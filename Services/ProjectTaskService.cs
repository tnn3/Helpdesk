using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repositories;
using Interfaces.Services;

namespace Services
{
    public class ProjectTaskService : BaseService<ProjectTask>, IProjectTaskService
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IRepository<Status> _statusRepository;
        private readonly IRepository<Priority> _priorityRepository;

        //for unit tests
        public ProjectTaskService()
        {
            
        }

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository,
            IRepository<Status> statusRepository,
            IRepository<Priority> priorityRepository) : base(projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _priorityRepository = priorityRepository;
            _statusRepository = statusRepository;
        }

        public override Task AddAsync(ProjectTask entity, ApplicationUser signedInUser)
        {
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = signedInUser;
            entity.ModifiedAt = DateTime.Now;
            entity.ModifiedBy = signedInUser;
            return base.AddAsync(entity);
        }

        public override ProjectTask Update(ProjectTask newTask, ApplicationUser signedInUser)
        {
            var oldTask = _projectTaskRepository.FindWithReferencesNoTrackingAsync(newTask.Id).Result;

            LogChanges(newTask, oldTask, signedInUser);
            newTask.CreatedBy = oldTask.CreatedBy;
            newTask.CreatedAt = oldTask.CreatedAt;
            newTask.ModifiedAt = DateTime.Now;
            newTask.ModifiedBy = signedInUser;

            return base.Update(newTask);
        }

        public Task<List<ProjectTask>> AllWithReferencesAsync()
        {
            return _projectTaskRepository.AllWithReferencesAsync();
        }

        public Task<ProjectTask> FindWithReferencesAsync(int id)
        {
            return _projectTaskRepository.FindWithReferencesAsync(id);
        }

        public Task<ProjectTask> FindWithReferencesNoTrackingAsync(int id)
        {
            return _projectTaskRepository.FindWithReferencesNoTrackingAsync(id);
        }

        public void LogChanges(ProjectTask newTask, ProjectTask oldTask, ApplicationUser signedInUser)
        {
            var changes = new List<Change>();
            if (newTask.PaidWork != oldTask.PaidWork)
            {
                changes.Add(new Change
                {
                    Before = oldTask.PaidWork.ToString(),
                    After = newTask.PaidWork.ToString()
                });
            }
            if (newTask.PriorityId != oldTask.PriorityId)
            {
                var priority = _priorityRepository.Find(newTask.PriorityId);
                changes.Add(new Change
                {
                    Before = oldTask.Priority.Name,
                    After = priority.Name
                });
            }
            if (newTask.AmountDone != oldTask.AmountDone)
            {
                changes.Add(new Change
                {
                    Before = oldTask.AmountDone.ToString(),
                    After = newTask.AmountDone.ToString()
                });
            }
            if (newTask.ComponentPrice != oldTask.ComponentPrice)
            {
                changes.Add(new Change
                {
                    Before = oldTask.ComponentPrice.ToString(CultureInfo.InvariantCulture),
                    After = newTask.ComponentPrice.ToString(CultureInfo.InvariantCulture)
                });
            }
            if (newTask.Price != oldTask.Price)
            {
                changes.Add(new Change
                {
                    Before = oldTask.Price.ToString(CultureInfo.InvariantCulture),
                    After = newTask.Price.ToString(CultureInfo.InvariantCulture)
                });
            }
            if (!newTask.Title.Equals(oldTask.Title))
            {
                changes.Add(new Change
                {
                    Before = oldTask.Title,
                    After = newTask.Title
                });
            }
            if (!newTask.Description.Equals(oldTask.Description))
            {
                changes.Add(new Change
                {
                    Before = oldTask.Description,
                    After = newTask.Description
                });
            }
            if (!newTask.ClientName.Equals(oldTask.ClientName))
            {
                changes.Add(new Change
                {
                    Before = oldTask.ClientName,
                    After = newTask.ClientName
                });
            }
            if (!newTask.ClientEmail.Equals(oldTask.ClientEmail))
            {
                changes.Add(new Change
                {
                    Before = oldTask.ClientEmail,
                    After = newTask.ClientEmail
                });
            }
            if (!newTask.ClientPhone.Equals(oldTask.ClientPhone))
            {
                changes.Add(new Change
                {
                    Before = oldTask.ClientPhone,
                    After = newTask.ClientPhone
                });
            }
            if (newTask.StatusId != oldTask.StatusId)
            {
                var status = _statusRepository.Find(newTask.StatusId);
                changes.Add(new Change
                {
                    Before = oldTask.Status.Name,
                    After = status.Name
                });
            }

            //add assignee logging

            if (changes.Count > 0)
            {
                newTask.ChangeSets.Add(new ChangeSet
                {
                    CreatedBy = signedInUser,
                    CreatedAt = DateTime.Now,
                    Changes = changes,
                    ModifiedBy = signedInUser,
                    ModifiedAt = DateTime.Now
                });
            }
        }
    }
}
