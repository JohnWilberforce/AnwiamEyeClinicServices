using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AnwiamEyeClinicServices.Models;

namespace AnwiamEyeClinicServices.Controllers
{
    [Route("[Controller]/[Action]")]
    public class PurchasesController : Controller
    {
        private readonly AnwiamServicesContext _context;
        //private readonly AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context c;

        public PurchasesController()
        {
           //c = new AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context();
          _context = new AnwiamServicesContext();
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            var vFT_OCTContext = _context.Purchases.Include(p => p.PatientId);
            return View(await vFT_OCTContext.ToListAsync());
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.PatientId)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // GET: Purchases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Telephone, SpectacleRx,FrameType,FramePrice,LensType,LensPrice,TotalPrice,AmountPaid, Quantity,Date")] PurchaseRefractionPx purchase)
        {
          
            var pId=0;
            int rowsAffected=0;
            try
            {
                        SqlParameter prmResult = new SqlParameter("@Result", System.Data.SqlDbType.Int);
                        prmResult.Direction = System.Data.ParameterDirection.Output;
                        SqlParameter prmFrType = new SqlParameter("@FrameType", purchase.FrameType);
                        SqlParameter prmFrPrice = new SqlParameter("@FramePrice", purchase.FramePrice);
                        SqlParameter prmLensTy = new SqlParameter("@LensType", purchase.LensType);
                        SqlParameter prmLensPr = new SqlParameter("@LensPrice", purchase.LensPrice);
                        SqlParameter prmTotalP = new SqlParameter("@TotalPrice", purchase.TotalPrice);
                        SqlParameter prmAmntPaid = new SqlParameter("@AmountPaid", purchase.AmountPaid);
                        SqlParameter prmQty = new SqlParameter("@Quantity", purchase.Quantity);
                        SqlParameter prmDate = new SqlParameter("@date", purchase.Date);
                        SqlParameter prmName=new SqlParameter("@PName", purchase.Name);
                        SqlParameter prmTelephone=new SqlParameter("@Telephone",purchase.Telephone);
                        SqlParameter prmSpecRx = new SqlParameter("@SpectacleRx", purchase.SpectacleRx);

                        _context.Database.ExecuteSqlRaw(" Exec @Result=usp_purchaseUpdateStock @PName,@Telephone,@SpectacleRx, @FrameType, @FramePrice,@date, @LensType,@LensPrice, @TotalPrice, @AmountPaid, @Quantity", 
                        prmResult, prmName, prmTelephone, prmSpecRx,prmFrType, prmFrPrice, prmLensTy, prmLensPr, 
                        prmTotalP, prmAmntPaid, prmQty, prmDate);
                rowsAffected = Convert.ToInt32(prmResult.Value);
                    }
                
                catch (Exception ex)
                {
                    pId = 0;
                    rowsAffected = 0;
                   
                }
   
            if (rowsAffected == 1)
            {
                return View("purchase");
            }
            else
            {
                ViewBag.error = "Try Again !!";
                    return View(purchase); }
        }

        [HttpPost]
        public async Task<IActionResult> GetRefractionsBtnDates(DateTime date1, DateTime date2)
        {
            List<PurchaseRefractionPx> res= new List<PurchaseRefractionPx>();
            try
            {
                res= await _context.PurchaseRefractionPxes.FromSqlInterpolated($"select * from ufn_FetchRefractionsBtnDates({date1},{date2})").ToListAsync();
                //res= await _context.PurchaseRefractionPxes.Where(x => x.Date >= date1 && x.Date <= date2).OrderByDescending(x => x.Date).ToListAsync(); ;

                ViewBag.date1 = date1.ToString("MMMM dd, yyyy");

                ViewBag.date2 = date2.ToString("MMMM dd, yyyy");
            }
            catch(Exception ex)
            {
                res = null;
            }
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> SearchByName(string patientName)
        {
           var px=await _context.RefractionPxes.Where(x => x.Name.Contains(patientName)).FirstOrDefaultAsync();
            if (px != null)
            {
                var obj = await _context.Purchases.Where(x => x.PatientId == px.Id).OrderByDescending(x=>x.PurchaseId).ToListAsync();
                if (obj == null)
                {
                    return NotFound();
                }
                ViewBag.PatientName = px.Name;
                ViewBag.PatientTel = px.Telephone;
                ViewBag.PatientSpecs = px.SpectacleRx;
                return View(obj);
            }
            else { return NotFound(); }
        }
        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

          
                var purchase = await _context.Purchases.Where(x=>x.PurchaseId== id).FirstOrDefaultAsync();
                if (purchase == null)
                {
                    return NotFound();
                }
                ViewData["PatientId"] = new SelectList(_context.RefractionPxes, "Id", "Id", purchase.PatientId);
                return View(purchase);
            
        }
        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int PurchaseId,string PatientName, int PatientId, string FrameType, decimal FramePrice, string LensType, decimal LensPrice,decimal TotalPrice,decimal AmountPaid,DateTime date)
        {
            var p= _context.Purchases.Where(x=>x.PurchaseId==PurchaseId).FirstOrDefault();
            if (p != null)
            {
                
                p.PatientId = PatientId;
                p.FrameType = FrameType;
                p.FramePrice = FramePrice;
                p.LensType = LensType;
                p.LensPrice = LensPrice;
                p.TotalPrice = TotalPrice;
                p.AmountPaid = AmountPaid;
                p.Date = date;
              
            }
            await _context.SaveChangesAsync();
            try
                {
                   
                    
                    var obj = _context.RefractionPxes.Where(x => x.Id == p.PatientId).FirstOrDefault();
                    if (obj != null)
                    {
                        ViewBag.PatientName = obj.Name;
                        ViewBag.PatientTel = obj.Telephone;
                        ViewBag.PatientSpecs = obj.SpectacleRx;
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(p.PurchaseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return View("refractionBalPaymentSuccess");
            
            ViewData["PatientId"] = new SelectList(_context.RefractionPxes, "Id", "Id", p.PatientId);
            ViewBag.error = "Ty Again!";
            return View(p);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchases == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchases
                .Include(p => p.PatientId)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchases == null)
            {
                return Problem("Entity set 'VFT_OCTContext.Purchases'  is null.");
            }
            var purchase = await _context.Purchases.FindAsync(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
          return (_context.Purchases?.Any(e => e.PurchaseId == id)).GetValueOrDefault();
        }
    }
}
