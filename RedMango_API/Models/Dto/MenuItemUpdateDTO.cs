using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RedMango_API.Models.DTO
{
    public class MenuItemUpdateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpecialTag { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public IFormFile File { get; set; }
    }
}
