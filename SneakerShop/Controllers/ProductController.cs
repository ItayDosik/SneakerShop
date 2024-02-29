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
            return View("ViewAllProducts");

        }

        public ActionResult Edit(int id) 
        {
            Product product = _db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Save(Product _product)
        {
            Product product = _db.Products.Find(_product.ProductId);
            product.ProductName = _product.ProductName;
            product.ProductDescription = _product.ProductDescription;
            product.Price = _product.Price;
            product.ProductPictureURL1 = _product.ProductPictureURL1;
            product.ProductPictureURL2 = _product.ProductPictureURL2;
            product.ProductPictureURL3 = _product.ProductPictureURL3;
            product.Category = _product.Category;
            product.Size = _product.Size;
            _db.SaveChanges();
            return View("ViewAllProducts");
        }

        public ActionResult ViewAllProducts()
        {
            List<Product> productList = _db.Products.ToList();
            return View("Products", productList);

        }


    }


}
