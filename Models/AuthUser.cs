using System.ComponentModel.DataAnnotations;

namespace DryveTrack_BackEnd.Models
{
    public class AuthUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
