using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace SneakerShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;

       public ProductController(AppDbContext db)
        {
            _db = db;
        }


        public ActionResult ViewAllProducts()
        {
            List<Product> productList = _db.Products.ToList();
            return View("Products", productList);
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


        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Product: " + product.ProductName + " added successfully.";
                return RedirectToAction("ViewAllProducts");
            }
            else
            {
                TempData["ErrorMessage"] = "Product cannot be added, check again for the values you entered.";
                return RedirectToAction("ViewAllProducts");
            }
           

        }

        public ActionResult Edit(int id) 
        {
            Product product = _db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product _product)
        {
            try
            {
                Product product = _db.Products.Find(_product.ProductId);

                if (ModelState.IsValid && product != null)
                {
                    product.ProductName = _product.ProductName;
                    product.ProductDescription = _product.ProductDescription;
                    product.Price = _product.Price;
                    product.ProductPictureURL1 = _product.ProductPictureURL1;
                    product.ProductPictureURL2 = _product.ProductPictureURL2;
                    product.ProductPictureURL3 = _product.ProductPictureURL3;
                    product.Category = _product.Category;
                    product.Size = _product.Size;
                    _db.SaveChanges();
                    TempData["SuccessMessage"] = "Product: " + product.ProductName + " saved successfully.";
                    return RedirectToAction("ViewAllProducts");
                }
                else
                {
                    TempData["ErrorMessage"] = "Product cannot be edited, check again for the values you entered.";
                    return RedirectToAction("ViewAllProducts");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while saving the product.";
                return RedirectToAction("ViewAllProducts");
            }

            //product.ProductName = _product.ProductName;
            //product.ProductDescription = _product.ProductDescription;
            //product.Price = _product.Price;
            //product.ProductPictureURL1 = _product.ProductPictureURL1;
            //product.ProductPictureURL2 = _product.ProductPictureURL2;
            //product.ProductPictureURL3 = _product.ProductPictureURL3;
            //product.Category = _product.Category;
            //product.Size = _product.Size;
            //_db.SaveChanges();
            //return RedirectToAction("ViewAllProducts");
        }


        public ActionResult Delete(int id)
        {

            try
            {
                Product product = _db.Products.Find(id);

                if (product != null)
                {
                    _db.Products.Remove(product);
                    _db.SaveChanges();

                    if (_db.Products.Find(id) == null)
                    {
                        TempData["SuccessMessage"] = "Product removed successfully.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Product removal failed.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Product cannot be found.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("ViewAllProducts");


        }


        public ActionResult Search(string search)
        {
            List<Product> results = _db.Products.Where(p => p.ProductName.Contains(search) || p.ProductDescription.Contains(search)).ToList();
           
            return View("SearchItems", results);

        }




        public ActionResult SearchItems(List<Product> productList)
        {
            
            return View(productList);
        }

    }


}
