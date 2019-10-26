using System.ComponentModel.DataAnnotations;
using mcs.api.Security.Interface;

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