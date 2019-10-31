using System.ComponentModel.DataAnnotations;

using System.Linq;

namespace mcs.domain.Templates
{
    public class Property
    {
        public int Ref_Id { get; set; }
        [Required(ErrorMessage = "A title is required"), MaxLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "A Description is required"), MaxLength(2500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "A rating must be either 0 or more but not higher than 5")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Please assigne a price ")]
        public double Price { get; set; }
        public int Parent_Ref_Id { get; set; }
        public int Address_Id { get; set; }
        public string Vr_Image { get; set; } = "Not defined";
        public string Main_Image { get; set; }
        public bool Website { get; set; }
    }
}