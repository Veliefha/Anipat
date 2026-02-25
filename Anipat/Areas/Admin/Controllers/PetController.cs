using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PetController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PetController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // --- LİSTƏLƏMƏ ---
        public async Task<IActionResult> Index()
        {
            var pets = await _context.Pets.OrderByDescending(p => p.CreatedAt).ToListAsync();
            return View(pets);
        }

        // --- DETALLAR (BU HİSSƏ ƏLAVƏ EDİLDİ) ---
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pet = await _context.Pets.FirstOrDefaultAsync(m => m.Id == id);

            if (pet == null) return NotFound();

            return View(pet);
        }

        // --- YARATMAQ (GET) ---
        [HttpGet]
        public IActionResult Create() => View();

        // --- YARATMAQ (POST) ---
        [HttpPost]
        public async Task<IActionResult> Create(Pet pet)
        {
            // ModelState yoxlaması əlavə etmək yaxşı olar
            if (pet.ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + pet.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "img/pets", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await pet.ImageFile.CopyToAsync(stream);
                }
                pet.ImageUrl = fileName;
            }

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // --- REDAKTƏ (GET) ---
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return NotFound();
            return View(pet);
        }

        // --- REDAKTƏ (POST) ---
        [HttpPost]
        public async Task<IActionResult> Update(Pet pet)
        {
            var dbPet = await _context.Pets.FindAsync(pet.Id);
            if (dbPet == null) return NotFound();

            if (pet.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(dbPet.ImageUrl))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "img/pets", dbPet.ImageUrl);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + pet.ImageFile.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "img/pets", fileName);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await pet.ImageFile.CopyToAsync(stream);
                }
                dbPet.ImageUrl = fileName;
            }

            dbPet.Name = pet.Name;
            dbPet.Breed = pet.Breed;
            dbPet.Age = pet.Age;
            dbPet.Energy = pet.Energy;
            dbPet.City = pet.City;
            dbPet.Description = pet.Description;
            dbPet.IsKidsFriendly = pet.IsKidsFriendly;
            dbPet.IsAdopted = pet.IsAdopted;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // --- SİLMƏK ---
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return NotFound();

            if (!string.IsNullOrEmpty(pet.ImageUrl))
            {
                string path = Path.Combine(_env.WebRootPath, "img/pets", pet.ImageUrl);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}