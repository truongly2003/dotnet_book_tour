using BookTour.Application.Interface;
using BookTour.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartureController : ControllerBase
    {
        private readonly IDepartureService _departureService;
        public DepartureController(IDepartureService departureService)
        {
            _departureService = departureService;
        }
        [HttpGet()]
        public async Task<IActionResult> getAllDeparture()
        {
            var departure = await _departureService.getAllDepartureAsync();

            return Ok(departure);
        }
    }
}
