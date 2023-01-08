using System.ComponentModel.DataAnnotations;

namespace DryveTrack_BackEnd.Models
{
    public class VehicleOdometer
    {
        [Key]
        public Guid VehicleOdometerId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid OdometerId { get; set; }
    }
}
