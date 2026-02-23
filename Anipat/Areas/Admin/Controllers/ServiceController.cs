using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Anipat.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        // http://localhost:5112/Admin/Service/Index
        public IActionResult Index()
        {
            var services = _context.Services.ToList();
            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid) return View(service);

            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        public IActionResult Update(Service updatedService)
        {
            var dbService = _context.Services.Find(updatedService.Id);
            if (dbService == null) return NotFound();

            dbService.Title = updatedService.Title;
            dbService.Description = updatedService.Description;
            dbService.IconPath = updatedService.IconPath;
            dbService.IsActive = updatedService.IsActive;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var service = _context.Services.Find(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}