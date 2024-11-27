using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailRouteController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("lol tuấn 57");
        }
    }
}
