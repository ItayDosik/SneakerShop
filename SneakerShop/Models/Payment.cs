using System.ComponentModel.DataAnnotations;

namespace SneakerShop.Models
{
    public class Payment
    {
        [Key]
        public string PayID { get; set; }
        public string CartID { get; set; }
        public string PaymentMethod { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }






    }
}
