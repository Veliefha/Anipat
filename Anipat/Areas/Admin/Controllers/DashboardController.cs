using Anipat.DAL;
using Anipat.Models.ViewModels;
using Anipat.Models.Enums; // AdoptionStatus-un yerləşdiyi namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 1. Yeni ViewModel obyekti yaradırıq
            var viewModel = new AdminDashboardVM();

            // 2. Bugünün Sahibləndirmə müraciətlərini sayırıq (LINQ Count + Date Filter)
            viewModel.TodayAdoptionsCount = await _context.AdoptionRequests
                .CountAsync(r => r.RequestDate.Date == DateTime.Today);

            // 3. Sistemdəki cəmi heyvan sayını tapırıq (LINQ Count)
            viewModel.TotalPetsCount = await _context.Pets.CountAsync();

            // 4. Gözləyən (Pending) randevuların sayını tapırıq (LINQ Count + Status Filter)
            viewModel.PendingAppointmentsCount = await _context.Appointments
                .CountAsync(a => a.Status == AdoptionStatus.Pending);

            // 5. Son 5 müraciəti bazadan çəkirik (OrderByDescending + Take + Include)
            viewModel.LatestRequests = await _context.AdoptionRequests
                .Include(r => r.Pet)
                .Include(r => r.AppUser)
                .OrderByDescending(r => r.RequestDate) // Ən son gələn yuxarıda
                .Take(5) // Yalnız son 5 ədəd
                .ToListAsync();

            // 6. Hazır modeli View-ya göndəririk
            return View(viewModel);
        }
    }
}