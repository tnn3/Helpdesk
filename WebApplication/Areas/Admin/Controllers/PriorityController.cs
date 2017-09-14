using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EntityFrameworkCore;
using Domain;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PriorityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PriorityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Priority
        public async Task<IActionResult> Index()
        {
            return View(await _context.Priorities.ToListAsync());
        }

        // GET: Admin/Priority/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _context.Priorities
                .SingleOrDefaultAsync(m => m.PriorityId == id);
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
        public async Task<IActionResult> Create([Bind("PriorityId,Name")] Priority priority)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priority);
                await _context.SaveChangesAsync();
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

            var priority = await _context.Priorities.SingleOrDefaultAsync(m => m.PriorityId == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("PriorityId,Name")] Priority priority)
        {
            if (id != priority.PriorityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priority);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriorityExists(priority.PriorityId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(priority);
        }

        // GET: Admin/Priority/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priority = await _context.Priorities
                .SingleOrDefaultAsync(m => m.PriorityId == id);
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
            var priority = await _context.Priorities.SingleOrDefaultAsync(m => m.PriorityId == id);
            _context.Priorities.Remove(priority);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriorityExists(int id)
        {
            return _context.Priorities.Any(e => e.PriorityId == id);
        }
    }
}
