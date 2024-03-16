using RedMango_API.Models.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedMango_API.Models.Dto
{
    public class CartItemDTO
    {
        public int Id { get; set; }

        public int MenuItemId { get; set; }
        public MenuItemUpdateDTO MenuItem { get; set; } = new();
        public int Quantity { get; set; }
        public int ShoppingCartId { get; set; }
    }
}
