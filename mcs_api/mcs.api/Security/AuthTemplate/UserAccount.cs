using System.ComponentModel.DataAnnotations;
using mcs.api.Security.AuthTemplate.Interface;

namespace mcs.api.Security.AuthTemplate
{
    public class UserAccount : IUserAccount
    {
        [Required]
        [StringLength(11, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        private string[] Role { get; set; }

        public void SetRoles(params string[] roles)
            => Role = roles;

        public string[] GetRoles()
            => Role;

    }
}