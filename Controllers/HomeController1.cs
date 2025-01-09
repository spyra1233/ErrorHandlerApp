using Microsoft.AspNetCore.Mvc;

namespace ErrorHandlerApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            return View();
        }
    }
}
