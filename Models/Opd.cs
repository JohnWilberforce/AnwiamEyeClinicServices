using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models;

public partial class Opd
{
   
    public int Id { get; set; }
    [Required]
    public string? PatientId { get; set; }
    [Required]
    public string? PatientName { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    public string? Contact { get; set; }
    [Required]
    public string? Services { get; set; }
    [Required]
    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
    public string? Status { get; set; } 
}
