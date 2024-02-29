using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ProductPictureURL1 { get; set; }
        public string ProductPictureURL2 { get; set; }
        public string ProductPictureURL3 { get; set; }
        public int Qnt { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
    }
}
