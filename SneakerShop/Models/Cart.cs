using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Cart
    {
        [Key]
        public string CartID { get; set; }

        [Key]
        public string ProductID { get; set; }

        public int Qnt { get; set; }
       


    }
}
