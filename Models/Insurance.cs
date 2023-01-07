using System.ComponentModel.DataAnnotations;

namespace DryveTrack_BackEnd.Models
{
    public class Insurance
    {
        [Key]
        public Guid InsuranceId { get; set; }
        public string LicensePlate { get; set; }
        public string InsuranceExpiryDate { get; set; }
        public string InsuranceProvider { get; set; }
    }
}
