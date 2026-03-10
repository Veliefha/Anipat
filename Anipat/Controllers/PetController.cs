using Anipat.DAL;
using Anipat.Models;
using Anipat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Controllers
{
    public class PetController : Controller
    {
        private readonly AppDbContext _context;

        public PetController(AppDbContext context)
        {
            _context = context;
        }

        // --- BÜTÜN HEYVANLAR (SİYAHI) ---
        public async Task<IActionResult> Index()
        {
            // Sadəcə övladlığa götürülməyənləri göstəririk
            var pets = await _context.Pets.Where(p => !p.IsAdopted).ToListAsync();
            return View(pets);
        }

        // --- DETALLAR SƏHİFƏSİ ---
        public async Task<IActionResult> Details(int id)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (pet == null) return NotFound();
            return View(pet);
        }

        // --- ŞƏHƏRƏ GÖRƏ FİLTER (Xəritə əvəzinə) ---
        // Artıq məsafə hesablamırıq, sadəcə şəhər adına görə axtarırıq
        public async Task<IActionResult> FilterByCity(string city)
        {
            var pets = _context.Pets.Where(p => !p.IsAdopted);

            if (!string.IsNullOrEmpty(city))
            {
                pets = pets.Where(p => p.City.Contains(city));
            }

            return View("Index", await pets.ToListAsync());
        }
    }
}