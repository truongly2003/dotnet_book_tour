using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
