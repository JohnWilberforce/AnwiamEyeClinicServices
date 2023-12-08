using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnwiamEyeClinicServices.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;


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
            return View();
        }

        // GET: OPDController/Details/5
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
            try
            {
                opd = servicesContext.Opds.Where(x => x.Date >= stDate && x.Date <= eDate).ToList();
                if (opd != null)
                {
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
             public ActionResult OnlyConsultationOpd(DateTime stDate, DateTime eDate)
        {
            List<Opd> opd = null;
            try
            {
                opd = servicesContext.Opds.Where(x => x.Date >= stDate && x.Date <= eDate).Where(x=>x.Services.Contains("Consultation")).ToList();
                if (opd != null)
                {
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
            try
            {
                opd.PatientId = PatientId;
                opd.PatientName = PatientName;
                opd.Address = Address;
                opd.Contact = Contact;
                opd.Services = string.Join(", ", Services);
                opd.Amount = Convert.ToDecimal(Amount);
                opd.Date = Date;
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
        public ActionResult DailyRecords(DateTime date)
        {
            List<Opd> res = null;
            try
            {
                res = servicesContext.Opds.Where(x => x.Date == date).ToList();
                //ViewBag.records=res;

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
        private bool OctExists(int id)
        {
            return (servicesContext.Opds?.Any(e => e.Id == id)).GetValueOrDefault();
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
    }
}
