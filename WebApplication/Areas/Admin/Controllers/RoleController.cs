using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        // GET: /Roles
        public ActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        // GET: /Roles/Details
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var role = await _roleManager.FindByIdAsync(id);
            return View(role);
        }

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public async Task<ActionResult> Create(IdentityRole roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(roleViewModel.Name);
                var roleresult = await _roleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First().ToString());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: /Roles/Edit/Admin
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: /Roles/Edit/5
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                IdentityRole prevRole = await _roleManager.FindByIdAsync(role.Id);
                prevRole.Name = role.Name;
                var result = await _roleManager.UpdateAsync(prevRole);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: /Roles/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                {
                    return NotFound();
                }
                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First().ToString());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}