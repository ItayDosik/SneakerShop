using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }
        [Required]
        public string ProductPictureURL1 { get; set; }
        [Required]
        public string ProductPictureURL2 { get; set; }
        [Required]
        public string ProductPictureURL3 { get; set; }
        [Required]
        public int Qnt { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Size { get; set; }

    }
}
