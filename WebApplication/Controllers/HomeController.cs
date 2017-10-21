using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectTaskService _projectTaskService;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IProjectTaskService projectTaskService,
            UserManager<ApplicationUser> userManager)
        {
            _projectTaskService = projectTaskService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var tasks = await _projectTaskService.AllBefore(user.LoggedIn.AddDays(-2));
                vm.Tasks = tasks.OrderByDescending(t => t.ModifiedAt);
            }
            return View(vm);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
