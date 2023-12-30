using System;
using System.Collections.Generic;

namespace AnwiamEyeClinicServices.Models;

public partial class FrameStock
{
    public int Id { get; set; }

    public string? FrameType { get; set; }

    public int? Quantity { get; set; }

    public DateTime? Date { get; set; }
}
