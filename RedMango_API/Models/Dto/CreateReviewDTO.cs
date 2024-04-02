using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RedMango_API.Models.Dto
{
    public class CreateReviewDTO
    {
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Stars { get; set; }
        public int MenuItemId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
