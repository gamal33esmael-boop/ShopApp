using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 99999)]
        public decimal Price { get; set; }

        [Range(0, 10000)]
        public int Stock { get; set; }

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}