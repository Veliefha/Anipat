using Anipat.DAL;
using Anipat.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.AppUser)
                .OrderByDescending(a => a.CreatedDate)
                .ToListAsync();
            return View(appointments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, AdoptionStatus status)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            // ƏSAS MƏNTİQ BURADADIR:
            if (status == AdoptionStatus.Approved)
            {
                // Eyni gündə və eyni saatda olan digər "Gözləyən" (Pending) müraciətləri tapırıq
                var othersOnSameSlot = await _context.Appointments
                    .Where(a => a.Id != id &&
                           a.AppointmentDate.Date == appointment.AppointmentDate.Date &&
                           a.TimeSlot == appointment.TimeSlot &&
                           a.Status == AdoptionStatus.Pending)
                    .ToListAsync();

                // Onları avtomatik Rədd (Rejected) edirik
                foreach (var other in othersOnSameSlot)
                {
                    other.Status = AdoptionStatus.Rejected;
                }
            }

            appointment.Status = status;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}