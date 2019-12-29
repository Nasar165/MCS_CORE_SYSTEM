using System.ComponentModel.DataAnnotations;
using mcs.api.Models;
using mcs.api.Security.AuthTemplate.Interface;

namespace mcs.api.Security.AuthTemplate
{
    public sealed class UserAccount : DataExtension, IUserAccount
    {
        IEncrypter encrypter {get;}
        public UserAccount()
            => encrypter = new AesEncrypter(
                AppConfigHelper.Instance.GetValueFromAppConfig("AppSettings","SymmetricKey"));
        public int UserAccount_Id { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
        public string Username { get; set; }
        private string password {get;set;}
        [Required]
        public string Password { 
            get
            {
                return password;
            } 
            set 
            {   if(value != null)
                    password = encrypter.EncryptData(value);
            }
        }
    }
}