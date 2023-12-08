using System;
using System.Collections.Generic;

namespace AnwiamEyeClinicServices.Models;

public partial class FrameStock
{
    public int Id { get; set; }

    public string? FrameTypeByColor { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }

    public DateTime? Date { get; set; }
}
