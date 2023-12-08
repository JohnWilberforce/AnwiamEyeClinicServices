using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnwiamEyeClinicServices.Models
{
    public class PurchaseRefractionPx
    {
        
        public string Name { get; set; } = null!;
        [Required]
        public string? Telephone { get; set; }
        [Required]
        public string? SpectacleRx { get; set; }
        [Required]
        public string? FrameType { get; set; }
        [Required]
        public decimal? FramePrice { get; set; }
        [Required]
        public string? LensType { get; set; }
        [Required]
        public decimal? LensPrice { get; set; }
        [Required]
        public decimal? TotalPrice { get; set; }
        [Required]
        public decimal? AmountPaid { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public int Quantity { get; set; }

    }
}
