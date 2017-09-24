using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PriorityController : Controller
    {
        private readonly IRepository<Priority> _priorityRepository;

        public PriorityController(IRepository<Priority> priorityRepository)
        {
            _priorityRepository = priorityRepository;
        }

        // GET: Admin/Priority
        public async Task<IActionResult> Index()
        {
            return View(await _priorityRepository.AllAsync());
        }

        // GET: Admin/Priority/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _priorityRepository.FindAsync(id);
            if (priority == null)
            {
                return NotFound();
            }

            return View(priority);
        }

        // GET: Admin/Priority/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Priority/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Priority priority)
        {
            if (ModelState.IsValid)
            {
                _priorityRepository.Add(priority);
                await _priorityRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priority);
        }

        // GET: Admin/Priority/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _priorityRepository.FindAsync(id);
            if (priority == null)
            {
                return NotFound();
            }
            return View(priority);
        }

        // POST: Admin/Priority/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Priority priority)
        {
            if (id != priority.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(priority);
            try
            {
                _priorityRepository.Update(priority);
                await _priorityRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_priorityRepository.Exists(priority.Id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Priority/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _priorityRepository.FindAsync(id.Value);
            if (priority == null)
            {
                return NotFound();
            }

            return View(priority);
        }

        // POST: Admin/Priority/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priority = await _priorityRepository.FindAsync(id);
            _priorityRepository.Remove(priority);
            await _priorityRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
