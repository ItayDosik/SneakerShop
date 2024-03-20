using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SneakerShop.Controllers;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using System.Security.Claims;



namespace SneakerShop.Services
{
    public interface IProductService
    {
        List<CartItem> GetCartItems(string userID);
        List<CartItem> GetCartItems(int cartSessionID);
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly CartController CartController;
        public ProductService(AppDbContext db, UserManager<Users> userManager)
        {
            _db = db;
            CartController = new CartController(db, userManager);
        }

        public List<CartItem> GetCartItems(string userID)
        {
            Cart cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.UserId == userID);
            if(cart == null)
            {
                cart = new Cart();
                cart.UserId = userID;
                cart.cartItems = new List<CartItem>();
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }
            return cart.cartItems;
        }

        public List<CartItem> GetCartItems(int cartSessionID)
        {
            Cart cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.CartId == cartSessionID);
            if (cart == null)
            {
                cart = new Cart();
                cart.cartItems = new List<CartItem>();
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }
            return cart.cartItems;
        }
    }
}
