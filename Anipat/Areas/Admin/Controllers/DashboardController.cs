using Anipat.DAL; // AppDbContext-in olduğu qovluğu bura əlavə et
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Dashboard-a yalnız admin girə bilsin
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        // Bura Constructor-dır, bazanı bura "inject" edirik
        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Müraciətləri bazadan çəkirik
            var requests = await _context.AdoptionRequests
                .Include(r => r.Pet)
                .Include(r => r.AppUser)
                .OrderByDescending(r => r.RequestDate)
                .Take(10)
                .ToListAsync();

            return View(requests);
        }
    }
}