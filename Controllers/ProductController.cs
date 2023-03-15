using Microsoft.AspNetCore.Mvc;

namespace NanoBoutiqueUSA.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult allProduct()
        {
            return View();
        }
    }
}
