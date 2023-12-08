using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models;

public partial class Purchase
{
    [Key]
    public int PurchaseId { get; set; }
    public int PatientId { get; set; }
    public string? PatientName {get; set; }

    public string? FrameType { get; set; }

    public decimal? FramePrice { get; set; }

    public string? LensType { get; set; }

    public decimal? LensPrice { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? AmountPaid { get; set; }

    public DateTime? Date { get; set; }

    public int? Quantity { get; set; }

}
