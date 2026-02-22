using Anipat.DAL;
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

        
        public async Task<IActionResult> Index()
        {
            var pets = await _context.Pets.Where(p => !p.IsAdopted).ToListAsync();
            return View(pets);
        }

       
        public async Task<IActionResult> Details(int id)
        {
            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == id);
            if (pet == null) return NotFound();

            return View(pet);
        }
    }
}