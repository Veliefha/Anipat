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

        public async Task<IActionResult> FilterByDistance(double userLat, double userLong, string returnUrl, double radius = 50)
        {
            var allPets = await _context.Pets.Where(p => !p.IsAdopted).ToListAsync();

            var filteredPets = allPets.Select(pet => {
                pet.Distance = Math.Round(CalculateDistance(userLat, userLong, pet.Latitude, pet.Longitude), 1);
                return pet;
            })
            .Where(pet => pet.Distance <= radius && pet.Latitude != 0)
            .OrderBy(pet => pet.Distance)
            .ToList();

            // ƏGƏR PET SEHIFESINDEN GELIBSE
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl.ToLower().Contains("/pet"))
            {
                return View("Index", filteredPets);
            }

            // ANA SEHIFE UCUN
            var viewModel = new HomeVM
            {
                LatestPets = filteredPets.Any() ? filteredPets : allPets.OrderByDescending(x => x.Id).Take(6).ToList(),
                Services = await _context.Services.ToListAsync(),
                Statistics = await _context.Statistics.ToListAsync(),
                TeamMembers = await _context.Teams.ToListAsync()
            };

            return View("/Views/Home/Index.cshtml", viewModel);
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371;
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