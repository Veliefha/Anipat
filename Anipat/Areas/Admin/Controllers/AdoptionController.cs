using Anipat.DAL;
using Anipat.Models;
using Anipat.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdoptionController : Controller
    {
        private readonly AppDbContext _context;

        public AdoptionController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _context.AdoptionRequests
                .Include(r => r.Pet)
                .Include(r => r.AppUser)
                .ToListAsync();

            if (requests == null)
            {
                requests = new List<AdoptionRequest>();
            }

            return View(requests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, AdoptionStatus newStatus)
        {
            var request = await _context.AdoptionRequests
                .Include(r => r.Pet)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (request == null) return NotFound();

            // Əgər müraciət təsdiqlənirsə (Approved)
            if (newStatus == AdoptionStatus.Approved && request.Status != AdoptionStatus.Approved)
            {
                // 1. Heyvanı sahiblənmiş kimi qeyd et
                if (request.Pet != null)
                {
                    request.Pet.IsAdopted = true;
                }

                // 2. STATİSTİKA SAYINI ARTIR
                // Bazadakı Statistics cədvəlindən müvafiq başlığı tapırıq
                var stats = await _context.Statistics
                    .FirstOrDefaultAsync(s => s.Title == "Sahibləndirilən Heyvanlar");

                if (stats != null)
                {
                    stats.Count += 1; // Rəqəmi 1 vahid artırırıq
                }

                // 3. Eyni heyvana gələn digər gözləyən müraciətləri rədd et
                var otherRequests = await _context.AdoptionRequests
                    .Where(r => r.PetId == request.PetId && r.Id != id && r.Status == AdoptionStatus.Pending)
                    .ToListAsync();

                foreach (var other in otherRequests)
                {
                    other.Status = AdoptionStatus.Rejected;
                }
            }

            request.Status = newStatus;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}