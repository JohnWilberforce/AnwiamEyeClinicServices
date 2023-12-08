using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AnwiamEyeClinicServices.Models;

namespace AnwiamEyeClinicServices.Controllers
{
    [Route("[Controller]/[Action]")]
    public class OCTController : Controller
    {
        private readonly AnwiamServicesContext _context;
        //AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context c;
        public OCTController()
        {
            //c = new AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context();
            _context = new AnwiamServicesContext();
        }

        // GET: OCT
        public async Task<IActionResult> Index()
        {
              return _context.Octs != null ? 
                          View(await _context.Octs.ToListAsync()) :
                          Problem("Entity set 'VFT_OCTContext.Octs'  is null.");
        }
        [HttpGet]
        public ViewResult GetTotals()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Totals(DateTime stDate, DateTime eDate)
        {
            Total? total=new Total();
            total=_context.Totals.FromSqlInterpolated($"select * from ufn_totals({stDate}, {eDate})").FirstOrDefault();

            if (total != null)
            {
                ViewBag.VftCount = total.VftCount;
                ViewBag.OnhCount = total.OnhCount;
                ViewBag.MaculaCount = total.MaculaCount;
                ViewBag.PachymetryCount = total.PachymetryCount;
                ViewBag.VftTotalAmount = total.VftTotalAmount;
                ViewBag.TotalOfOnhMacPachyAmount = total.TotalOfOnhMacPachyAmount;
                ViewBag.month = stDate.ToString("MMMM").ToUpper();
                ViewBag.stDate = stDate.ToString("M-d-yyyy");
                ViewBag.eDate = eDate.ToString("M-d-yyyy");
                ViewBag.b = "between";
                ViewBag.a = "and";
                return View("GetTotals");
            }
            
            return View("GetTotals");




        }
        // GET: OCT/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Octs == null)
            {
                return NotFound();
            }

            var oct = await _context.Octs
                .FirstOrDefaultAsync(m => m.ScanId == id);
            if (oct == null)
            {
                return NotFound();
            }

            return View(oct);
        }

        // GET: OCT/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OCT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScanId,PatientName,ReferredDrName,ReFfacility,Onh,Macula,Pachymetry,Date")] Oct oct)
        {
            if (ModelState.IsValid)
            {
                if (_context.Octs.Find(oct.ScanId) != null)
                {
                    ViewBag.tryAgain = "Scan Id Already Used!!";
                    return View();
                }
                    _context.Add(oct);
                await _context.SaveChangesAsync();
                
                return View("OctRegistrySuccess");
            }
            return View(oct);
        }

        // GET: OCT/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var id1 = id;
            if (id.Contains("%2F"))
            {
                id1 = id.Replace("%2F", "/");
            }
            
            if (id1 == null || _context.Octs == null)
            {
                return NotFound();
            }

            var oct = await _context.Octs.FindAsync(id1);
            if (oct == null)
            {
                return NotFound();
            }
            return View(oct);
        }

        // POST: OCT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ScanId,PatientName,ReferredDrName,ReFfacility,Onh,Macula,Pachymetry,Date")] Oct oct)
        {
            var Octrecord = _context.Octs.Find(oct.ScanId);
            if (Octrecord == null)
            {
                return NotFound("Try Again");
            }
            try
                {
               
                Octrecord.PatientName= oct.PatientName;
                Octrecord.ReferredDrName= oct.ReferredDrName;
                Octrecord.ReFfacility= oct.ReFfacility;
                Octrecord.Onh= oct.Onh;
                Octrecord.Macula= oct.Macula;
                Octrecord.Pachymetry= oct.Pachymetry;
                Octrecord.Date= oct.Date;
                await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OctExists(oct.ScanId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewBag.id = oct.ScanId;
                return View("EditSuccess");
            
            //return View(oct);
        }
        [HttpPost]
        public ViewResult DailyRecords(DateTime today)
        {
            List<Oct> oct = null;

            oct = _context.Octs.Where(x => x.Date == today).ToList();
            ViewBag.today = today.ToString("dddd, dd MMMM yyyy");

            return View(oct);
        }

        [HttpPost]
        public ViewResult Search(DateTime stDate, DateTime eDate)
        {

            List<Oct> oct = new List<Oct>();
            try
            {
                oct = _context.Octs.Where(x => x.Date >= stDate.Date && x.Date <= eDate.Date).ToList();

              
            }
            catch (Exception ex)
            {
                oct = null;
            }

            return View(oct);
        }
        public ViewResult ViewReportOCT(DateTime stDate, DateTime eDate)
        {

            List<OCTreport> octreport = new List<OCTreport>();



            octreport = _context.OCTreports.FromSqlInterpolated($"select * from udf_GenerateOCTreport({stDate}, {eDate})").
                OrderByDescending(x=>x.Amount).ToList();
            return View(octreport);
        }
           

            private bool OctExists(string id)
        {
          return (_context.Octs?.Any(e => e.ScanId == id)).GetValueOrDefault();
        }
    }
}
