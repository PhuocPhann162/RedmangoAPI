using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedMango_API.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int Stars { get; set; }
        [Required]
        public int MenuItemId { get; set; }
        [Required]
        public string UserId { get; set; }

        [NotMapped]
        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }

        [NotMapped]
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
