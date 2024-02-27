using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SneakerShop.Models;
using SneakerShop.ViewModels;
using System.Threading;

namespace SneakerShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        public AccountController(SignInManager<Users> signInManager, UserManager<Users> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager; 
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if(ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName!,model.Password!,model.RememberMe,false); //the ! is for not-null check
                if(result.Succeeded)
                {
                    return RedirectToAction("Index","Home");// for testing only
                }
                ModelState.AddModelError("", "Invalid Login attempt");
                return View(model);
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
            public async Task<IActionResult> Register(RegisterVM model)
        {
            if(ModelState.IsValid)
            {
                Users user = new() { //create user object
                    Name = model.Name,
                    UserName = model.UserName,
                    Address = model.Address,
                    Phone = model.Phone,
                    Password = model.Password,
                    Email = model.Email,
                    CartID = "test1",//need to generate some how uniqe keys  
                    Role = "User",
                };
                var result = await userManager.CreateAsync(user, model.Password!);

                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index","Home");// for testing only
                }
               
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
               

            }
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
