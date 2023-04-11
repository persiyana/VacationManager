using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VacationManager.Models;
using Microsoft.AspNetCore.Authorization;
using VacationManager.Models.AdditionalModels;
using System.Security.Claims;
using VacationManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace VacationManager.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, UserManager<ApplicationUser> usermanager, RoleManager<IdentityRole> rolemanager)
        {
            _logger = logger;
            _userManager = usermanager;
            _roleManager = rolemanager;
            _context = context;
        }

        public async Task<IActionResult> Index()
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

        [HttpGet]
        public async Task<IActionResult> EditUser()
        {
         
            string username = User.Identity.Name;
            ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.Equals(username));
            EditUserAccountModel editUser = new EditUserAccountModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName
            };
            return View(editUser);

        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserAccountModel userAccountModel)
        {
            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.Equals(username));
                user.FirstName = userAccountModel.FirstName;
                user.LastName = userAccountModel.LastName;
                user.Email = userAccountModel.Email;
                user.UserName = userAccountModel.Username;

                //_context.Entry(user).State = EntityState.Modified;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}