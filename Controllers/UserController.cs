using Microsoft.AspNetCore.Mvc;
using ErrorHandlerApp.Data;
using ErrorHandlerApp.Models;

namespace ErrorHandlerApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.PasswordHash = PasswordHasher.HashPassword(user.PasswordHash);
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin"); 
                }
                return RedirectToAction("Index", "Bug");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }
    }
}
