using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ErrorHandlerApp.Data;
using ErrorHandlerApp.Models;
using System.Linq;

namespace ErrorHandlerApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError(string.Empty, "Username and password are required.");
                return View();
            }

            var hashedPassword = PasswordHasher.HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Role", user.Role);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View();
        }



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}
