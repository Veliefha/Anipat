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

        public async Task<IActionResult> Index()
        {
            var pets = await _context.Pets.OrderByDescending(p => p.CreatedAt).ToListAsync();
            return View("/Views/Pet/Index.cshtml", pets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("/Views/Pet/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pet pet)
        {
            // 1. Şəkil Yükləmə (Null olmasın deyə)
            if (pet.ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + pet.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "img/pets", fileName);

                string directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await pet.ImageFile.CopyToAsync(stream);
                }
                pet.ImageUrl = fileName;
            }
            else
            {
                pet.ImageUrl = "default.jpg";
            }

            pet.CreatedAt = DateTime.Now;

            // Koordinat sətirlərini SİLDİK
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return NotFound();
            return View("/Views/Pet/Update.cshtml", pet);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Pet pet)
        {
            var dbPet = await _context.Pets.FindAsync(pet.Id);
            if (dbPet == null) return NotFound();

            if (pet.ImageFile != null)
            {
                if (!string.IsNullOrEmpty(dbPet.ImageUrl) && dbPet.ImageUrl != "default.jpg")
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

            // Məlumatları yeniləyirik (Koordinatları çıxdıq)
            dbPet.Name = pet.Name;
            dbPet.Species = pet.Species;
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

        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                if (!string.IsNullOrEmpty(pet.ImageUrl) && pet.ImageUrl != "default.jpg")
                {
                    string path = Path.Combine(_env.WebRootPath, "img/pets", pet.ImageUrl);
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                }
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}