using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models;

public partial class RefractionPx
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? SpectacleRx { get; set; }
}
