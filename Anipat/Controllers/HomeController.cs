using Anipat.DAL;
using Anipat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Anipat.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeVM
            {
                Services = _context.Services.ToList(),
                Statistics = _context.Statistics.ToList(),
                Testimonials = _context.Testimonials.ToList(),
                Teams = _context.Teams.ToList(),
                // Bu hissəni əlavə etdik ki, heyvanlar da ana səhifəyə gəlsin
                LatestPets = _context.Pets.Where(p => !p.IsAdopted).Take(3).ToList()
            };

            return View(model);
        }
    }
}