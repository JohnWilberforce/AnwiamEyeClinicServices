using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models
{
    public partial class Total
    {
        [Key]
        public int? VftCount { get; set; }
        public int? OnhCount { get; set; }
        public int? MaculaCount { get; set; }
        public int? PachymetryCount { get; set; }
        public decimal? VftTotalAmount { get; set; }
        public decimal? TotalOfOnhMacPachyAmount { get; set; }
    }
}
