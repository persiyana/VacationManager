using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VacationManager.Data;
using VacationManager.Models;

namespace VacationManager.Controllers
{
    [Authorize(Roles = "CEO")]
    public class TeamsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public TeamsController(UserManager<ApplicationUser> usermanager, ApplicationDbContext context, RoleManager<IdentityRole> rolemanager)
        {
            _userManager = usermanager;
            _context = context;
            _roleManager = rolemanager;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Teams.Include(t => t.Leader).Include(t => t.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Leader)
                .Include(t => t.Project)
                .Include(t => t.ApplicationUsers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["LeaderId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName");
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
            return View();
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProjectId,LeaderId")] Team team)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Teams.Add(team);
                _context.SaveChanges(); 
                var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == team.LeaderId);
                user.TeamId = team.Id;
                _context.SaveChanges();

                }
                catch (Exception)
                {
                    return RedirectToAction(nameof(Error), new { command= "Add"});
                }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaderId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", team.LeaderId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", team.ProjectId);
            return View(team);
        }

        public IActionResult Error(string command)
        {
            return View(model: command);
        }
        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            ViewData["LeaderId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", team.LeaderId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", team.ProjectId);
            return View(team);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProjectId,LeaderId")] Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    _context.SaveChanges();
                    var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == team.LeaderId);
                    user.TeamId = team.Id;
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    if (!TeamExists(team.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return RedirectToAction(nameof(Error), new { command = "Update" });
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeaderId"] = new SelectList(_context.ApplicationUsers, "Id", "UserName", team.LeaderId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", team.ProjectId);
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teams == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(t => t.Leader)
                .Include(t => t.Project)
                .Include(t => t.ApplicationUsers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teams == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Teams'  is null.");
            }
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                var users = _context.ApplicationUsers.ToListAsync().Result.Where(t => t.TeamId != null && t.TeamId == team.Id).ToList();
                for (int i = 0; i < users.Count; i++)
                {
                    users[i].TeamId = null;
                }
                team.ApplicationUsers = new List<ApplicationUser>();
                _context.SaveChanges();
                _context.Teams.Remove(team);
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
          return (_context.Teams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
