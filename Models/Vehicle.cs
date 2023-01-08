using System.ComponentModel.DataAnnotations;

namespace DryveTrack_BackEnd.Models
{
    public class Vehicle
    {
        [Key]
        public Guid VehicleId { get; set; }
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public string VehicleType { get; set; }
        public string Color { get; set; }
        public Guid Odometer { get; set; }
    }
}
