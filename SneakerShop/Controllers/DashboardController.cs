using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SneakerShop.Models;
using SneakerShop.ViewModels;

namespace SneakerShop.Controllers
{
    public class DashboardController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        public DashboardController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }




        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View("dashboard");
        }

        public IActionResult addProduct()
        {
            return View();
        }
        //
        public IActionResult removeProduct()
        {


            return View();
        }
        public IActionResult editProduct()
        {
            return View();
        }
        public IActionResult manageSupply()
        {
            return View();
        }
        public IActionResult findProduct()
        {
            return View();
        }





        public IActionResult removeUser()
        {
            var RemoveUserViewModel = new RemoveUserVM { 
            
                users = userManager.Users.ToList()
            };
            return View(RemoveUserViewModel);
        }

    [HttpPost]
        public async Task<IActionResult> removeUser(RemoveUserVM model)
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
                return RedirectToAction("removeUser");
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
                return RedirectToAction("removeUser"); 
        }

        // helper function for passing user object to removeUserConfirm
        public async Task<IActionResult> findUserById(string id)
        {
            Users user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "User cannot be found");
                return View("removeUser");
            }
            return RedirectToAction("removeUserConfirm",user);
        }

        public IActionResult addUser()
        {
            return View();
        }
        public IActionResult editUser()
        {
            return View();
        }

    }
}
