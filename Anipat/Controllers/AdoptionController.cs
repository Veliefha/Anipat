using Anipat.DAL; // Sənin AppDbContext bu qovluqdadırsa bunu saxla
using Anipat.Models;
using Anipat.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdoptionController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> MyRequests()
        {
            // 1. Kimin giriş etdiyini bilməliyik
            var userId = _userManager.GetUserId(User);

            // 2. Bazadan yalnız həmin adamın müraciətlərini çəkirik
            var myRequests = await _context.AdoptionRequests
                .Include(r => r.Pet) // Heyvanın adını görmək üçün
                .Where(r => r.AppUserId == userId)
                .OrderByDescending(r => r.RequestDate)
                .ToListAsync();

            // 3. Bu məlumatları səhifəyə göndəririk
            return View(myRequests);
        }
        [HttpPost]
        public async Task<IActionResult> SendRequest(int petId)
        {
            // Giriş edən istifadəçinin ID-sini götürürük
            var userId = _userManager.GetUserId(User);

            if (userId == null) return RedirectToAction("Login", "Account", new { area = "Admin" });

            // Eyni heyvana təkrar müraciət edib-etmədiyini yoxlayırıq
            bool alreadyApplied = await _context.AdoptionRequests
                .AnyAsync(r => r.PetId == petId && r.AppUserId == userId);

            if (alreadyApplied)
            {
                // İstifadəçiyə mesaj göndərmək üçün TempData istifadə edirik
                TempData["Error"] = "Siz artıq bu heyvan üçün müraciət etmisiniz!";
                return RedirectToAction("Details", "Pet", new { id = petId });
            }

            // Yeni müraciəti yaradırıq
            var request = new AdoptionRequest
            {
                PetId = petId,
                AppUserId = userId,
                Status = AdoptionStatus.Pending, // İlk status: Gözləmədə
                RequestDate = DateTime.Now,
                Message = "Mən bu heyvanı sahiblənmək istəyirəm."
            };

            _context.AdoptionRequests.Add(request);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Müraciətiniz uğurla göndərildi! Admin təsdiqini gözləyin.";
            return RedirectToAction("Details", "Pet", new { id = petId });
        }
    }
}