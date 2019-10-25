using System.ComponentModel.DataAnnotations;
using mcs.api.Models.Interface;

namespace mcs.api.Models
{
    public class UserAccount : IUserAccount
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}