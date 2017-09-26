using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Repositories;
using Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ProjectTaskController : Controller
    {
        private readonly IBaseService<Status> _statusService;
        private readonly IBaseService<Priority> _priorityService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTaskController(
            IBaseService<Status> statusService,
            IBaseService<Priority> priorityService,  
            SignInManager<ApplicationUser> signInManager,
            IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
            _signInManager = signInManager;
            _statusService = statusService;
            _priorityService = priorityService;
        }

        // GET: ProjectTask
        public async Task<IActionResult> Index()
        {
            return View(await _projectTaskService.AllWithReferencesAsync());
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

            var projectTask = await _projectTaskService.FindAsync(id);
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

        public async Task PopulateViewModel(ProjectTaskCreateEditViewModel vm, ProjectTask projectTask = null)
        {
            vm.Priorities = new SelectList(await _priorityService.AllAsync(),
                nameof(Priority.Id),
                nameof(Priority.Name));
            vm.Statuses = new SelectList(await _statusService.AllAsync(),
                nameof(Status.Id),
                nameof(Status.Name));

            if (projectTask != null)
            {
                vm.ProjectTask = projectTask;
            }
        }
    }
}
