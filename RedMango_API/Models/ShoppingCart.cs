using System.ComponentModel.DataAnnotations.Schema;

namespace RedMango_API.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public string StripePaymentIntentId { get; set; }
        public string ClientSecret { get; set; }

        IEnumerable<CartItem> CartItems { get; set; }

        [NotMapped]
        public double CartTotal { get; set; }
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; } = new();
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
