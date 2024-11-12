using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
