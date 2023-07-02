using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart_API_Application.Models
{
    public class ProductNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductNo { get; set; }
        public string SpecialDetails { get; set; }  
        public DateTime CreatedData { get; set; }
        public int ProductId { get;set; }
        public Product Product { get; set; }
    }
}
