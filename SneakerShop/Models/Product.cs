using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Product
    {
        [Key]
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Price { get; set; }
        public string ProductPictureURL { get; set; }
        public string ReleaseDate { get; set; }
        public string Category { get; set; }
    }
}
