using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Anipat.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactMessage model)
        {
            // Ehtiyat tədbiri: Əgər istifadəçi giriş edibsə, 
            // formaya nə yazmasından asılı olmayaraq onun rəsmi emailini götürürük.
            if (User.Identity.IsAuthenticated)
            {
                var officialEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity.Name;
                model.Email = officialEmail;
            }

            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                model.CreatedDate = DateTime.Now;
                model.IsRead = false;
                model.AdminReply = null;

                _context.ContactMessages.Add(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Mesajınız bizə çatdı! 🐾";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Texniki xəta baş verdi.");
                return View("Index", model);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyMessages()
        {
            // Email-i həm Claim-dən, həm Name-dən yoxlayırıq (Identity-nin fərqli konfiqurasiyaları üçün)
            var userEmail = User.FindFirstValue(ClaimTypes.Email) ?? User.Identity.Name;

            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account", new { area = "Admin" });
            }

            // ToLower() istifadə edirik ki, böyük-kiçik hərf fərqi filtrə mane olmasın
            var myMsgs = await _context.ContactMessages
                .Where(m => m.Email.ToLower() == userEmail.ToLower())
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();

            return View(myMsgs);
        }
    }
}