using System.Linq;
using System.Threading.Tasks;
using Domain;
using Interfaces.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models.AccountViewModels;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _uow;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork uow)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _uow = uow;
        }

        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel vm, string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return BadRequest();
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = vm.Email,
                    Email = vm.Email
                };
                var createUser = await _userManager.CreateAsync(user, vm.Password);

                if (createUser.Succeeded)
                {
                    var addRole = await _userManager.AddToRoleAsync(user, role.Name);

                    if (addRole.Succeeded) return RedirectToAction("Index");
                    ModelState.AddModelError("", addRole.Errors.First().ToString());
                }
                ModelState.AddModelError("", createUser.Errors.First().ToString());
            }
            ViewBag.roleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Id", "Name");
            return View();
        }

        // GET: /Users/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.roleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Id", "Name");
            return View(user);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ApplicationUser formUser, string id, string roleId)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            IdentityRole role = null;
            if (!string.IsNullOrEmpty(roleId))
            {
                role = await _roleManager.FindByIdAsync(roleId);
                if (role == null)
                {
                    return NotFound();
                }
            }
            user.UserName = formUser.UserName;
            if (ModelState.IsValid)
            {
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError("", updateResult.Errors.First().ToString());
                    ViewBag.roleId = new SelectList(_roleManager.Roles, "Id", "Name");
                    return View();
                }

                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Any())
                {
                    foreach (var userRole in userRoles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole);
                    }
                }

                if (role == null) return RedirectToAction("Index");

                var addRole = await _userManager.AddToRoleAsync(user, role.Name);
                if (addRole.Succeeded) return RedirectToAction("Index");

                ModelState.AddModelError("", addRole.Errors.First().ToString());
            }
            ViewBag.roleId = new SelectList(_roleManager.Roles, "Id", "Name");
            return View();
        }

        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(id))
                {
                    return BadRequest();
                }

                var user = await _uow.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                /*var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Any())
                {
                    foreach (var userRole in userRoles)
                    {
                        await _userManager.RemoveFromRoleAsync(user, userRole);
                    }
                }*/
                user.IsDisabled = true;
                await _uow.SaveChangesAsync();
                //await _userManager.DeleteAsync(user);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}