using System.ComponentModel.DataAnnotations;
using mcs.api.Security.AuthTemplate.Interface;

namespace mcs.api.Security.AuthTemplate
{
    public class AccessKey : IAccessKey
    {
        [Required]
        public string TokenKey { get; set; }
        [Required]
        public string GroupKey { get; set; }

    }
}