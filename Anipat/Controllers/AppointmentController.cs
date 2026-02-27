using Anipat.DAL;
using Anipat.Models;
using Anipat.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Controllers
{
    [Authorize] // Yalnız giriş edənlər randevu ala bilər
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AppointmentController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Randevu səhifəsini göstərir
        [HttpGet]
        public IActionResult Book()
        {
            return View();
        }

        // Təqvim üçün dolu tarixləri JSON formatında qaytarır
        [HttpGet]
        public async Task<IActionResult> GetBusySlots()
        {
            var busyAppointments = await _context.Appointments
                .Where(a => a.Status == AdoptionStatus.Approved)
                .Select(a => new {
                    title = "Dolu",
                    start = a.AppointmentDate.ToString("yyyy-MM-dd"),
                    allDay = true,
                    display = "background",
                    color = "#ff4444"
                })
                .ToListAsync();

            return Json(busyAppointments);
        }

        // Randevu müraciətini qeyd edir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(DateTime date, string slot, string subject)
        {
            var userId = _userManager.GetUserId(User);

            // Eyni tarixdə və eyni saatda təsdiqlənmiş randevu varmı yoxla
            bool isBusy = await _context.Appointments.AnyAsync(a =>
                a.AppointmentDate.Date == date.Date &&
                a.TimeSlot == slot &&
                a.Status == AdoptionStatus.Approved);

            if (isBusy)
            {
                TempData["Error"] = "Təəssüf ki, bu saat artıq rezerv edilib.";
                return RedirectToAction(nameof(Book));
            }
            if (date.Date < DateTime.Now.Date)
            {
                TempData["Error"] = "Keçmiş tarixə randevu almaq mümkün deyil.";
                return RedirectToAction(nameof(Book));
            }

            var appointment = new Appointment
            {
                AppUserId = userId,
                AppointmentDate = date,
                TimeSlot = slot,
                Subject = subject,
                Status = AdoptionStatus.Pending,
                CreatedDate = DateTime.Now
            };

            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Randevu müraciətiniz uğurla göndərildi.";
            return RedirectToAction("Index", "Home");
        }

public async Task<IActionResult> MyAppointments()
    {
        // Giriş edən istifadəçinin ID-sini alırıq
        var userId = _userManager.GetUserId(User);

        // Yalnız həmin istifadəçiyə aid randevuları gətiririk
        var myRequests = await _context.Appointments
            .Where(a => a.AppUserId == userId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

        return View(myRequests);
    }
}
}