using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VacationManager.Data;
using VacationManager.Models;
using VacationManager.Models.AdditionalModels;

namespace VacationManager.Controllers
{
    [Authorize(Roles = "CEO")]
    public class CeoController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public CeoController(UserManager<ApplicationUser> usermanager, ApplicationDbContext context, RoleManager<IdentityRole> rolemanager)
        {
            _userManager = usermanager;
            _context = context;
            _roleManager = rolemanager;
        }

        [HttpGet]
        public IActionResult Index()
        {
        
            CeoIndexModel model = new(_userManager)
            {
                Users = _userManager.Users.Include(u=>u.Team).ToList(),
                UserRoles = new Dictionary<ApplicationUser, string>(),
                Teams = _context.Teams.Include(t=>t.ApplicationUsers).ToList()
            };
            model.AddToList();
            return View(model);
        }
        [HttpGet]
        public IActionResult EditRole(string userId)
        {
            ViewBag.Roles = new SelectList(_context.Roles.AsNoTracking().ToList(), nameof(IdentityRole.Id), nameof(IdentityRole.Name));
            EditRoleViewModel viewModel = new EditRoleViewModel()
            {
                Id = _userManager.FindByIdAsync(userId).Result.Id,
                Email = _userManager.FindByIdAsync(userId).Result.Email,
                Username = _userManager.FindByIdAsync(userId).Result.UserName
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel viewModel, string roleId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);
                var role = _roleManager.FindByIdAsync(roleId).Result.Name;
                await _userManager.RemoveFromRolesAsync(user, _userManager.GetRolesAsync(user).Result);
                await _userManager.AddToRoleAsync(user, role);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult AddUserToTeam(string userId)
        {
            ViewBag.Teams = new SelectList(_context.Teams, "Id", "Name");
            AddUserToTeamModel toTeam =  new AddUserToTeamModel()
            {
                UserId = _userManager.FindByIdAsync(userId).Result.Id,
                Email = _userManager.FindByIdAsync(userId).Result.Email,
                UserName = _userManager.FindByIdAsync(userId).Result.UserName
            };
           
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View(toTeam);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToTeam(AddUserToTeamModel model, int teamId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
                if(user != null)
                {
                    user.TeamId = teamId;
                    _context.ApplicationUsers.Update(user);
                    team.ApplicationUsers.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
