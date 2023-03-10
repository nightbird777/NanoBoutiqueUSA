using Microsoft.AspNetCore.Mvc;
using NanoBoutiqueUSA.Models;
using System.Text;

namespace NanoBoutiqueUSA.Controllers
{
    public class BoutiqueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult getAll()
        {
            UserDB userDB = new UserDB();
            List<User> users = userDB.getAllUsers();
            ViewBag.User = users;
            return View();
        }

        public IActionResult editUser(int id)
        {
            UserDB userDB = new UserDB();
            User user = userDB.getUserById(id);
            ViewBag.User = user;
            return View();
        }

        public IActionResult editUserSave(User user)
        {
            UserDB userDB = new UserDB();
            userDB.saveEditUser(user);
            List<User> users = userDB.getAllUsers();
            return RedirectToAction("getAll");
        }

        public IActionResult DeleteUser(int id)
        {
            UserDB userDB = new UserDB();
            userDB.removeUser(id);
            List<User> users = userDB.getAllUsers();
            return RedirectToAction("getAll");
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        public IActionResult SaveNewUser(User user)
        {
            UserDB userDB = new UserDB();
            userDB.CreateNewUser(user);
            List<User> users = userDB.getAllUsers();
            return RedirectToAction("getAll");
        }

        public IActionResult ExportAsCSV()
        {
            UserDB userDB = new UserDB();
            List<User> users = userDB.getAllUsers();

            var builder = new StringBuilder();
            builder.AppendLine("Name, Phone, Email, AddressLine, City, State, Zip, Password, CreatedBy, UpdatedBy, DateCreated, DateLastUpdated, Active");

            foreach (var user in users)
            {
                builder.AppendLine($"{user.Name}, {user.Phone}, {user.Email}, {user.AddressLine}, {user.City}, {user.State}, {user.Zip}, {user.Password}, " +
                    $"{user.CreatedBy}, {user.UpdatedBy}, {user.DateCreated}, {user.DateLastUpdated}, {user.Active}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Users.csv");
        }
    }
}
