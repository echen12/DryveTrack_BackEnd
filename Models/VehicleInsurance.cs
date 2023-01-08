namespace DryveTrack_BackEnd.Models
{
    public class VehicleInsurance
    {
        public Guid VehicleInsuranceId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid InsuranceId { get; set; }
    }
}
