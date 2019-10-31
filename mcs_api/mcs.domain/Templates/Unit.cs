using System.ComponentModel.DataAnnotations;

namespace mcs.domain.Templates
{
    sealed public class Unit : Property
    {
        [Required(ErrorMessage = "A Unit id is required!"), MaxLength(4)]
        public string Unit_Id { get; set; }
        [Required(ErrorMessage = "Resale must be set to either true or false!")]
        public bool Resale { get; set; }
        [Required(ErrorMessage = "Enter a valid unit type ID!")]
        public int Unit_Type_Id { get; set; }
        [Required(ErrorMessage = "Building is required! It can be a building number or villa nr etc.")]
        public string Building { get; set; }
        [Required(ErrorMessage = "A Unit id is required!")]
        public int Bathroom { get; set; }
        [Required(ErrorMessage = "A Unit id is required!")]
        public int Bedroom { get; set; }
        [Required(ErrorMessage = "Enter a valid unit size!")]
        public double Size { get; set; }
        public double LandSize { get; set; }
        [Required(ErrorMessage = "Enter a valid floor")]
        public int Floor { get; set; }
        [Required(ErrorMessage = "Enter a valid ownership type ID!")]
        public int Ownership_Type_Id { get; set; }
    }
}