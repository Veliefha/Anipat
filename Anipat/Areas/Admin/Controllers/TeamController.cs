using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Anipat.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // 1. Siyahı (Read)
        public IActionResult Index()
        {
            var members = _context.Teams.ToList();
            return View(members);
        }

        // 2. Yeni Üzv Yaratmaq (GET)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. Yeni Üzv Yaratmaq (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            if (team.ImageFile != null)
            {
                // Şəkil yükləmə məntiqi
                string fileName = Guid.NewGuid().ToString() + "_" + team.ImageFile.FileName;
                string path = Path.Combine(_env.WebRootPath, "img/team", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await team.ImageFile.CopyToAsync(stream);
                }
                team.ImageUrl = fileName;
            }

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 4. Redaktə Etmək (GET)
        [HttpGet]
        public IActionResult Update(int id)
        {
            var teamMember = _context.Teams.Find(id);
            if (teamMember == null) return NotFound();
            return View(teamMember);
        }

        // 5. Redaktə Etmək (POST)
        [HttpPost]
        public async Task<IActionResult> Update(Team team)
        {
            var existMember = await _context.Teams.FindAsync(team.Id);
            if (existMember == null) return NotFound();

            if (team.ImageFile != null)
            {
                // Köhnə şəkli qovluqdan silirik
                if (!string.IsNullOrEmpty(existMember.ImageUrl))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "img/team", existMember.ImageUrl);
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                // Yeni şəkli yükləyirik
                string fileName = Guid.NewGuid().ToString() + "_" + team.ImageFile.FileName;
                string newPath = Path.Combine(_env.WebRootPath, "img/team", fileName);
                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await team.ImageFile.CopyToAsync(stream);
                }
                existMember.ImageUrl = fileName;
            }

            existMember.FullName = team.FullName;
            existMember.Position = team.Position;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 6. Silmək
        public IActionResult Delete(int id)
        {
            var teamMember = _context.Teams.Find(id);
            if (teamMember == null) return NotFound();

            // Şəkli qovluqdan fiziki olaraq silirik
            if (!string.IsNullOrEmpty(teamMember.ImageUrl))
            {
                string path = Path.Combine(_env.WebRootPath, "img/team", teamMember.ImageUrl);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            }

            _context.Teams.Remove(teamMember);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}