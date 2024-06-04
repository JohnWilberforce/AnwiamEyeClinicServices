using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnwiamEyeClinicServices.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AnwiamEyeClinicServices.Controllers
{
    [Route("[Controller]/[Action]")]
    public class OPDController : Controller
    {
        //AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context c;
        AnwiamServicesContext servicesContext;
        public OPDController()
        {
            //c = new AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context();
            servicesContext = new AnwiamServicesContext();
        }
        // GET: OPDController
        public async Task<ActionResult> Index()
        {
            var formattedData = DateTime.Now.Date;
            return servicesContext.Opds != null ?
                        View(await servicesContext.Opds.Where(x => x.Date == formattedData && x.Services.Contains("Consultation")).OrderByDescending(x => x.Id).
                        ToListAsync()) :
                        Problem("Entity set 'AnwiamServicesContext.Opds'  is null.");
        }
        public async Task<ViewResult> GenerateOpdNumber()
        {
            ViewBag.OpdNumber = null;
            var number = await servicesContext.Opds.OrderByDescending(y => y.Id).Select(x => x.Id).FirstOrDefaultAsync();
            var seqNumber = $"AEC{number + 1}/24";
            ViewBag.OpdNumber = seqNumber;
            return View("Create");
        }
        // GET: OPDController/Details/5
        public ViewResult OpdHome()
        {
            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OPDController/Create
        public ActionResult Create()
        {
            ViewBag.records = new List<Opd>() { };
            ViewBag.success = "";
            return View();
        }

        public ActionResult AccountOpd()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AccountOpd2(DateTime stDate, DateTime eDate)
        {
            List<Opd> opd = null;
            ViewBag.stDate = stDate.ToString("MMM-dd").ToUpper();
            ViewBag.eDate = eDate.ToString("MMM-dd").ToUpper();
            try
            {
                opd = await servicesContext.Opds.Where(x => x.Date >= stDate && x.Date <= eDate).ToListAsync();
                if (opd != null)
                {
                    ViewBag.counter = opd.Count();
                    return View(opd);
                }
                return View(opd);
            }
            catch (Exception ex)
            {
                opd = null;
                return View(opd);
            }
        }
        [HttpPost]
        public async Task<ActionResult> FrameSales(DateTime stDate, DateTime eDate)
        {
            List<RevenueServices>? opd = null;
            List<RevenueServices>? opd2 = null;
            ViewBag.stDate = stDate.ToString("MMM-dd").ToUpper();
            ViewBag.eDate = eDate.ToString("MMM-dd").ToUpper();
            try
            {
                opd = await servicesContext.RevenueServicies.Where(x => x.Date >= stDate && x.Date <= eDate).ToListAsync();
                if (opd != null)
                {
                    opd2 = opd.Where(x => x.Services.ToLower() == "frame" && x.Status.ToLower() =="paid").ToList();
                    ViewBag.OPDFrameSales = opd.Where(x => x.Services.ToLower() == "frame" && x.Status.ToLower() == "paid").Sum(x => x.Amount);

                    ViewBag.counter = opd2.Count();
                    return View(opd2);
                }
                return View(opd2);
            }
            catch (Exception ex)
            {
                opd2 = null;
                return View(opd2);
            }
        }
        [HttpPost]
        public async Task<ActionResult> OnlyConsultationOpd(DateTime stDate, DateTime eDate)
        {
            List<Opd> opd = null;
            ViewBag.Amount = null;
            ViewBag.stDate = stDate.ToString("MMM-dd").ToUpper();
            ViewBag.eDate = eDate.ToString("MMM-dd").ToUpper();
            try
            {
                opd = await servicesContext.Opds.Where(x => x.Date >= stDate && x.Date <= eDate).Where(x => x.Services.Contains("Consultation")
                && x.Status.ToLower() == "paid").ToListAsync();
                if (opd != null)
                {
                    var sumAmount = opd.Where(x => x.Amount > 0).Sum(x => x.Amount);
                    ViewBag.counter = opd.Count();
                    ViewBag.Amount = sumAmount;
                    return View(opd);
                }
                return View(opd);
            }
            catch (Exception ex)
            {
                opd = null;
                return View(opd);
            }
        }
        // POST: OPDController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string PatientId, string PatientName, string Address, string Contact,
            string[] Services, decimal Amount, DateTime Date, string Age)
        {
            ViewBag.records = new List<Opd>() { };
            Opd opd = new Opd();
            OPDConsultStatus ops = new OPDConsultStatus();
            using (var transaction = servicesContext.Database.BeginTransaction())
            {
                try
                {
                    opd.PatientId = PatientId;
                    opd.PatientName = PatientName;

                    opd.Address = Address;
                    opd.Contact = Contact;
                    opd.Services = string.Join(", ", Services);
                    opd.Amount = Convert.ToDecimal(Amount);
                    opd.Date = Date;
                    opd.Status = "";
                    opd.Age = Age;
                    await servicesContext.Opds.AddAsync(opd);
                    await servicesContext.SaveChangesAsync();

                    var curr = await servicesContext.Opds.Where(x => x.PatientId == PatientId).OrderByDescending(x => x.Id).Select(y => y.Id).FirstOrDefaultAsync();

                    ops.Id = curr;
                    ops.PatientId = PatientId;
                    ops.PatientName = PatientName;

                    ops.Address = Address;
                    ops.Contact = Contact;
                    ops.Services = string.Join(", ", Services);
                    ops.Amount = Convert.ToDecimal(Amount);
                    ops.Date = Date;
                    ops.Status = "";
                    ops.Age = Age;
                    await servicesContext.OPDConsultStatuses.AddAsync(ops);
                    await servicesContext.SaveChangesAsync();

                    ViewBag.success = "Patient is Added Successfully";
                    transaction.Commit();

                    return View();

                }

                catch
                {
                    transaction.Rollback();
                    ViewBag.success = "Try Again";
                    return View();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAdmin(string PatientId, string PatientName, string Address, string Contact,
            string[] Services, decimal Amount, DateTime Date)
        {
            ViewBag.records = new List<Opd>() { };
            Opd opd = new Opd();
            OPDConsultStatus ops = new OPDConsultStatus();
            try
            {
                opd.PatientId = PatientId;
                opd.PatientName = PatientName;

                opd.Address = Address;
                opd.Contact = Contact;
                opd.Services = string.Join(", ", Services);
                opd.Amount = Convert.ToDecimal(Amount);
                opd.Date = Date;
                if (opd.PatientId == null || opd.PatientName == null || opd.Address == null || opd.Contact == null || opd.Services == null ||
                    opd.Amount == Convert.ToDecimal(string.Empty))
                {
                    throw new Exception();
                }
                servicesContext.Opds.Add(opd);
                servicesContext.SaveChanges();

                ViewBag.success = "Patient is Added Successfully";
                return View();

                //return View("success");
            }
            catch
            {
                ViewBag.success = "Try Again";
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateRev(string PatientName,
            string[] Services, decimal Amount, DateTime Date)
        {
            ViewBag.records = new List<Opd>() { };
            RevenueServices opd = new RevenueServices();
            try
            {

                opd.PatientName = PatientName;
                opd.Services = string.Join(", ", Services);
                opd.Amount = Convert.ToDecimal(Amount);
                opd.Date = Date;
                opd.Status = "Paid";
                if (opd.Services.IsNullOrEmpty() || opd.PatientName == null)
                {
                    throw new Exception();
                }
                await servicesContext.RevenueServicies.AddAsync(opd);
                await servicesContext.SaveChangesAsync();
                ViewBag.success = "Patient is Added Successfully";
                return View("Create");
            }
            catch (Exception Ex)
            {
                ViewBag.success = "Try Again";
                return View("Create");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DailyRecordsOPD(DateTime date1, DateTime date2)
        {
            List<Opd> res = null;
            try
            {
                res = await servicesContext.Opds.Where(x => (x.Date >= date1 && x.Date <= date2) && x.Services.Contains("Consultation")).ToListAsync();
                //ViewBag.records=res;

                return View(res);
            }
            catch (Exception Ex)
            {
                return View("Create");
            }

        }

        [HttpPost]
        public async Task<ActionResult> DailyRecordsREV(DateTime date)
        {
            List<RevenueServices> res = null;
            try
            {
                res = await servicesContext.RevenueServicies.FromSqlInterpolated($"select * from udf_GetDailyRecordsRev({date})").ToListAsync();
                return View(res);
            }
            catch (Exception Ex)
            {
                return View("Create");
            }
        }

        [HttpGet]
        public async Task<ActionResult> DailyRecordsREV()
        {
            List<RevenueServices> res = null;
            try
            {
                DateTime date = DateTime.Now.Date;
                res = await servicesContext.RevenueServicies.FromSqlInterpolated($"select * from udf_GetDailyRecordsRev({date})").ToListAsync();
                return View(res);
            }
            catch (Exception Ex)
            {
                return View("Create");
            }
        }

        // GET: OPDController/Edit/5
        public async Task<ActionResult> Edit(int? Id)
        {

            if (Id == null || servicesContext.Opds == null)
            {
                return NotFound("Not Found, Please Go back");
            }

            var opd = await servicesContext.Opds.FindAsync(Id);
            if (opd == null)
            {
                return NotFound("Not Found, Please Go back");
            }
            return View(opd);
        }
        public async Task<ActionResult> EditRev(int? Id)
        {

            if (Id == null || servicesContext.RevenueServicies == null)
            {
                return NotFound("Not Found, Please Go back");
            }

            var rev = await servicesContext.RevenueServicies.FindAsync(Id);
            if (rev == null)
            {
                return NotFound("Not Found, Please Go back");
            }
            return View(rev);
        }
        private bool OctExists(int id)
        {
            return (servicesContext.Opds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool OctExistsRev(int id)
        {
            return (servicesContext.RevenueServicies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        // POST: OPDController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int Id, [Bind("Id,PatientId,PatientName,Address,Contact,Services,Amount,Date,Status")] Opd opd)
        {

            if (ModelState.IsValid)
            {
                if (Id != opd.Id)
                {
                    return NotFound("Not Found, Please Go back");
                }
                try
                {
                    servicesContext.Update(opd);
                    await servicesContext.SaveChangesAsync();
                    var opdStatus = await servicesContext.OPDConsultStatuses.Where(X => X.Id == Id).FirstOrDefaultAsync();
                    if (opdStatus != null)
                    {
                        opdStatus.PatientId = opd.PatientId;
                        opdStatus.PatientName = opd.PatientName;
                        opdStatus.Services = opd.Services;
                        opdStatus.Address = opd.Address;
                        opdStatus.Contact = opd.Contact;
                        opdStatus.Amount = opd.Amount;
                        await servicesContext.SaveChangesAsync();

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OctExists(opd.Id))
                    {
                        return NotFound("Not Found, Please Go back");
                    }
                    else
                    {
                        throw;
                    }
                }

                return View("success");
            }
            return View(opd);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditRev(int Id, [Bind("Id,PatientName,Services,Amount,Status,Date")] RevenueServices rev)
        {

            if (ModelState.IsValid)
            {
                if (Id != rev.Id)
                {
                    return NotFound("Not Found, Please Go back");
                }
                try
                {
                    servicesContext.Update(rev);
                    await servicesContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OctExistsRev(rev.Id))
                    {
                        return NotFound("Not Found, Please Go back");
                    }
                    else
                    {
                        throw;
                    }
                }

                return View("success");
            }
            return View(rev);
        }
        public async Task<IActionResult> StatusOPD(int id)
        {
            using (var con = new AnwiamServicesContext())
            {
                var rec = con.Opds.Find(id);
                if (rec != null)
                {

                    rec.Status = "Paid";
                    con.Opds.Update(rec);
                    await con.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");

        }
        // GET: OPDController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {

            if (id == null || servicesContext.Opds == null)
            {
                return NotFound("Not Found, Please Go back");
            }

            var opd = await servicesContext.Opds.FindAsync(id);
            if (opd == null)
            {
                return NotFound("Not Found, Please Go back");
            }
            return View(opd);

        }
        public async Task<ActionResult> DeleteRev(int? id)
        {

            if (id == null || servicesContext.RevenueServicies == null)
            {
                return NotFound("Not Found, Please Go back");
            }

            var rev = await servicesContext.RevenueServicies.FindAsync(id);
            if (rev == null)
            {
                return NotFound("Not Found, Please Go back");
            }
            return View(rev);

        }
        // POST: OPDController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int Id, IFormCollection collection)
        {
            {
                try
                {
                    if (servicesContext.Opds == null)
                    {
                        return Problem("Entity set 'AnwiamServicesContext.Opds'  is null.");
                    }
                    var opd = await servicesContext.Opds.FindAsync(Id);
                    if (opd != null)
                    {
                        servicesContext.Opds.Remove(opd);
                    }
                    var opdStatus = await servicesContext.OPDConsultStatuses.FindAsync(Id);
                    if (opdStatus != null)
                    {
                        servicesContext.OPDConsultStatuses.Remove(opdStatus);
                    }

                    await servicesContext.SaveChangesAsync();
                    return View("successDelete");

                }
                catch
                {
                    return View();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRev(int Id, IFormCollection collection)
        {
            {
                try
                {
                    if (servicesContext.RevenueServicies == null)
                    {
                        return View();
                    }
                    var rev = await servicesContext.RevenueServicies.FindAsync(Id);
                    if (rev != null)
                    {
                        servicesContext.RevenueServicies.Remove(rev);
                    }

                    await servicesContext.SaveChangesAsync();
                    return View("successDelete");

                }
                catch
                {
                    return View();
                }
            }
        }
    }
}
