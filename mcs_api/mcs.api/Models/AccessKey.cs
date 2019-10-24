using System.ComponentModel.DataAnnotations;

namespace mcs.api.Models
{
    public class AccessKey
    {
        [Required]
        public string CompanyId { get; set; }
        [Required]
        public int GroupId { get; set; }

    }
}