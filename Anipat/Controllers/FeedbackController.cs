using Anipat.DAL;
using Anipat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        // GET: api/feedback
        [HttpGet]
        public IActionResult GetAll()
        {
            var feedbacks = _context.Feedbacks.ToList(); // Service → Feedbacks
            return Ok(feedbacks);
        }

        // POST: api/feedback
        [HttpPost]
        public IActionResult Create(Feedback feedback)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Feedbacks.Add(feedback); // Service → Feedbacks
            _context.SaveChanges();
            return Ok(feedback);
        }
    }
}
