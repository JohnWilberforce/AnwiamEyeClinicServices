namespace AnwiamEyeClinicServices.Models
{
    public class OPDConsultStatus
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

        public string? Age { get; set; }
    }
}
