using System.ComponentModel.DataAnnotations;
using mcs.api.Security.AuthTemplate.Interface;
using mcs.Components.Security;

namespace mcs.api.Security.AuthTemplate
{
    public sealed class UserAccount : DataExtension, IUserAccount
    {
        [Required]
        [StringLength(11, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }      
        public void EncryptPassword()
            => Password = AesEncrypter._instance.EncryptData(Password);
    }
}