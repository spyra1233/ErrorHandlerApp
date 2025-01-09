using Microsoft.AspNetCore.Mvc;
using ErrorHandlerApp.Data;
using ErrorHandlerApp.Models;
using Microsoft.EntityFrameworkCore;


namespace ErrorHandlerApp.Controllers
{
    public class BugController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BugController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bugs = _context.Bugs.ToList();
            return View(bugs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Bugs.Add(bug);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(bug);
        }
    }
}
