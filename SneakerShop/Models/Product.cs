using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Description is required")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string ProductPictureURL1 { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string ProductPictureURL2 { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string ProductPictureURL3 { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Qnt { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Size is required")]

        public string Size { get; set; }

    }
}
