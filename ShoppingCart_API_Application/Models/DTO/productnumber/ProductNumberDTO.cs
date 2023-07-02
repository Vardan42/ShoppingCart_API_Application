using System.ComponentModel.DataAnnotations;

namespace ShoppingCart_API_Application.Models.DTO.productnumber
{
    public class ProductNumberDTO
    {
        [Required]
        public int ProductNo { get; set; }
        [Required]
        public int ProductId { get; set; }
        public string SpecialDetails { get; set; }
        public Product Product { get; set; }
    }
}
