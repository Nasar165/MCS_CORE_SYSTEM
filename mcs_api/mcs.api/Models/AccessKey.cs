using System.ComponentModel.DataAnnotations;
using mcs.api.Models.Interface;

namespace mcs.api.Models
{
    public class AccessKey : IAccessKey
    {
        [Required]
        public string TokenKey { get; set; }
        [Required]
        public string GroupKey { get; set; }

    }
}