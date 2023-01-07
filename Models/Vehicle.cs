namespace DryveTrack_BackEnd.Models
{
    public class Vehicle
    {
        public Guid VehicleId { get; set; }
        public string VIN { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Odometer { get; set; }
    }
}
