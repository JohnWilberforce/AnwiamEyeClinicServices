namespace AnwiamEyeClinicServices.Models
{
    public class RetinalImage
    {
      public  int Id { get; set; }
   public string? scanId { get;set; }
   public string? patientName { get; set; }
  public  string? referredDrName { get; set; }
    public string? reFfacility { get; set; }
  public  DateTime? date { get; set; }

    }
}
