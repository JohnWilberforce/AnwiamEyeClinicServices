using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnwiamEyeClinicServices.Models;

public partial class Consultation
{
    public int Id { get; set; }

    public string? PatientId { get; set; }

    public string? PatientName { get; set; }

    public string? VisualAcuity { get; set; }

    public string? ChiefComplaint { get; set; }

    public string? PatientHistory { get; set; }

    public string? FamilyHistory { get; set; }

    public string? Diagnosis { get; set; }

    public string? Medications { get; set; }

    public string? SpectacleRx { get; set; }

    [Required]
    public DateTime? Date { get; set; }
    public string? Eyelids { get;set; }
    public string? Conjunctiva { get; set; }
    public string? Cornea { get; set; }
    public string? Pupils { get; set; }
    public string? Media { get; set; }
    public string? Lens { get; set; }
    public string? OpticNerve { get; set; }
    public string? Fundus { get; set; }
    public string? Note { get; set; }

}
