using System.ComponentModel.DataAnnotations;

namespace DryveTrack_BackEnd.Models
{
    public class UserVehicle
    {
        [Key]
        public Guid UserVehicleId { get; set; }
        public Guid User { get; set; }
        public Guid Vehicle { get; set; }
        
    }
}
