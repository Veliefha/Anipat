using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anipat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var service = _context.Services.ToList();
            return Ok(service);
        }
        [HttpPost]
        public IActionResult Create(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return Ok(service);
        }
    }
}
