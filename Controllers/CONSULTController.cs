using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnwiamEyeClinicServices.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AnwiamEyeClinicServices.Controllers
{
    [Route("[Controller]/[Action]")]
    public class CONSULTController : Controller
    {
        private readonly AnwiamServicesContext _context;
        //private readonly AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context c;

        public CONSULTController()
        {
            _context = new AnwiamServicesContext();
            //c=new AspnetAnwiamEyeClinicServices53bc9b9d9d6a45d484292a2761773502Context();
        }

        // GET: CONSULT
        public IActionResult Index()
        {
            var formattedData = DateTime.Now.Date;
              return _context.Opds != null ? 
                          View(_context.Opds.Where(x=>x.Date==formattedData && x.Services.Contains("Consultation")).ToList()) :
                          Problem("Entity set 'AnwiamServicesContext.Consultations'  is null.");
        }
        [HttpPost]
        public ViewResult hxBtnDates(DateTime date1, DateTime date2)
        {
            List<Consultation> cons = null;
            try
            {
                cons = _context.Consultations.Where(x => x.Date >= date1 && x.Date <= date2).ToList();
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
        public ViewResult PxHistory(string patientId)
        {
            //var result = from c in _context.Consultations join p in _context.Opds on c.PatientId equals p.PatientId
            //            where c.Date == date && c.PatientId == p.PatientId
            //             select p;
            List<Consultation>? result = null;
            try
            {
                result = _context.Consultations.Where(x => x.PatientId == patientId).ToList();
                if (result != null)
                {
                    return View(result);
                }
                else { return View(null); }
            }
            catch(Exception ex) { result = null;
                return View(result);
            }
        }
        // GET: CONSULT/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultations == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // GET: CONSULT/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CONSULT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,PatientName,VisualAcuity,ChiefComplaint,PatientHistory,FamilyHistory,Diagnosis,Medications,SpectacleRx,Date")] Consultation consultation)
        {
            if (ModelState.IsValid)
            {
                if (ConsultationExists(consultation.Id))
                {
                    _context.Consultations.Update(consultation);
                    _context.SaveChanges();
                }
                else
                {
                    _context.Add(consultation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(consultation);
        }
        public IActionResult PharmacyDlyRecords()
        {
            var formattedData = DateTime.Now.Date;
            return _context.Pharmacys != null ?
                        View(_context.Pharmacys.Where(x => x.Date == formattedData).ToList()) :
                        Problem("Entity set 'AnwiamServicesContext.Consultations'  is null.");
        }

        public async Task<IActionResult> PharmacyStatusUpdate(int id)
        {
            using (var con = new AnwiamServicesContext()) {
                var rec = con.Pharmacys.Find(id);
                if (rec != null)
                {

                    rec.Status = "Dispensed";
                    con.Pharmacys.Update(rec);
                    await con.SaveChangesAsync();
                }
            }
            
            return RedirectToAction("PharmacyDlyRecords");

        }
        [HttpPost]
        public ViewResult pharmacyBtnDates(DateTime date1, DateTime date2)
        {
            List<Pharmacy> cons = null;
            try
            {
                cons = _context.Pharmacys.Where(x => x.Date >= date1 && x.Date <= date2).ToList();
                if (cons != null)
                {
                    return View(cons);
                }
                return View(cons);
            }
            catch (Exception ex)
            {
                cons = null;
                return View(cons);
            }
        }
        // GET: CONSULT/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Opds == null)
            {
                return NotFound();
            }

            var opdToConsultation = await _context.Opds.FindAsync(id);
            if (opdToConsultation == null)
            {
                return NotFound();
            }
            ViewBag.Id=opdToConsultation.PatientId;
            ViewBag.name = opdToConsultation.PatientName;
            return View();
        }

        // POST: CONSULT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,PatientName,VisualAcuity,ChiefComplaint,PatientHistory,FamilyHistory,Diagnosis,Medications,SpectacleRx,Date")] Consultation consultation)
        {
            var px = _context.Consultations.Where(x => x.PatientId == consultation.PatientId && x.Date == DateTime.Now.Date).FirstOrDefault();


            try
            {
                 if (px != null) {
                    ViewBag.alreadySeen = "Patient is already Examined";
                    return View();
                }
                else {
                    _context.Consultations.Add(consultation);
                    await _context.SaveChangesAsync();

                    
                        Pharmacy p = new Pharmacy();
                        p.PatientId = consultation.PatientId;
                        p.PatientName = consultation.PatientName;
                        p.Diagnosis = consultation.Diagnosis;
                        p.Medications = consultation.Medications;
                        p.Date = consultation.Date;

                        _context.Pharmacys.Add(p);
                        await _context.SaveChangesAsync();
                    }
                    Opd? pxRecord = null;
                    try {
                        
                            pxRecord = _context.Opds.Where(x => x.PatientId == consultation.PatientId).FirstOrDefault();
                            if (pxRecord != null)
                            {
                                pxRecord.Status = "Seen";
                                _context.Opds.Update(pxRecord);
                                _context.SaveChanges();
                            }
                       

                    }
                    catch (Exception ex) { pxRecord.Status = null;return View(); }
                    return View("success");
                
            }
            catch (Exception Ex)
            {
                return View(consultation);
            }
                return RedirectToAction(nameof(Index));
            
            return View(consultation);
        }

        // GET: CONSULT/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultations == null)
            {
                return NotFound();
            }

            var consultation = await _context.Consultations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultation == null)
            {
                return NotFound();
            }

            return View(consultation);
        }

        // POST: CONSULT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultations == null)
            {
                return Problem("Entity set 'AnwiamServicesContext.Consultations'  is null.");
            }
            var consultation = await _context.Consultations.FindAsync(id);
            if (consultation != null)
            {
                _context.Consultations.Remove(consultation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultationExists(int id)
        {
          return (_context.Consultations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
