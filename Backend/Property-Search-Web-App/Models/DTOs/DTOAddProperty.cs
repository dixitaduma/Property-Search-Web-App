using System.ComponentModel.DataAnnotations;

namespace Property_Search_Web_App.Models.DTOs
{
    public class DTOAddProperty
    {
        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address can't be longer than 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        [StringLength(100, ErrorMessage = "Type can't be longer than 100 characters.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }


        [StringLength(1000, ErrorMessage = "Description can't be longer than 1000 characters.")]
        public string Description { get; set; }

      
        public List<DTOAddSpace> Spaces { get; set; }
    }
}
