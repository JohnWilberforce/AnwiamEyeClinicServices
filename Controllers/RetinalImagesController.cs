using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnwiamEyeClinicServices.Models;

namespace AnwiamEyeClinicServices.Controllers
{
    [Route("[Controller]/[action]")]
    public class RetinalImagesController : Controller
    {
        private readonly AnwiamServicesContext _context;

        public RetinalImagesController()
        {
            _context = new AnwiamServicesContext();
        }

        // GET: RetinalImages
        public async Task<IActionResult> Index()
        {
              return _context.RetinalImages != null ? 
                          View(await _context.RetinalImages.ToListAsync()) :
                          Problem("Entity set 'AnwiamServicesContext.RetinalImages'  is null.");
        }
        public async Task<IActionResult> RetImagefromRevenue()
        {

            var formattedData = DateTime.Now.Date;
            var l1 = await _context.RevenueServicies.Where(x => x.Services.Contains("RetinalImaging")).ToListAsync();
            var l2 = l1.Where(x => x.Date == formattedData).ToList();
            return _context.RevenueServicies != null ?
                        View(l2) :
                        Problem("Entity set 'VFT_OCTContext.Vfts'  is null.");

        }

        // GET: RetinalImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RetinalImages == null)
            {
                return NotFound();
            }

            var retinalImage = await _context.RetinalImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retinalImage == null)
            {
                return NotFound();
            }

            return View(retinalImage);
        }

        // GET: RetinalImages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RetinalImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,scanId,patientName,referredDrName,reFfacility,date")] RetinalImage retinalImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(retinalImage);
                await _context.SaveChangesAsync();
                ViewBag.success = "Saved";
                return View();
            }
            return View(retinalImage);
        }

        [HttpPost]
        public ViewResult DailyRecords(DateTime date1)
        {
            List<RetinalImage> cons = null;
            try
            {
                cons = _context.RetinalImages.Where(x => x.date == date1).ToList();
                ViewBag.today = date1.ToString("dddd, dd MMMM yyyy");
                if (cons != null)
                {
                    return View(cons);
                }
                throw new Exception("No Data Found");
            }
            catch (Exception ex)
            {
                cons = null;
                return View(cons);
            }
        }
        [HttpPost]
        public async Task<ViewResult> ViewReportImaging(DateTime stDate, DateTime eDate)
        {
            List<ImageReport> vftreport = new List<ImageReport>();



            vftreport = await _context.ImageReports.FromSqlInterpolated($"select * from udf_GenerateImagingReport({stDate}, {eDate})").OrderByDescending(x => x.Amount).ToListAsync();
            return View(vftreport);


        }
        // GET: RetinalImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RetinalImages == null)
            {
                return NotFound();
            }

            var retinalImage = await _context.RetinalImages.FindAsync(id);
            if (retinalImage == null)
            {
                return NotFound();
            }
            return View(retinalImage);
        }

        // POST: RetinalImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,scanId,patientName,referredDrName,reFfacility,date")] RetinalImage retinalImage)
        {
            if (id != retinalImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(retinalImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetinalImageExists(retinalImage.Id))
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
            return View(retinalImage);
        }

        // GET: RetinalImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RetinalImages == null)
            {
                return NotFound();
            }

            var retinalImage = await _context.RetinalImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retinalImage == null)
            {
                return NotFound();
            }

            return View(retinalImage);
        }

        // POST: RetinalImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RetinalImages == null)
            {
                return Problem("Entity set 'AnwiamServicesContext.RetinalImages'  is null.");
            }
            var retinalImage = await _context.RetinalImages.FindAsync(id);
            if (retinalImage != null)
            {
                _context.RetinalImages.Remove(retinalImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetinalImageExists(int id)
        {
          return (_context.RetinalImages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
