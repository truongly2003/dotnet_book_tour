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
        //[HttpGet]
        //public async Task<IActionResult> Index(int page = 1, int size = 10, string sort = "")
        //{
        //    var book = await _routeService.getAllRouteAsync(page, size, sort);
        //    return Ok(book);
        //}
        //[HttpGet("arrival")]
        //public async Task<IActionResult> getAllArrival()
        //{
        //    var arrival = await _routeService.getAllArrivalAsync();

        //    return Ok(arrival);
        //}
        //[HttpGet("departure")]
        //public async Task<IActionResult> getAllDeparture()
        //{
        //    var arrival = await _routeService.getAllDeparture();

        //    return Ok(arrival);
        //}
    }
}
