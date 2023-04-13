using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.Vacations.Where(v=> v.ApplicationUserId==userId);
            return View(await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "CEO")]
        public async Task<IActionResult> IndexCeo()
        {
            var applicationDbContext = _context.Vacations.Include(v => v.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Vacations/Edit/5
        [Authorize(Roles = "CEO")]
        public async Task<IActionResult> EditCeo(int? id)
        {
            if (id == null || _context.Vacations == null)
            {
                return NotFound();
            }

            var vacation = await _context.Vacations.Include(v=>v.ApplicationUser).FirstOrDefaultAsync(v=>v.Id==id);
            if (vacation == null)
            {
                return NotFound();
            }
            CeoEditUserVacationModel editUserVacation = new CeoEditUserVacationModel()
            {
                Id = vacation.Id,
                ApplicationUserId = vacation.ApplicationUserId,
                ApplicationUserName = vacation.ApplicationUser.UserName,
                StartDate = vacation.StartDate,
                EndDate = vacation.EndDate,
                RequestCreationDate = vacation.RequestCreationDate,
                HalfDayVacation = vacation.HalfDayVacation,
                Approved = vacation.Approved,
                VacationOption = vacation.VacationOption,
                FilePath = vacation.FilePath
            };
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vacation.ApplicationUserId);
            return View(editUserVacation);
        }

        // POST: Vacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCeo(int id, [Bind("Id,ApplicationUserId,StartDate,EndDate,RequestCreationDate,HalfDayVacation,Approved,VacationOption,FilePath")] CeoEditUserVacationModel vacationViewModel)
        {
            if (id != vacationViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Vacation vacation = await _context.Vacations.FindAsync(id);
                    vacation.Approved = vacationViewModel.Approved;
                    _context.Update(vacation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExists(vacationViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexCeo));
            }
            //ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vacation.ApplicationUserId);
            //ViewData["VacationOptions"] = new SelectList(_context.Vacations, "Id", "VacationOption", vacation.VacationOption);
            return View(vacationViewModel);
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
        [Authorize(Roles = "CEO")]
        public async Task<IActionResult> DetailsCeo(int? id)
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
            EditUserVacationModel editUserVacation = new EditUserVacationModel() {
                Id =vacation.Id,
                StartDate = vacation.StartDate,
                EndDate = vacation.EndDate,
                RequestCreationDate = vacation.RequestCreationDate,
                HalfDayVacation = vacation.HalfDayVacation,
                Approved = vacation.Approved,
                VacationOption = vacation.VacationOption,
                FilePath = vacation.FilePath
            };
            ViewData["ApplicationUserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", vacation.ApplicationUserId);
            return View(editUserVacation);
        }

        // POST: Vacations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,StartDate,EndDate,RequestCreationDate,HalfDayVacation,Approved,VacationOption,FilePath")] EditUserVacationModel vacationViewModel)
        {
            if (id != vacationViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Vacation vacation = await _context.Vacations.FindAsync(id);
                    vacation.StartDate = vacationViewModel.StartDate;
                    vacation.EndDate = vacationViewModel.EndDate;
                    vacation.RequestCreationDate = vacationViewModel.RequestCreationDate;
                    vacation.HalfDayVacation = vacationViewModel.HalfDayVacation;
                    vacation.VacationOption = vacationViewModel.VacationOption;
                    _context.Update(vacation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VacationExists(vacationViewModel.Id))
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
            return View(vacationViewModel);
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
