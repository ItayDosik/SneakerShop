using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SneakerShop.Controllers
{
    public class DashboardController : Controller
    {

        [Authorize(Roles = "Admin")]
        public IActionResult Dashboard()
        {
            return View("dashboard");
        }
    }
}
