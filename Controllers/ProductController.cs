using Microsoft.AspNetCore.Mvc;
using NanoBoutiqueUSA.Models;

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
            ProductDB productDB = new ProductDB();
            List<Product> products = productDB.getAllProducts();
            ViewBag.Product = products;
            return View();
        }

        public IActionResult editProduct(int id)
        {
            ProductDB productDB = new ProductDB();
            Product product = productDB.getProductById(id);
            ViewBag.Product = product;
            return View();
        }

        public IActionResult editProductSave(Product product, IFormFile img)
        {
            ProductDB productDB = new ProductDB();

            var fileName = Path.GetFileName(img.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            var stream = new FileStream(filePath, FileMode.Create);
            img.CopyToAsync(stream);
            product.Image = fileName;
            ViewBag.message = "File Created successfully";

            productDB.saveEditProduct(product);
            List<Product> products = productDB.getAllProducts();
            return RedirectToAction("allProduct");
        }

        public IActionResult DeleteProduct(int id)
        {
            ProductDB productDB = new ProductDB();
            productDB.removeProduct(id);
            List<Product> products = productDB.getAllProducts();
            return RedirectToAction("allProduct");
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        public IActionResult SaveNewProduct(Product product, IFormFile img)
        {
            ProductDB productDB = new ProductDB();

            var fileName = Path.GetFileName(img.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
            var stream = new FileStream(filePath, FileMode.Create);
            img.CopyToAsync(stream);
            product.Image = fileName;
            ViewBag.message = "File Created successfully";

            productDB.CreateNewProduct(product);
            List<Product> products = productDB.getAllProducts();
            return RedirectToAction("allProduct");
        }

        public ActionResult searchProduct()
        {
            string search = Request.Form["Name"];
            ProductDB productDB = new ProductDB();
            List<Product> products = productDB.searchProduct(search);
            ViewBag.product = products;
            return View("CustomerView");
        }

        public IActionResult CustomerView()
        {
            ProductDB productDB = new ProductDB();
            List<Product> products = productDB.getAllProducts();
            ViewBag.Product = products;
            return View();
        }
    }
}
