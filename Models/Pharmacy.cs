using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models
{
    public class Pharmacy
    {
        [Key]
        public int Id { get; set; }

        public string? PatientId { get; set; }

        public string? PatientName { get; set; }
        public string? Diagnosis { get; set; }

        public string? Medications { get; set; }
        public DateTime? Date { get; set; }
        public string? Status { get;set; }
    }
}
