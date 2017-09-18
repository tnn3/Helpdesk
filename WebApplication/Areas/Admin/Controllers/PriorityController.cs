using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Base;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PriorityController : Controller
    {
        private readonly IUnitOfWork _uow;

        public PriorityController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/Priority
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Priorities.AllAsync());
        }

        // GET: Admin/Priority/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _uow.Priorities.FindAsync(id);
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
                _uow.Priorities.Add(priority);
                await _uow.SaveChangesAsync();
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

            var priority = await _uow.Priorities.FindAsync(id);
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
                _uow.Priorities.Update(priority);
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_uow.Priorities.Exists(priority.Id))
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

            var priority = await _uow.Priorities.FindAsync(id.Value);
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
            var priority = await _uow.Priorities.FindAsync(id);
            _uow.Priorities.Remove(priority);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
