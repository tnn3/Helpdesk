using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Extensions;
using WebApplication.ViewModels;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ProjectTaskController : Controller
    {
        private readonly IBaseService<Status> _statusService;
        private readonly IBaseService<Priority> _priorityService;
        private readonly IBaseService<ApplicationUser> _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTaskController(
            IBaseService<Status> statusService,
            IBaseService<Priority> priorityService,  
            IBaseService<ApplicationUser> userService,  
            SignInManager<ApplicationUser> signInManager,
            IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
            _signInManager = signInManager;
            _statusService = statusService;
            _priorityService = priorityService;
            _userService = userService;
        }

        // GET: ProjectTask
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["SortId"] = string.IsNullOrEmpty(sortOrder) ? "IdDesc" : "";
            ViewData["SortStatus"] = sortOrder == "Status" ? "StatusDesc" : "Status";
            ViewData["SortDone"] = sortOrder == "AmountDone" ? "AmountDoneDesc" : "AmountDone";
            ViewData["SortPriority"] = sortOrder == "Priority" ? "PriorityDesc" : "Priority";
            ViewData["SortTitle"] = sortOrder == "Title" ? "TitleDesc" : "Title";
            ViewData["SortClient"] = sortOrder == "ClientName" ? "ClientNameDesc" : "ClientName";
            ViewData["SortAssignee"] = sortOrder == "Assignee" ? "AssigneeDesc" : "Assignee";
            ViewData["SortModifiedAt"] = sortOrder == "ModifiedAt" ? "ModifiedAtDesc" : "ModifiedAt";

            if (searchString == null) searchString = currentFilter;
            else page = 1;

            ViewData["CurrentFilter"] = searchString;

            var tasks = await _projectTaskService.AllWithReferencesAsync();
            tasks = SearchTasks(tasks, searchString);
            tasks = SortTasks(tasks, sortOrder);

            var pageSize = 10;

            return View(PaginatedList<ProjectTask>.Create(tasks, page ?? 1, pageSize));
        }

        // GET: ProjectTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _projectTaskService.FindWithReferencesAsync(id.Value);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTask/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ProjectTaskCreateEditViewModel();
            await PopulateViewModel(vm);

            return View(vm);
        }

        // POST: ProjectTask/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTaskCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var signedInUser = _signInManager.UserManager.GetUserAsync(User);
                await _projectTaskService.AddAsync(vm.ProjectTask, signedInUser.Result);
                await _projectTaskService.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            await PopulateViewModel(vm);
            return View(vm);
        }

        // GET: ProjectTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _projectTaskService.FindWithReferencesAsync(id.Value);
            if (projectTask == null)
            {
                return NotFound();
            }

            var vm = new ProjectTaskCreateEditViewModel();
            await PopulateViewModel(vm, projectTask);

            return View(vm);
        }

        // POST: ProjectTask/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTaskCreateEditViewModel vm)
        {
            if (id != vm.ProjectTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var signedInUser = await _signInManager.UserManager.GetUserAsync(User);
                    _projectTaskService.Update(vm.ProjectTask, signedInUser);

                    await _projectTaskService.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_projectTaskService.Exists(vm.ProjectTask.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            await PopulateViewModel(vm);
            return View(vm);
        }

        // GET: ProjectTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _projectTaskService.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTask/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var projectTask = await _projectTaskService.FindAsync(id);
            _projectTaskService.Remove(projectTask);
            await _projectTaskService.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateViewModel(ProjectTaskCreateEditViewModel vm, ProjectTask projectTask = null)
        {
            vm.Priorities = new SelectList(await _priorityService.AllAsync(),
                nameof(Priority.Id),
                nameof(Priority.Name));
            vm.Statuses = new SelectList(await _statusService.AllAsync(),
                nameof(Status.Id),
                nameof(Status.Name));
            vm.Assignees = new SelectList(await _userService.AllAsync(),
                nameof(ApplicationUser.Id),
                nameof(ApplicationUser.FirstLastname));
            if (projectTask != null)
            {
                vm.ProjectTask = projectTask;
            }
        }

        private static List<ProjectTask> SortTasks(List<ProjectTask> tasks, string sortBy)
        {
            switch (sortBy)
            {
                case "IdDesc":
                    tasks = tasks.OrderByDescending(t => t.Id).ToList();
                    break;
                case "Status":
                    tasks = tasks.OrderBy(t => t.Status.Name).ToList();
                    break;
                case "StatusDesc":
                    tasks = tasks.OrderByDescending(t => t.Status.Name).ToList();
                    break;
                case "AmountDone":
                    tasks = tasks.OrderBy(t => t.AmountDone).ToList();
                    break;
                case "AmountDoneDesc":
                    tasks = tasks.OrderByDescending(t => t.AmountDone).ToList();
                    break;
                case "Priority":
                    tasks = tasks.OrderBy(t => t.Priority.Name).ToList();
                    break;
                case "PriorityDesc":
                    tasks = tasks.OrderByDescending(t => t.Priority.Name).ToList();
                    break;
                case "Title":
                    tasks = tasks.OrderBy(t => t.Title).ToList();
                    break;
                case "TitleDesc":
                    tasks = tasks.OrderByDescending(t => t.Title).ToList();
                    break;
                case "ClientName":
                    tasks = tasks.OrderBy(t => t.ClientName).ToList();
                    break;
                case "ClientNameDesc":
                    tasks = tasks.OrderByDescending(t => t.ClientName).ToList();
                    break;
                case "Assignee":
                    tasks = tasks.OrderBy(t => t.Assignee).ToList();
                    break;
                case "AssigneeDesc":
                    tasks = tasks.OrderByDescending(t => t.Assignee).ToList();
                    break;
                case "ModifiedAt":
                    tasks = tasks.OrderBy(t => t.ModifiedAt).ToList();
                    break;
                case "ModifiedAtDesc":
                    tasks = tasks.OrderByDescending(t => t.ModifiedAt).ToList();
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Id).ToList();
                    break;
            }
            return tasks;
        }

        private static List<ProjectTask> SearchTasks(List<ProjectTask> tasks, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.ToLower().Contains(searchString)
                                         || t.Description.ToLower().Contains(searchString)
                                         || t.ClientName.ToLower().Contains(searchString)
                                         || t.ClientPhone.Contains(searchString)
                                         || t.ClientEmail.Contains(searchString)
                ).ToList();
            }

            return tasks;
        }
    }
}
