using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalController : ControllerBase
    {
        private readonly IArrivalService _arrivalService;
        public ArrivalController(IArrivalService arrivalService)
        {
            _arrivalService = arrivalService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllArrival() { 
            var arrival= await _arrivalService.getAllArrivalAsync();
            return Ok(arrival);
        }
    }
}
