using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models
{
    public class ImageReport
    {
        [Key]
        public string? referredDrName { get; set; }
        //public string? reFfacility { get; set; }
        public int TotalRetinalImagesReferrals { get; set; }
        public decimal Amount { get; set; }
    }
}
