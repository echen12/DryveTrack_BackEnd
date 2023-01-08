namespace DryveTrack_BackEnd.Models
{
    public class Odometer
    {
        public Guid OdometerId { get; set; }
        public string VIN { get; set; }
        public string LastUpdated { get; set; }
        public string lastMileage { get; set; }
        public string OilChangeStartInterval { get; set; }
    }
}
