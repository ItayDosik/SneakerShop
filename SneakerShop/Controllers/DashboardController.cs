using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SneakerShop.Models;
using SneakerShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using SneakerShop.Models.Data;
using System.Collections.Generic;
using SneakerShop.Controllers;
using SneakerShop.Migrations;
using Microsoft.EntityFrameworkCore;
namespace SneakerShop.Controllers
{
    public class DashboardController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly ProductController productController;
        private readonly AppDbContext _db;
        public DashboardController(SignInManager<Users> signInManager, UserManager<Users> userManager, AppDbContext db)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            productController = new ProductController(db);
            _db = db;

        }

        [Authorize(Roles = "Admin")]
        public IActionResult manageSupply()
        {
            List<Product> results = _db.Products.ToList();
            return View("manageSupply", new productManagement { products = results });
        }

        [HttpPost]
        public IActionResult UpdateQuantities(Dictionary<int, int> products)
        {
            if (ModelState.IsValid)
            {
                foreach (var productId in products.Keys)
                {
                    var dbProduct = _db.Products.Find(productId);
                    if (dbProduct != null)
                    {
                        
                        dbProduct.Qnt += products[productId];
                    }
                }

                _db.SaveChanges();

                TempData["SuccessMessage"] = "Quantities updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid data submitted.";
            }

            return RedirectToAction("ManageSupply");
        }


























        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View("dashboard");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult NewProduct()
        {
            return View("NewProduct");
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
                return RedirectToAction("productManagement");
            }
            else
            {
                TempData["ErrorMessage"] = "Product cannot be added, check again for the values you entered.";
                return RedirectToAction("productManagement");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult removeProduct(int id) 
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

            return RedirectToAction("productManagement", "Dashboard");

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return productController.Edit(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Product _product) 
        {
            try
            {
                productController.Edit(_product);
                TempData["SuccessMessage"] = "Product successfully saved";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while saving the product.";
                return RedirectToAction("productManagement");
            }


            return RedirectToAction("productManagement");
        }

        public IActionResult backwardToProductManagement()
        {
            List<Product> results = _db.Products.ToList();
            return View("productManagement", new productManagement { products = results });
        }

        public IActionResult productManagement()
        {
            List<Product> results = _db.Products.ToList();
            return View("productManagement", new productManagement { products = results });
        }

        [HttpPost]
        public IActionResult productManagement(string keyWord)
        {
            List<Product> results = _db.Products.Where(p => p.ProductName.Contains(keyWord) || p.ProductDescription.Contains(keyWord)).ToList(); 
            return View("productManagement", new productManagement { products = results});
        }

        public IActionResult Edit()
        {
            return View("Error");
        }

        public async Task<IActionResult> userManagement()
        {

            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account"); 
            }
            var allUsersExceptCurrent = userManager.Users.Where(u => u.Id != currentUser.Id).ToList();

            return View("userManagement", new RemoveUserVM { users = allUsersExceptCurrent });

        }

    [HttpPost]
        public async Task<IActionResult> userManagement(RemoveUserVM model)
        {
            var method = model.method; 
            var identifier = model.identifier;
            Users user = null;

            if (method == "username")
                user = await userManager.FindByNameAsync(identifier);
            else
                user = await userManager.FindByEmailAsync(identifier);


            if (user == null)
            {
                TempData["ErrorMessage"] = $"User with {model.identifier} cannot be found";
                return View(new RemoveUserVM { users = userManager.Users.ToList() });
            }
            return View("removeUserConfirm",user);

        }
        
        public  IActionResult removeUserConfirm(Users user)
        {
            return View("removeUserConfirm", user);
        }

        [HttpPost]
        public async Task<IActionResult> removeUserConfirm(string id)
        { 
            Users user = await userManager.FindByIdAsync(id);
            if (user == null) 
            {
                TempData["ErrorMessage"] = "User cannot be found.";
                return RedirectToAction("userManagement");
            }
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User removed successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong, try again later.";
            }
                return RedirectToAction("userManagement"); 
        }

        // helper function for passing user object to removeUserConfirm
        public async Task<IActionResult> findUserById(string id)
        {
            Users user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "User cannot be found");
                return View("userManagement");
            }
            return RedirectToAction("removeUserConfirm",user);
        }

        public IActionResult addUser()
        {
            return View();
        }
        
        public async Task<IActionResult> EditUser(string UserId)
        {

            Users user = await userManager.FindByIdAsync(UserId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {UserId} cannot be found";
                return View("NotFound");
            }

            var userRoles = await userManager.GetRolesAsync(user); //users with no roles or more than 1 role change to Member as default 
            if(userRoles.Count() == 0 || userRoles.Count() > 1 ){
                userRoles = ["Member"];
            }

            var model = new EditUserVM
            {
                editUserName = user.UserName,
                editName = user.Name,
                editEmail = user.Email,
                editPhoneNumber = user.PhoneNumber,
                editRole = userRoles[0], // assuming that each user has one Role
                id = UserId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> saveUserEdit(EditUserVM editUser)
        {
            var user = await userManager.FindByIdAsync(editUser.id!); 
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User {editUser.editUserName} cannot be found"; 
                return View("userManagement");
            }

            // check if the new email is already in use
            var existingUserByEmail = await userManager.FindByEmailAsync(editUser.editEmail!);
            if (existingUserByEmail != null && existingUserByEmail.Id != user.Id)
            {
                ModelState.AddModelError("editEmail", "Email is already in use.");
                return View("editUser", editUser);
            }

            // check if the new username is already in use
            var existingUserByName = await userManager.FindByNameAsync(editUser.editUserName!);
            if (existingUserByName != null && existingUserByName.Id != user.Id)
            {
                ModelState.AddModelError("editUserName", "Username is already in use.");
                return View("editUser", editUser);
            }

            // validate phone number using regular expression
            if (!Regex.IsMatch(editUser.editPhoneNumber!, @"^0(5[0-9])\d{7}$"))
            {
                ModelState.AddModelError("editPhoneNumber", "Invalid phone number.");
                return View("editUser", editUser);
            }


            user.Email = editUser.editEmail;
            user.UserName = editUser.editUserName;
            user.Name = editUser.editName;
            user.PhoneNumber = editUser.editPhoneNumber;
            

            // remove old roles and add new role
            var roles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, roles);
            await userManager.AddToRoleAsync(user, editUser.editRole!);


            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "User has been updated successfully!";
                return RedirectToAction("userManagement");
            }
            else
            {
                TempData["ErrorMessage"] = "Something went wrong. Please try again later.";
            }

            return View(user);
        }


        public IActionResult createUser()
        {
            return View("createUser");
        }


        [HttpPost]
        public async Task<IActionResult> createUser(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                Users user = new()
                {
                    Name = model.Name,
                    UserName = model.UserName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Role = model.Role
                };

                var existingEmail = await userManager.FindByEmailAsync(model.Email!);
                if (existingEmail != null)
                {
                    ModelState.AddModelError("", "Email address is already in use.");
                    return View(model);
                }


                var result = await userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, model.Role!);
                    TempData["SuccessMessageCreate"] = "User Created!";
                    return RedirectToAction("createUser", "Dashboard");
                }
                else
                {
                    TempData["ErrorMessageCreate"] = "User Not Created, Something Went Wrong!";
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View(model);
        }

    }
}
