using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models;

public partial class Opd
{
   
    public int Id { get; set; }

    public string? PatientId { get; set; }

    public string? PatientName { get; set; }

    public string? Address { get; set; }

    public string? Contact { get; set; }

    public string? Services { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
    public string? Status { get; set; } 
}
