using Microsoft.Build.Framework;

namespace AnwiamEyeClinicServices.Models
{
    public class RevenueServices
    {
        public int Id { get; set; }
        [Required]
        public string? PatientName { get; set; }
        [Required]
        public string? Services { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime Date { get; set; }

    }
}
