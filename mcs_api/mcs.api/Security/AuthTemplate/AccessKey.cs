using System.ComponentModel.DataAnnotations;
using mcs.api.Security.AuthTemplate.Interface;

namespace mcs.api.Security.AuthTemplate
{
    public sealed class AccessKey : DataExtension, IAccessKey
    {
        [Required]
        [StringLength(11, ErrorMessage = "The {0} value cannot exceed {1} characters. ")]
        public string TokenKey { get; set; }
        [Required]
        public string GroupKey { get; set; }
        public string Roles { get; set; }
    }
}