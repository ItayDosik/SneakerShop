using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShop.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public List<CartItem> cartItems { get; set; }

        [ForeignKey("UserId")]
        public string? UserId { get; set; }


    }
}
