using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anipat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {

            var feedback = _context.Feedbacks.ToList();
            return Ok(feedback);
        }
        [HttpPost]

        public IActionResult Create(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return Ok(feedback);
        }
    } 
}
