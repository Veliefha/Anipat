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
            // Faylın yerini tam ünvanla göstəririk
            return View("/Views/Pet/Index.cshtml", pets);
        }

        // --- RADİUSA GÖRƏ FİLTER (TEST ÜÇÜN) ---
        public async Task<IActionResult> FilterByDistance(double userLat, double userLong, double radius = 15)
        {
            var allPets = await _context.Pets.ToListAsync();

            var filteredPets = allPets.Select(pet => {
                pet.Distance = CalculateDistance(userLat, userLong, pet.Latitude, pet.Longitude);
                return pet;
            })
            .Where(pet => pet.Distance <= radius)
            .OrderBy(pet => pet.Distance)
            .ToList();

            return View("/Views/Pet/Index.cshtml", filteredPets);
        }

        // --- YARATMAQ (GET) ---
        [HttpGet]
        public IActionResult Create()
        {
            // Xəta almamaq üçün ana Views qovluğuna yönləndiririk
            return View("/Views/Pet/Create.cshtml");
        }

        // --- YARATMAQ (POST) ---
        [HttpPost]
        public async Task<IActionResult> Create(Pet pet)
        {
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

            // Xəta almamaq üçün ana Views qovluğuna yönləndiririk
            return View("/Views/Pet/Update.cshtml", pet);
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
            dbPet.Latitude = pet.Latitude;
            dbPet.Longitude = pet.Longitude;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // --- SİLMƏK ---
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                if (!string.IsNullOrEmpty(pet.ImageUrl))
                {
                    string path = Path.Combine(_env.WebRootPath, "img/pets", pet.ImageUrl);
                    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
                }
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // --- KÖMƏKÇİ HESABLAMA METODLARI ---
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // km
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private double ToRadians(double angle) => (Math.PI / 180) * angle;
    }
}