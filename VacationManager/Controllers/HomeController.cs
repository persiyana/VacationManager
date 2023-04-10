using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VacationManager.Models;
using Microsoft.AspNetCore.Authorization;
using VacationManager.Models.AdditionalModels;
using System.Security.Claims;
using VacationManager.Data;
using Microsoft.AspNetCore.Identity;

namespace VacationManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            _logger = logger;
            _userManager = usermanager;
            _roleManager = rolemanager;
        }

        public async Task<IActionResult> Index(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _userManager.FindByIdAsync(userId).Result;
                var userRoles = await _userManager.GetRolesAsync(user);
                HomePageModel model = new HomePageModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName,
                    Role = userRoles[0]
                };
            
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}