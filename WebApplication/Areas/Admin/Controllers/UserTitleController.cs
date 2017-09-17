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
    public class UserTitleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserTitleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserTitle
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserTitles.ToListAsync());
        }

        // GET: Admin/UserTitle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTitle = await _context.UserTitles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userTitle == null)
            {
                return NotFound();
            }

            return View(userTitle);
        }

        // GET: Admin/UserTitle/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserTitle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserTitleId,Title")] UserTitle userTitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userTitle);
        }

        // GET: Admin/UserTitle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTitle = await _context.UserTitles.SingleOrDefaultAsync(m => m.Id == id);
            if (userTitle == null)
            {
                return NotFound();
            }
            return View(userTitle);
        }

        // POST: Admin/UserTitle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserTitleId,Title")] UserTitle userTitle)
        {
            if (id != userTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTitleExists(userTitle.Id))
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
            return View(userTitle);
        }

        // GET: Admin/UserTitle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTitle = await _context.UserTitles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userTitle == null)
            {
                return NotFound();
            }

            return View(userTitle);
        }

        // POST: Admin/UserTitle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTitle = await _context.UserTitles.SingleOrDefaultAsync(m => m.Id == id);
            _context.UserTitles.Remove(userTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTitleExists(int id)
        {
            return _context.UserTitles.Any(e => e.Id == id);
        }
    }
}
