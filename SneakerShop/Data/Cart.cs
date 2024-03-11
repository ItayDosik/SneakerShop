using System.ComponentModel.DataAnnotations;
using SneakerShop.Models;

namespace SneakerShop.Data
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        List<CartItem> cartItems { get; set; }




    }
}
