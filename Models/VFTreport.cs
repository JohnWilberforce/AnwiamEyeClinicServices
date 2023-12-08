using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models
{
    public class VFTreport
    {
        //[Key]
        //public string? ReferredDoctor { get; set;}
        //public string? ReferredFacility { get; set;}
        //public int NumberOfReferrals { get; set; }
        //public decimal Amount { get; set; }

        [Key]
        public string? referredDrName { get; set; }
        //public string? reFfacility { get; set; }
        public int TotalVFTReferrals { get; set; }
        public decimal Amount { get; set; }
    }
}
