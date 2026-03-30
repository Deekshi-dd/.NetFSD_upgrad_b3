using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Product
    {
        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Name must be 5 to 15 characters")]
        public string ? ProductName { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Category must be 5 to 15 characters")]
        public string ? Category { get; set; }
    }
}
