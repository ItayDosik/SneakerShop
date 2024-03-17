using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakerShop.Models;
using SneakerShop.ViewModels;
using SneakerShop.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;
using SneakerShop.Migrations;

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
            


            PaymentVM payment = new PaymentVM(); 
            payment.cart = _db.Carts.Include(u => u.user).Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.UserId == userID);
            foreach (var item in payment.cart.cartItems)
            {
                if(item.quantity > _db.Products.Find(item.ProductId).Qnt)
                {
                    TempData["ErrorMessage"] = item.product.ProductName + " is not available in selected quantity.";
                    return RedirectToAction("ViewAllProducts","Product");
                }
            }

            //Cart my_cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == cartSessionID);
            return View("checkout", payment);
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

            _db.Carts.Add(cart);
            _db.SaveChanges();
            PaymentVM payment = new PaymentVM();
            payment.cart = cart;


            return View("checkout", payment);
        }

        public IActionResult checkoutPur(PaymentVM pay)
        {

            pay.cart = _db.Carts.Include(u => u.user).Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == pay.cartID);

            foreach (var item in pay.cart.cartItems)
            {
                _db.Products.Find(item.ProductId).Qnt -= item.quantity;
                _db.SaveChanges();
            }

            _db.Carts.Remove(pay.cart);
            _db.SaveChanges();
            return View("paymentSuccessed");
        }



    }
}
