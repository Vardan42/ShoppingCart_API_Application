using System.ComponentModel.DataAnnotations;

namespace ShoppingCart_API_Application.Models.DTO.productnumber
{
    public class ProductNumberUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; }
    }
}
