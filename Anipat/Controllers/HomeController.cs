using Microsoft.AspNetCore.Mvc;

namespace Anipat.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
