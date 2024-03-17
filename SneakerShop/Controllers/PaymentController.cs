using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Users> _userManager;
        public PaymentController(AppDbContext db, UserManager<Users> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index(string userID)
        {
            Cart my_cart = _db.Carts.Include(u => u.user).Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.UserId == userID);
            //Cart my_cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == cartSessionID);
            return View("checkout", my_cart);
        }


        public async Task<IActionResult> buyNowPayment(string userID, int productID)
        {
            Cart cart = new Cart()
            {
                UserId = userID,
                user = await _userManager.GetUserAsync(User),
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



    }
}
