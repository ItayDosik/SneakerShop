using Microsoft.AspNetCore.Mvc;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using System.Data;
using System.Data.SqlClient;


namespace SneakerShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

       public ProductController(AppDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            List<Product> ProductList = new List<Product>();
            _db.Products.ToList();
            return View();
        }

        public ActionResult NewProduct()
        {
            return View();
        }


        public ActionResult AddProduct(Product product)
        {
            _db.Products.Add(product);
            _db.SaveChanges();
            return View("NewProduct");

        }

        public ActionResult ViewAllProducts()
        {
            List<Product> productList = _db.Products.ToList();
            return View("Products", productList);

        }


    }


}
