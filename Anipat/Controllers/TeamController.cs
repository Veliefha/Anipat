using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public IActionResult GetAll()
        {
            var team=_context.Teams.ToList();
            return Ok(team);
        }
        [HttpPost]
        public IActionResult Create(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return Ok(team);
        }
    }
}
