using System.ComponentModel.DataAnnotations;

namespace Property_Search_Web_App.Models.DTOs
{
    public class DTOAddSpace
    {
        [Required(ErrorMessage = "Space type is required.")]
        [StringLength(50, ErrorMessage = "Type can't be longer than 50 characters.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        [Range(1, 10000, ErrorMessage = "Size must be between 1 and 10,000 square feet.")]
        public decimal Size { get; set; }

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters.")]
        public string Description { get; set; }
    }
}
