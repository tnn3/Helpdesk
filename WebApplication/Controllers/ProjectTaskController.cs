using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ProjectTaskController : Controller
    {
        private readonly IProjectTaskRepository _projectTaskRepository;
        private readonly IRepository<Status> _statusRepo;
        private readonly IRepository<Priority> _priorityRepo;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProjectTaskController(
            IProjectTaskRepository projectTaskRepository, 
            IRepository<Status> statusRepository, 
            IRepository<Priority> priorityRepository,  
            SignInManager<ApplicationUser> signInManager)
        {
            _projectTaskRepository = projectTaskRepository;
            _signInManager = signInManager;
            _statusRepo = statusRepository;
            _priorityRepo = priorityRepository;
        }

        // GET: ProjectTask
        public async Task<IActionResult> Index()
        {
            return View(await _projectTaskRepository.AllWithReferencesAsync());
        }

        // GET: ProjectTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _projectTaskRepository.FindWithReferencesAsync(id.Value);
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
                var userId = _signInManager.UserManager.GetUserId(User);
                vm.ProjectTask.CreatedAt = DateTime.Now;
                vm.ProjectTask.CreatedById = userId;
                vm.ProjectTask.ModifiedAt = DateTime.Now;
                vm.ProjectTask.ModifiedById = userId;
                _projectTaskRepository.Add(vm.ProjectTask);
                await _projectTaskRepository.SaveChangesAsync();

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

            var projectTask = await _projectTaskRepository.FindAsync(id);
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
                    _projectTaskRepository.Update(vm.ProjectTask);

                    await _projectTaskRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_projectTaskRepository.Exists(vm.ProjectTask.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // GET: ProjectTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _projectTaskRepository.FindAsync(id);
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
            var projectTask = await _projectTaskRepository.FindAsync(id);
            _projectTaskRepository.Remove(projectTask);
            await _projectTaskRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task PopulateViewModel(ProjectTaskCreateEditViewModel vm, ProjectTask projectTask = null)
        {
            vm.Priorities = new SelectList(await _priorityRepo.AllAsync(),
                nameof(Priority.Id),
                nameof(Priority.Name));
            vm.Statuses = new SelectList(await _statusRepo.AllAsync(),
                nameof(Status.Id),
                nameof(Status.Name));

            if (projectTask != null)
            {
                vm.ProjectTask = projectTask;
            }
        }
    }
}
