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
        private readonly IRepository<ApplicationUser> _userRepository;

        //for unit tests
        public ProjectTaskService()
        {
            
        }

        public ProjectTaskService(IProjectTaskRepository projectTaskRepository,
            IRepository<Status> statusRepository,
            IRepository<Priority> priorityRepository,
            IRepository<ApplicationUser> userRepository) : base(projectTaskRepository)
        {
            _projectTaskRepository = projectTaskRepository;
            _priorityRepository = priorityRepository;
            _statusRepository = statusRepository;
            _userRepository = userRepository;
        }

        public override Task AddAsync(ProjectTask entity, ApplicationUser signedInUser)
        {
            entity.CreatedAt = DateTime.Now;
            entity.CreatedById = signedInUser.Id;
            entity.ModifiedAt = DateTime.Now;
            entity.ModifiedById = signedInUser.Id;
            return base.AddAsync(entity);
        }

        public override ProjectTask Update(ProjectTask newTask, ApplicationUser signedInUser)
        {
            var oldTask = _projectTaskRepository.FindWithReferencesNoTrackingAsync(newTask.Id).Result;
            if (newTask.AssigneeId != null)
            {
                newTask.Assignee = _userRepository.Find(newTask.AssigneeId);
            }
            newTask.Status = _statusRepository.Find(newTask.StatusId);
            newTask.Priority = _priorityRepository.Find(newTask.PriorityId);
            LogChanges(newTask, oldTask, signedInUser);
            newTask.CreatedById = oldTask.CreatedById;
            newTask.CreatedAt = oldTask.CreatedAt;
            newTask.ModifiedAt = DateTime.Now;
            newTask.ModifiedById = signedInUser.Id;

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

        public Task<List<ProjectTask>> AllBefore(DateTime dateTime)
        {
            return _projectTaskRepository.AllBefore(dateTime);
        }

        public void LogChanges(ProjectTask newTask, ProjectTask oldTask, ApplicationUser signedInUser)
        {
            var changes = new List<Change>();
            if (newTask.PaidWork != oldTask.PaidWork)
            {
                changes.Add(new Change
                {
                    Before = oldTask.PaidWork.ToString(),
                    After = newTask.PaidWork.ToString(),
                    Fieldname = nameof(oldTask.PaidWork)
                });
            }
            if (newTask.PriorityId != oldTask.PriorityId)
            {
                changes.Add(new Change
                {
                    Before = oldTask.Priority.Name,
                    After = newTask.Priority.Name,
                    Fieldname = nameof(oldTask.Priority)
                });
            }
            if (newTask.AmountDone != oldTask.AmountDone)
            {
                changes.Add(new Change
                {
                    Before = oldTask.AmountDone.ToString(),
                    After = newTask.AmountDone.ToString(),
                    Fieldname = nameof(oldTask.AmountDone)
                });
            }
            if (newTask.ComponentPrice != oldTask.ComponentPrice)
            {
                changes.Add(new Change
                {
                    Before = oldTask.ComponentPrice.ToString(CultureInfo.InvariantCulture),
                    After = newTask.ComponentPrice.ToString(CultureInfo.InvariantCulture),
                    Fieldname = nameof(oldTask.ComponentPrice)
                });
            }
            if (newTask.Price != oldTask.Price)
            {
                changes.Add(new Change
                {
                    Before = oldTask.Price.ToString(CultureInfo.InvariantCulture),
                    After = newTask.Price.ToString(CultureInfo.InvariantCulture),
                    Fieldname = nameof(oldTask.Price)
                });
            }
            if (newTask.Title != oldTask.Title)
            {
                changes.Add(new Change
                {
                    Before = oldTask.Title,
                    After = newTask.Title,
                    Fieldname = nameof(oldTask.Title)
                });
            }
            if (newTask.Description != oldTask.Description)
            {
                changes.Add(new Change
                {
                    Before = oldTask.Description,
                    After = newTask.Description,
                    Fieldname = nameof(oldTask.Description)
                });
            }
            if (newTask.ClientName != oldTask.ClientName)
            {
                changes.Add(new Change
                {
                    Before = oldTask.ClientName,
                    After = newTask.ClientName,
                    Fieldname = nameof(oldTask.ClientName)
                });
            }
            if (newTask.ClientEmail != oldTask.ClientEmail)
            {
                changes.Add(new Change
                {
                    Before = oldTask.ClientEmail,
                    After = newTask.ClientEmail,
                    Fieldname = nameof(oldTask.ClientEmail)
                });
            }
            if (newTask.ClientPhone != oldTask.ClientPhone)
            {
                changes.Add(new Change
                {
                    Before = oldTask.ClientPhone,
                    After = newTask.ClientPhone,
                    Fieldname = nameof(oldTask.ClientPhone)
                });
            }
            if (newTask.StatusId != oldTask.StatusId)
            {
                changes.Add(new Change
                {
                    Before = oldTask.Status.Name,
                    After = newTask.Status.Name,
                    Fieldname = nameof(oldTask.Status)
                });
            }

            if (newTask.AssigneeId != oldTask.AssigneeId)
            {
                changes.Add(new Change
                {
                    Before = oldTask.Assignee.FirstLastname,
                    After = newTask.Assignee.FirstLastname
                });
            }

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
