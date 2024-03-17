using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerShop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly AppDbContext _db;
        public PaymentController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string userID)
        {
            Cart my_cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.UserId == userID);
            //Cart my_cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == cartSessionID);
            return View("checkout", my_cart);
        }


        public IActionResult buyNowPayment(string userID, int productID)
        {
            Cart cart = new Cart()
            {
                UserId = userID,
                cartItems = new List<CartItem>(),  
            };
            var prod = _db.Products.Find(productID);
            cart.cartItems.Add(new CartItem()
            {
                quantity = 1,
                product = prod
            });

            return View("checkout", cart);
        }


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
