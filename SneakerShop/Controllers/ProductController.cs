using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SneakerShop.Models;
using SneakerShop.Models.Data;
using System;
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



        [Authorize(Roles = "Admin")]
        public ActionResult NewProduct()
        {
            return View();

        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id) 
        {
            Product product = _db.Products.Find(id);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
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
                    product.Qnt = _product.Qnt;
                    product.Category = _product.Category;
                    product.Size = _product.Size;
                    product.SalePercentage = _product.SalePercentage;

                    if(_product.SalePercentage > 0)
                        product.IsOnSale = true;
                    else
                        product.IsOnSale = false;

                    
                    _db.SaveChanges();
                    if (TempData != null)
                    {
                        TempData["SuccessMessage"] = "Product successfully saved";
                    }
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

        }

        [Authorize(Roles = "Admin")]
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
            if (results.Count == 0)
            {
                results = _db.Products.ToList();
                TempData["ErrorMessage"] = "No sneakers found";
                return RedirectToAction("ViewAllProducts");
            }

            return View("Products", results);

        }

        public ActionResult CategoryFilter(string category)
        {
            List<Product> results = new List<Product>();
            if (category.Contains("Show")){
                results = _db.Products.ToList();
                return RedirectToAction("ViewAllProducts");
            }
            else
            {
                results = _db.Products.Where(p => p.Category.Contains(category)).ToList();
              
            }

            if (results.Count == 0)
            {
                TempData["ErrorMessage"] = "No sneakers found";
                return RedirectToAction("ViewAllProducts");
            }
            return View("Products", results);
        }

        public ActionResult SearchByPrice(string search)
        {
            List<Product> results = new List<Product>();
            if (search == null)
            {
                results = _db.Products.ToList();
                TempData["ErrorMessage"] = "Enter price range";
                return RedirectToAction("ViewAllProducts");
            }
            results = _db.Products.Where(p => p.Price >= 1 && p.Price <= decimal.Parse(search)).ToList();

            if (results.Count == 0)
            {
                results = _db.Products.ToList();
                TempData["ErrorMessage"] = "No sneakers found";
                return RedirectToAction("ViewAllProducts");
            }

            return View("Products", results);

        }

        public ActionResult Sort(string sort)
        {
            try
            {
                var productsList = from s in _db.Products
                                   select s;
                switch (sort)
                {
                    case "Price Increase":
                        productsList = productsList.OrderByDescending(s => s.Price);
                        break;
                    case "Price Decrease":
                        productsList = productsList.OrderBy(s => s.Price);
                        break;
                    case "Category":
                        productsList = productsList.OrderByDescending(s => s.Category);
                        break;
                    case "Most Popular":
                        productsList = productsList.OrderByDescending(s => s.Category);
                        break;
                    case "Remove Filter":
                        productsList = _db.Products;
                        break;
                    default:
                        productsList = _db.Products;
                        break;
                }
                return View("Products", productsList.ToList());
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("ViewAllProducts");


        }

    }



}
