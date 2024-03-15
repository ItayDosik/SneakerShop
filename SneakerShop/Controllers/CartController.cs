using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using SneakerShop.Models.Data;
using SneakerShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Principal;
using System.Security.Permissions;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SneakerShop.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _db;

        public CartController(AppDbContext db)
        {
            _db = db;
          
        }

        public IActionResult Index()
        {
            Cart my_cart = GetCart();
            if(my_cart != null)
            {
                return View(my_cart);
            }
            return View();
        }

        public Cart GetCart()
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userID != null)
            {
                Cart my_cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.UserId == userID);
                if(my_cart == null)
                {
                    my_cart = new Cart();
                    my_cart.UserId = userID;
                    my_cart.cartItems = new List<CartItem>();
                    _db.Carts.Add(my_cart);
                    _db.SaveChanges();

                }
                return my_cart;
            }
            else
            {
                var cartSession = HttpContext.Session.GetString("cart");
                if (cartSession != null)
                {
                    int cartSessionID = int.Parse(cartSession);
                    Cart my_cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == cartSessionID);
                    return my_cart;
                }
                else
                {
                    Cart my_cart = new Cart();
                    my_cart.cartItems = new List<CartItem>();
                    _db.Carts.Add(my_cart);
                    _db.SaveChanges();
                    HttpContext.Session.SetString("cart", (my_cart.CartId).ToString());
                    return my_cart;
                }
                       
            }
        }

        bool productAvailable(int productID, int quantity, Cart cart)
        {
            var index = IsInCart(productID, cart);
            if (index == -1)
            {
                if (quantity > _db.Products.Find(productID).Qnt)
                    return false;
            }
            else
            {
                var sumQun = cart.cartItems[index].quantity + quantity;
                if (sumQun > _db.Products.Find(productID).Qnt)
                    return false;
            }
            return true;
        }


        public IActionResult AddToCart(int productID, int quantity)
        {
            Cart my_cart = GetCart();
            if(!productAvailable(productID, quantity, my_cart))
            {
                TempData["ErrorMessage"] = "The selected quantity is not in stock.";
                return RedirectToAction("ViewAllProducts", "Product");
            }
            int index = IsInCart(productID, my_cart);

            if (index != -1)
            {
                my_cart.cartItems[index].quantity += quantity;
            }
            else
            {
                my_cart.cartItems.Add(new CartItem { product = _db.Products.Find(productID), quantity = quantity, cart = my_cart });
            }

            _db.SaveChanges();
            TempData["SuccessMessage"] = "Product added to cart successfully";
            return RedirectToAction("ViewAllProducts", "Product");
        }

        public ActionResult RemoveFromCart(int productID)
        {
            Cart my_cart = GetCart();
            int index = IsInCart(productID, my_cart);
            my_cart.cartItems.RemoveAt(index);
            _db.SaveChanges();
            TempData["SuccessMessage"] = "Product removed from cart successfully";
            return RedirectToAction("ViewAllProducts", "Product");
        }

        public ActionResult RemoveOneQuantity(int productID)
        {
            Cart my_cart = GetCart();
            int index = IsInCart(productID, my_cart);
            if(my_cart.cartItems[index].quantity == 1)
            {
                RemoveFromCart(productID);
            }
            else
            {
                my_cart.cartItems[index].quantity--;
                _db.SaveChanges();
                
            }
            return RedirectToAction("ViewAllProducts", "Product");
        }

        public ActionResult AddOneQuantity(int productID)
        {
            Cart my_cart = GetCart();
            if(productAvailable(productID,1,my_cart))
            {
                int index = IsInCart(productID, my_cart);
                my_cart.cartItems[index].quantity++;
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Product removed from cart successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "The selected quantity is not in stock.";
            }
            
            return RedirectToAction("ViewAllProducts", "Product");
        }

        public int IsInCart(int ProductId, Cart my_cart)
        {
            for (int i = 0; i < my_cart.cartItems.Count; i++)
            {
                if (my_cart.cartItems[i].product.ProductId == ProductId)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}

