using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnwiamEyeClinicServices.Models;
using Microsoft.Data.SqlClient;

namespace AnwiamEyeClinicServices.Controllers
{
    [Route("[Controller]/[Action]")]
    public class VFTController : Controller
    {
        private readonly AnwiamServicesContext _context;
        //AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context c;

        //SqlCommand Command { get; set; }
        //SqlConnection Connection { get; set; }

        public VFTController()
        {
            //c = new AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context();
            _context = new AnwiamServicesContext();

        }

        // GET: VFT
        public async Task<IActionResult> Index()
        {
            return _context.Vfts != null ?
                        View(await _context.Vfts.ToListAsync()) :
                        Problem("Entity set 'VFT_OCTContext.Vfts'  is null.");
        }

        // GET: VFT/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Vfts == null)
            {
                return NotFound();
            }

            var vft = await _context.Vfts
                .FirstOrDefaultAsync(m => m.ScanId == id);
            if (vft == null)
            {
                return NotFound();
            }

            return View(vft);
        }

        // GET: VFT/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VFT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScanId,PatientName,ReferredDrName,ReFfacility,Date")] Vft vft)
        {
            if (ModelState.IsValid)
            {
                if (_context.Vfts.Find(vft.ScanId) != null)
                {
                    ViewBag.tryAgain = "Scan Id Already Used!!";

                    return View();
                }
                _context.Add(vft);
                await _context.SaveChangesAsync();
               
                return View("Success");
            }
            return RedirectToAction("Create");
        }

        // GET: VFT/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var id1 = id;
            if (id == null || _context.Vfts == null)
            {
                return NotFound();
            }

            var vft = await _context.Vfts.FindAsync(id1);
            if (vft == null)
            {
                return NotFound();
            }
            return View(vft);
        }
        [HttpPost]
        public ViewResult DailyRecords(DateTime today)
        {
            List<Vft> vft = null;

            vft = _context.Vfts.Where(x => x.Date == today).ToList();
            ViewBag.today = today.ToString("dddd, dd MMMM yyyy");

            return View(vft);
        }

        [HttpPost]
        public ViewResult ViewReportVFT(DateTime stDate, DateTime eDate)
        {
            List<VFTreport> vftreport = new List<VFTreport>();



            vftreport = _context.VFTreports.FromSqlInterpolated($"select * from udf_GenerateReportByDate({stDate}, {eDate})").OrderByDescending(x => x.Amount).ToList();
                        return View(vftreport);

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ScanId,PatientName,ReferredDrName,ReFfacility,Date")] Vft vft)
        {
           

            if (VftExists(vft.ScanId))
            {
                try
                {
                    _context.Update(vft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VftExists(vft.ScanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.id = vft.ScanId;
                return View("EditSuccess");
            }
            return View(vft);
        }

        // GET: VFT/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Vfts == null)
            {
                return NotFound();
            }

            var vft = await _context.Vfts
                .FirstOrDefaultAsync(m => m.ScanId == id);
            if (vft == null)
            {
                return NotFound();
            }

            return View(vft);
        }

        // POST: VFT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Vfts == null)
            {
                return Problem("Entity set 'VFT_OCTContext.Vfts'  is null.");
            }
            var vft = await _context.Vfts.FindAsync(id);
            if (vft != null)
            {
                _context.Vfts.Remove(vft);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VftExists(string id)
        {
            return (_context.Vfts?.Any(e => e.ScanId == id)).GetValueOrDefault();
        }
    }
}
