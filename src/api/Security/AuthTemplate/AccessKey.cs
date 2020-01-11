using System.ComponentModel.DataAnnotations;
using api.Security.AuthTemplate.Interface;

namespace api.Security.AuthTemplate
{
    public sealed class AccessKey : DataExtension, IAccessKey
    {
        [Required]
        [StringLength(11, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string TokenKey { get; set; }
        [Required]
        public string GroupKey { get; set; }
    }
}