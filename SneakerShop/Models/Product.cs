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
        public string ProductPictureURL1 { get; set; }
        public string ProductPictureURL2 { get; set; }
        public string ProductPictureURL3 { get; set; }
        public int Qnt { get; set; }
        public string Category { get; set; }
        public string Size { get; set; }
        


    }
}
