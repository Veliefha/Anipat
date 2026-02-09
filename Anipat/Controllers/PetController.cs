
using anipat.Models;
using Anipat.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace anipat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly AppDbContext _context;
        public PetController(AppDbContext context) { _context = context; }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Pets.Where(p => p.IsActive && !p.IsAdopted).ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pet pet)
        {
            if (pet == null) return BadRequest("Məlumat boşdur");
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();
            return Ok(pet);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return NotFound();
            pet.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, Pet pet)
        {
            if (id != pet.Id)
            {
                return BadRequest("ID uyğun gəlmir");
            }

            _context.Entry(pet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pets.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}