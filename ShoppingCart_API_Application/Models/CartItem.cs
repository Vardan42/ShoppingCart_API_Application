using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart_API_Application.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; }
        public int ShoppingCartId {
            get; set;
        } 
    }
}
