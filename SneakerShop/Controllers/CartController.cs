using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SneakerShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using SneakerShop.Models.Data;

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

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int productID)
        {
            var sessionVal = HttpContext.Session.Get("cart");
            if(sessionVal == null)
            {
               
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { product= _db.Products.Find(productID), quantity =1 }) ;
              
            }
            else
            {

            }
            


            return View();
        }
    }
}

