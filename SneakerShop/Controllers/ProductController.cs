﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SneakerShop.Migrations;
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
                    product.locationInStore = _product.locationInStore;

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
            if (category == "Show"){
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

        public ActionResult LocationFilter(string location)
        {
            List<Product> results = new List<Product>();
            if (location == "Show All")
            {
                results = _db.Products.ToList();
                return RedirectToAction("ViewAllProducts");
            }
            else
            {
                results = _db.Products.Where(p => p.locationInStore.Contains(location)).ToList();

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
                List<Product> productsList = new List<Product>();
                switch (sort)
                {
                    case "Price Increase":
                        productsList = _db.Products.OrderByDescending(s => s.Price).ToList();
                        break;
                    case "Price Decrease":
                        productsList = _db.Products.OrderBy(s => s.Price).ToList();
                        break;
                    case "Category":
                        productsList = _db.Products.OrderBy(s => s.Category).ToList();
                        break;
                    case "Most Popular":
                        productsList = _db.Products.OrderBy(s => s.Qnt).ToList();
                        break;
                    case "Remove Filter":
                        productsList = _db.Products.ToList();
                        break;
                    case "Sale":
                        productsList = _db.Products.Where(product => product.IsOnSale == true).ToList();
                        break;
                    default:
                        productsList = _db.Products.ToList();
                        break;
                }
                return View("Products", productsList);
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }

            return RedirectToAction("ViewAllProducts");


        }

        public ActionResult NotifyMe()
        {
            TempData["SuccessMessage"] = "We'll notify you once the product is back in stock";
            return RedirectToAction("ViewAllProducts");
        }




    }



}
