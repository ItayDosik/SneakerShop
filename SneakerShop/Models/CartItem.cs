using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SneakerShop.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public int quantity { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product product { get; set; }


        [ForeignKey("CartId")]
        public int CartId { get; set; }
        public Cart cart { get; set; }

    }
}

