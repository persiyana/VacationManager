using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VacationManager.Data;
using VacationManager.Models;
using VacationManager.Models.AdditionalModels;

namespace VacationManager.Controllers
{
    public class VacationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VacationsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Vacations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vacations.Include(v => v.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vacations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vacations == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations
                .Include(v => v.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacation == null)
            {
                return NotFound();
            }

            return View(vacation);
        }

        // GET: Vacations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vacations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVacationModel vacationModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    string username = User.Identity.Name;
                    ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.Equals(username));
                    string photoName = UploadPhoto("Vacations", vacationModel.Image, _webHostEnvironment);

                    Vacation vacation = new Vacation()
                    {
                        ApplicationUserId = user.Id,
                        StartDate = vacationModel.StartDate,
                        EndDate = vacationModel.EndDate,
                        RequestCreationDate = vacationModel.RequestCreationDate,
                        HalfDayVacation = vacationModel.HalfDayVacation,
                        VacationOption = vacationModel.VacationOption,
                        Approved =  false,
                        FilePath = photoName
                    };
                    _context.Vacations.Add(vacation);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Vacation created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Vacations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vacations == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations.FindAsync(id);
            if (vacation == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vacation.ApplicationUserId);
            return View(vacation);
        }

        // POST: Vacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,StartDate,EndDate,RequestCreationDate,HalfDayVacation,Approved,VacationOption,FilePath")] Vacation vacation)
        {
            if (id != vacation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vacation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExists(vacation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vacation.ApplicationUserId);
            return View(vacation);
        }

        // GET: Vacations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vacations == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations
                .Include(v => v.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vacation == null)
            {
                return NotFound();
            }

            return View(vacation);
        }

        // POST: Vacations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vacations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vacations'  is null.");
            }
            var vacation = await _context.Vacations.FindAsync(id);
            
            if (vacation != null)
            {
                DeleteImage("Vacations", vacation.FilePath, _context, _webHostEnvironment);
                _context.Vacations.Remove(vacation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VacationExists(int id)
        {
          return (_context.Vacations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public static string UploadPhoto(string imageType, IFormFile formFile, IWebHostEnvironment webHostEnvironment)
        {
            string uniqueFileName = null;

            if (formFile != null)
            {
                string photosFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", imageType);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                string photoPathAndName = Path.Combine(photosFolder, uniqueFileName);

                Directory.CreateDirectory(photosFolder);
                using FileStream fileStream = new(photoPathAndName, FileMode.Create);

                formFile.CopyTo(fileStream);
            }

            return uniqueFileName;
        }
        public static async Task<bool> DeleteImage(string imageType, string imageUrl, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            string profilePhotoFileName = Path.Combine(webHostEnvironment.WebRootPath, "Images", imageType, imageUrl);

           
                if (System.IO.File.Exists(profilePhotoFileName))
                {
                    System.IO.File.Delete(profilePhotoFileName);
                    return true;
                }
            
            return false;
        }
    }
}
