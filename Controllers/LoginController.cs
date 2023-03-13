using Microsoft.AspNetCore.Mvc;
using NanoBoutiqueUSA.Models;

namespace NanoBoutiqueUSA.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            ViewBag.ErrorMessage = "";
            return View();
        }

        public IActionResult LoginAction(string email, string password)
        {
            LoginDB loginDB = new LoginDB();
            Login login = loginDB.signIn(email, password);

            if (login.UserId == 0)
            {
                ViewBag.ErrorMessage = "Username and password did not match any user";
                HttpContext.Session.SetString("isLoggedIn", "False");
                return View("Login");
            }
            else
            {
                ViewBag.ErrorMessage = "";
                HttpContext.Session.SetString("isLoggedIn", "True");
                HttpContext.Session.SetString("UserId", login.UserId.ToString());
                HttpContext.Session.SetString("Email", login.Email);
                HttpContext.Session.SetString("Password", login.Password);                
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult LogoutAction()
        {
            ViewBag.ErrorMessage = "";
            HttpContext.Session.SetString("isLoggedIn", "False");
            return View("Login");
        }
    }
}
