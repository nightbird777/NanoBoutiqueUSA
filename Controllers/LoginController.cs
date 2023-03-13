using Microsoft.AspNetCore.Mvc;
using NanoBoutiqueUSA.Models;
using System.Net.Mail;
using System.Net;

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

                Random random = new Random();
                string otp = random.Next(0, 999999).ToString("D6");


                MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("practicetest2027@gmail.com");
                    msg.To.Add(login.Email.ToString());
                    msg.Subject = "Code to verify login info";
                    msg.Body = otp.ToString();

                    SmtpClient smtpClient = new SmtpClient();
                    smtpClient.Host = "smtp.gmail.com";
                    NetworkCredential ntcd = new NetworkCredential();
                    ntcd.UserName = "practicetest2027@gmail.com";
                    ntcd.Password = "uqieeglrmbgbhkta";
                    smtpClient.Credentials = ntcd;
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    smtpClient.Send(msg);

                    return RedirectToAction("twoStepVerification", "Login");
            }
        }

        public IActionResult twoStepVerification()
        {
            return View();
        }

        public IActionResult verifyOTP(string otp, string otpCode)
        {
            if (otp == otpCode)
            {
                return RedirectToAction("Index", "Boutique");
            }
            else
            {
                return RedirectToAction("Login", "Login");
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
