using Microsoft.AspNetCore.Mvc;
using ErrorHandlerApp.Data;
using ErrorHandlerApp.Models;
using Microsoft.EntityFrameworkCore;


namespace ErrorHandlerApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var reports = _context.Reports.ToList();
            return View(reports);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Report report)
        {
            if (ModelState.IsValid)
            {
                _context.Reports.Add(report);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }
    }
}
