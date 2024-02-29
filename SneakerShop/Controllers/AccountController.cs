using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SneakerShop.Models;
using SneakerShop.ViewModels;
using System.Net.NetworkInformation;
using System.Threading;
using Microsoft.AspNetCore.Identity;


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
                    return RedirectToAction("Index","Home");
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

                Users user = new() { 
                    Name = model.Name,
                    UserName = model.UserName,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
                    
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
                    await userManager.AddToRoleAsync(user, "Member");
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index","Home");
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




