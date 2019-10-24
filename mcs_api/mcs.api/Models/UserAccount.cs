using System.ComponentModel.DataAnnotations;

namespace mcs.api.Models
{
    public class UserAccount
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}