using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingCart_API_Application.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        [NotMapped]
        public double CartTotal { get; set; }
        [NotMapped]
        public string StripPaymentIntentId { get; set; }
        [NotMapped]
        public string ClientSecret { get; set; }
    }
}
