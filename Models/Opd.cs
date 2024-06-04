using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models;

public partial class Opd
{

    public int Id { get; set; }
    [Required]
    [RegularExpression(@"^AEC\d+/24$", ErrorMessage = $"PatientId must begin with AEC and end with /24")]
    public string? PatientId { get; set; }

    public string? PatientName { get; set; }

    public string? Address { get; set; }

    public string? Contact { get; set; }
    [Required]
    public string? Services { get; set; }
    [Required]
    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
    public string? Status { get; set; }
    public string? Age { get; set; }
}
