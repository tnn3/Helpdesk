using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using Interfaces.Base;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserTitleController : Controller
    {
        private readonly IUnitOfWork _uow;

        public UserTitleController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Admin/UserTitle
        public async Task<IActionResult> Index()
        {
            return View(await _uow.UserTitles.AllAsync());
        }

        // GET: Admin/UserTitle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTitle = await _uow.UserTitles.FindAsync(id);
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
        public async Task<IActionResult> Create(UserTitle userTitle)
        {
            if (!ModelState.IsValid) return View(userTitle);

            _uow.UserTitles.Add(userTitle);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/UserTitle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTitle = await _uow.UserTitles.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, UserTitle userTitle)
        {
            if (id != userTitle.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(userTitle);
            try
            {
                _uow.UserTitles.Update(userTitle);
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_uow.UserTitles.Exists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/UserTitle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTitle = await _uow.UserTitles.FindAsync(id);
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
            var userTitle = await _uow.UserTitles.FindAsync(id);
            _uow.UserTitles.Remove(userTitle);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
