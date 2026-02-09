using Microsoft.AspNetCore.Mvc;
using Anipat.DAL;
using Anipat.Models;
using System.Linq;

namespace Anipat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/team
        [HttpGet]
        public IActionResult GetAll()
        {
            var team = _context.Teams.ToList();
            return Ok(team);
        }

        // POST: api/team
        [HttpPost]
        public IActionResult Create(Team member)
        {
            _context.Teams.Add(member);
            _context.SaveChanges();
            return Ok(member);
        }

        // PUT: api/team/{id} - Redaktə etmək üçün
        [HttpPut("{id}")]
        public IActionResult Update(int id, Team updatedMember)
        {
            var member = _context.Teams.Find(id);
            if (member == null) return NotFound("Üzv tapılmadı");

            // Bazadakı məlumatları yeniləyirik
            member.Name = updatedMember.Name;
            member.Position = updatedMember.Position;
            member.Image = updatedMember.Image;

            _context.SaveChanges();
            return Ok(member);
        }

        // DELETE: api/team/{id} - Silmək üçün
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var member = _context.Teams.Find(id);
            if (member == null) return NotFound("Üzv tapılmadı");

            _context.Teams.Remove(member);
            _context.SaveChanges();
            return Ok();
        }
    }
}