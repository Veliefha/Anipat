using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // ToListAsync və s. üçün lazımdır
using System.Linq;
using System.Threading.Tasks;

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
            return Ok(_context.Services.ToList());
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            _context.Services.Add(service);
            _context.SaveChanges();
            return Ok(service);
        }

        // --- BURA DİQQƏT: BU METODLAR SƏNDƏ YOX İDİ ---

        // UPDATE (Edit) metodu
        [HttpPut("{id}")]
        public IActionResult Update(int id, Service updatedService)
        {
            var service = _context.Services.Find(id);
            if (service == null) return NotFound("Xidmət tapılmadı");

            service.Name = updatedService.Name;
            service.Description = updatedService.Description;
            service.Icon = updatedService.Icon;

            _context.SaveChanges();
            return Ok(service);
        }

        // DELETE metodu
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var service = _context.Services.Find(id);
            if (service == null) return NotFound("Xidmət tapılmadı");

            _context.Services.Remove(service);
            _context.SaveChanges();
            return Ok();
        }
    }
}