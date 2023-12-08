using System;
using System.Collections.Generic;

namespace AnwiamEyeClinicServices.Models;

public partial class Oct
{
    public string ScanId { get; set; } = null!;

    public string? PatientName { get; set; }

    public string? ReferredDrName { get; set; }

    public string? ReFfacility { get; set; }

    public string? Onh { get; set; }

    public string? Macula { get; set; }

    public string? Pachymetry { get; set; }

    public DateTime? Date { get; set; }
}
