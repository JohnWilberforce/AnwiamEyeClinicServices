using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnwiamEyeClinicServices.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

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
        public ActionResult Index()
        {
            var formattedData = DateTime.Now.Date;
            return servicesContext.Opds != null ?
                        View(servicesContext.Opds.Where(x => x.Date == formattedData && x.Services.Contains("Consultation")).
                        ToList().OrderByDescending(x=>x.Id)) :
                        Problem("Entity set 'AnwiamServicesContext.Consultations'  is null.");
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
        public ActionResult AccountOpd2(DateTime stDate, DateTime eDate)
        {
            List<Opd> opd = null;
            ViewBag.stDate = stDate.ToString("MMM-dd").ToUpper();
            ViewBag.eDate = eDate.ToString("MMM-dd").ToUpper();
            try
            {
                opd = servicesContext.Opds.Where(x => x.Date >= stDate && x.Date <= eDate).ToList();
                if (opd != null)
                {
                    ViewBag.counter=opd.Count();
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
        public ActionResult FrameSales(DateTime stDate, DateTime eDate)
        {
            List<Opd>? opd = null;
            List<Opd>? opd2 = null;
            ViewBag.stDate = stDate.ToString("MMM-dd").ToUpper();
            ViewBag.eDate = eDate.ToString("MMM-dd").ToUpper();
            try
            {
                opd = servicesContext.Opds.Where(x => x.Date >= stDate && x.Date <= eDate).ToList();
                if (opd != null) { 
                opd2= opd.Where(x => x.Services.ToLower() == "frame").ToList();
                ViewBag.OPDFrameSales = opd.Where(x => x.Services.ToLower() == "frame").Sum(x => x.Amount);
              
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
             public ActionResult OnlyConsultationOpd(DateTime stDate, DateTime eDate)
        {
            List<Opd> opd = null;
            ViewBag.Amount = null;
            ViewBag.stDate=stDate.ToString("MMM-dd").ToUpper();
            ViewBag.eDate=eDate.ToString("MMM-dd").ToUpper();
            try
            {
                opd = servicesContext.Opds.Where(x => x.Date >= stDate && x.Date <= eDate).Where(x=>x.Services.Contains("Consultation")).ToList();
                if (opd != null)
                {
                    var sumAmount=opd.Where(x=>x.Amount>0).Sum(x=>x.Amount);
                    ViewBag.counter=opd.Count();
                    ViewBag.Amount=sumAmount;
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
        public ActionResult Create(string PatientId, string PatientName, string Address, string Contact,
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
                opd.Status = "";
                servicesContext.Opds.Add(opd);
                servicesContext.SaveChanges();

                var curr = servicesContext.Opds.OrderByDescending(x=>x.Id).Select(y => y.Id ).FirstOrDefault();
                
                ops.Id = curr;
                ops.PatientId = PatientId;
                ops.PatientName = PatientName;

                ops.Address = Address;
                ops.Contact = Contact;
                ops.Services = string.Join(", ", Services);
                ops.Amount = Convert.ToDecimal(Amount);
                ops.Date = Date;
                opd.Status = "";
                servicesContext.OPDConsultStatuses.Add(ops);
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
        public ActionResult CreateRev(string PatientName,
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
                if (opd.Services.IsNullOrEmpty()||opd.PatientName==null)
                {
                    throw new Exception();
                }
                servicesContext.RevenueServicies.Add(opd);
                servicesContext.SaveChanges();
                ViewBag.success = "Patient is Added Successfully";
                return View("Create");
            }
            catch(Exception Ex)
            {
                ViewBag.success = "Try Again";
                return View("Create");
            }
        }

        [HttpPost]
        public ActionResult DailyRecordsOPD(DateTime date)
        {
            List<Opd> res = null;
            try
            {
                res = servicesContext.Opds.Where(x => x.Date == date && x.Services.Contains("Consultation")).ToList();
                //ViewBag.records=res;

                return View(res);
            }
            catch (Exception Ex)
            {
                return View("Create");
            }
        }
       
        [HttpPost]
        public ActionResult DailyRecordsREV(DateTime date)
        {
            List<RevenueServices> res = null;
            try
            {
                res=servicesContext.RevenueServicies.FromSqlInterpolated($"select * from udf_GetDailyRecordsRev({date})").ToList();
                return View(res);
            }
            catch (Exception Ex)
            {
                return View("Create");
            }
        }

        [HttpGet]
        public ActionResult DailyRecordsREV()
        {
            List<RevenueServices> res = null;
            try
            {
                DateTime date = DateTime.Now.Date;
                res = servicesContext.RevenueServicies.FromSqlInterpolated($"select * from udf_GetDailyRecordsRev({date})").ToList();
                return View(res);
            }
            catch (Exception Ex)
            {
                return View("Create");
            }
        }

        // GET: OPDController/Edit/5
        public ActionResult Edit(int? Id)
        {

            if (Id == null || servicesContext.Opds == null)
            {
                return NotFound();
            }

            var opd = servicesContext.Opds.Find(Id);
            if (opd == null)
            {
                return NotFound();
            }
            return View(opd);
        }
        public ActionResult EditRev(int? Id)
        {

            if (Id == null || servicesContext.RevenueServicies == null)
            {
                return NotFound();
            }

            var rev = servicesContext.RevenueServicies.Find(Id);
            if (rev == null)
            {
                return NotFound();
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
        public ActionResult Edit(int Id, [Bind("Id,PatientId,PatientName,Address,Contact,Services,Amount,Date,Status")] Opd opd)
        {

            if (ModelState.IsValid)
            {
                if (Id != opd.Id)
                {
                    return NotFound();
                }
                try
                {
                    servicesContext.Update(opd);
                    servicesContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OctExists(opd.Id))
                    {
                        return NotFound();
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
        public ActionResult EditRev(int Id, [Bind("Id,PatientName,Services,Amount,Status,Date")] RevenueServices rev)
        {

            if (ModelState.IsValid)
            {
                if (Id != rev.Id)
                {
                    return NotFound();
                }
                try
                {
                    servicesContext.Update(rev);
                    servicesContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OctExistsRev(rev.Id))
                    {
                        return NotFound();
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
        public ActionResult Delete(int id)
        {

            if (id == null || servicesContext.Opds == null)
            {
                return NotFound();
            }

            var opd = servicesContext.Opds.Find(id);
            if (opd == null)
            {
                return NotFound();
            }
            return View(opd);

        }
        public ActionResult DeleteRev(int? id)
        {

            if (id == null || servicesContext.RevenueServicies == null)
            {
                return NotFound();
            }

            var rev = servicesContext.RevenueServicies.Find(id);
            if (rev == null)
            {
                return NotFound();
            }
            return View(rev);

        }
        // POST: OPDController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id, IFormCollection collection)
        {
            {
                try
                {
                    if (servicesContext.Opds == null)
                    {
                        return Problem("Entity set 'VFT_OCTContext.Octs'  is null.");
                    }
                    var opd = servicesContext.Opds.Find(Id);
                    if (opd != null)
                    {
                        servicesContext.Opds.Remove(opd);
                    }

                    servicesContext.SaveChangesAsync();
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
        public ActionResult DeleteRev(int Id, IFormCollection collection)
        {
            {
                try
                {
                    if (servicesContext.RevenueServicies == null)
                    {
                        return View();
                    }
                    var rev = servicesContext.RevenueServicies.Find(Id);
                    if (rev != null)
                    {
                        servicesContext.RevenueServicies.Remove(rev);
                    }

                    servicesContext.SaveChangesAsync();
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
