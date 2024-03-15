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
    }

    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly CartController CartController;
        public ProductService(AppDbContext db)
        {
            _db = db;
            CartController = new CartController(db);
        }

        public List<CartItem> GetCartItems(string userID)
        {
            Cart cart = _db.Carts.Include(c => c.cartItems).ThenInclude(ci => ci.product).FirstOrDefault(cid => cid.UserId == userID);
            return cart.cartItems;
        }
    }
}
