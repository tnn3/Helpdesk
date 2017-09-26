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
    public class UserTitleController : Controller
    {
        private readonly IRepository<UserTitle> _userTitleRepository;

        public UserTitleController(IRepository<UserTitle> userTitleRepository)
        {
            _userTitleRepository = userTitleRepository;
        }

        // GET: Admin/UserTitle
        public async Task<IActionResult> Index()
        {
            return View(await _userTitleRepository.AllAsync());
        }

        // GET: Admin/UserTitle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTitle = await _userTitleRepository.FindAsync(id);
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

            _userTitleRepository.Add(userTitle);
            await _userTitleRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/UserTitle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTitle = await _userTitleRepository.FindAsync(id);
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
                _userTitleRepository.Update(userTitle);
                await _userTitleRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_userTitleRepository.Exists(id))
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

            var userTitle = await _userTitleRepository.FindAsync(id);
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
            var userTitle = await _userTitleRepository.FindAsync(id);
            _userTitleRepository.Remove(userTitle);
            await _userTitleRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
