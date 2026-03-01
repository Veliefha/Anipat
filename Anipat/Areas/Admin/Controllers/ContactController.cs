using Anipat.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ContactController : Controller
{
    private readonly AppDbContext _context;
    public ContactController(AppDbContext context) { _context = context; }

    // Mesajların siyahısı
    public async Task<IActionResult> Index()
    {
        var messages = await _context.ContactMessages
            .OrderByDescending(m => m.CreatedDate)
            .ToListAsync();
        return View(messages);
    }

    // Mesajı Oxundu kimi işarələmək (AJAX və ya Form ilə)
    [HttpPost]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var msg = await _context.ContactMessages.FindAsync(id);
        if (msg != null)
        {
            msg.IsRead = true;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // Admin Cavabı yazmaq
    [HttpPost]
    public async Task<IActionResult> Reply(int id, string replyText)
    {
        var msg = await _context.ContactMessages.FindAsync(id);
        if (msg != null)
        {
            msg.AdminReply = replyText;
            msg.IsRead = true; // Cavab yazılıbsa, deməli oxunub
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}