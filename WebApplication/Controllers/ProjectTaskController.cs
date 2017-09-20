using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class ProjectTaskController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProjectTaskController(IUnitOfWork uow, SignInManager<ApplicationUser> signInManager)
        {
            _uow = uow;
            _signInManager = signInManager;
        }

        // GET: ProjectTask
        public async Task<IActionResult> Index()
        {
            return View(await _uow.ProjectTasks.AllWithReferencesAsync());
        }

        // GET: ProjectTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _uow.ProjectTasks.FindWithReferencesAsync(id.Value);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTask/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ProjectTaskCreateEditViewModel
            {
                Priorities = new SelectList(await _uow.Priorities.AllAsync(),
                    nameof(Priority.Id),
                    nameof(Priority.Name)),
                Statuses = new SelectList(await _uow.Statuses.AllAsync(),
                    nameof(Status.Id),
                    nameof(Status.Name))
            };

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
                _uow.ProjectTasks.Add(vm.ProjectTask);
                await _uow.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            vm.Priorities = new SelectList(await _uow.Priorities.AllAsync(),
                nameof(Priority.Id),
                nameof(Priority.Name));
            vm.Statuses = new SelectList(await _uow.Statuses.AllAsync(),
                nameof(Status.Id),
                nameof(Status.Name));
            return View(vm);
        }

        // GET: ProjectTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _uow.ProjectTasks.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }

            var vm = new ProjectTaskCreateEditViewModel
            {
                Priorities = new SelectList(await _uow.Priorities.AllAsync(),
                    nameof(Priority.Id),
                    nameof(Priority.Name)),
                Statuses = new SelectList(await _uow.Statuses.AllAsync(),
                    nameof(Status.Id),
                    nameof(Status.Name)),
                ProjectTask = projectTask
            };

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
                    _uow.ProjectTasks.Update(vm.ProjectTask);

                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.ProjectTasks.Exists(vm.ProjectTask.Id))
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

            var projectTask = await _uow.ProjectTasks.FindAsync(id);
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
            var projectTask = await _uow.ProjectTasks.FindAsync(id);
            _uow.ProjectTasks.Remove(projectTask);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
