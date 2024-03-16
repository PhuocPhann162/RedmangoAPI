using System.ComponentModel.DataAnnotations.Schema;

namespace RedMango_API.Models.Dto
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? CouponCode { get; set; }
        public ICollection<CartItemDTO> CartItems { get; set; }
        public double Discount { get; set; }
        public double CartTotal { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string ClientSecret { get; set; }
    }
}
