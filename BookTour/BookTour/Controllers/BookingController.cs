using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
