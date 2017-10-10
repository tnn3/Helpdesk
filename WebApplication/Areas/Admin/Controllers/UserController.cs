using System.Linq;
using System.Threading.Tasks;
using Domain;
using Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Areas.Admin.ViewModels;

namespace WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _userRepository;
        private readonly IRepository<UserTitle> _titleRepository;

        public UserController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IApplicationUserRepository userRepository,
            IRepository<UserTitle> titleRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _titleRepository = titleRepository;
        }

        // GET: /Users/
        public async Task<ActionResult> Index()
        {
            return View(await _userManager.Users.Include(u => u.Title).ToListAsync());
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
            var vm = new UserCreateEditViewModel
            {
                RoleSelectList = new SelectList(await _roleManager.Roles.ToListAsync(), "Id", "Name"),
                User = user,
                TitleSelectList = new SelectList(await _titleRepository.AllAsync(), "Id", "Title")
            };

            return View(vm);
        }

        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserCreateEditViewModel vm, string id)
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
            if (!string.IsNullOrEmpty(vm.RoleId))
            {
                role = await _roleManager.FindByIdAsync(vm.RoleId);
                if (role == null)
                {
                    return NotFound();
                }
            }
            user.UserName = vm.User.UserName;
            user.Firstname = vm.User.Firstname;
            user.Lastname = vm.User.Lastname;
            user.Email = vm.User.Email;
            user.TitleId = vm.User.TitleId;
            if (ModelState.IsValid)
            {
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    ModelState.AddModelError("", updateResult.Errors.First().ToString());
                    vm.RoleSelectList = new SelectList(_roleManager.Roles, "Id", "Name");
                    vm.TitleSelectList = new SelectList(await _titleRepository.AllAsync(), "Id", "Title");
                    return View(vm);
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
                if (addRole.Succeeded)
                {
                    await _userManager.UpdateAsync(user);
                }
                if (addRole.Succeeded) return RedirectToAction("Index");

                ModelState.AddModelError("", addRole.Errors.First().ToString());
            }

            vm.RoleSelectList = new SelectList(_roleManager.Roles, "Id", "Name");
            vm.TitleSelectList = new SelectList(await _titleRepository.AllAsync(), "Id", "Title");
            return View(vm);
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
            if (!ModelState.IsValid) return View();
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            var user = await _userRepository.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                foreach (var userRole in userRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, userRole);
                }
            }
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}