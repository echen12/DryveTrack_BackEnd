using System.ComponentModel.DataAnnotations;

namespace DryveTrack_BackEnd.Models
{
    public class Users
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
