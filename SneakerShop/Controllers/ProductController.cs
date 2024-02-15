using Microsoft.AspNetCore.Mvc;
using SneakerShop.Models;

namespace SneakerShop.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Enter()
        {
            return View();
        }

        public ActionResult Submit(Product product)
        {
            return View("Product", product);
        }

    }


}
