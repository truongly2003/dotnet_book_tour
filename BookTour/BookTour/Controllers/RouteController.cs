using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }
        [HttpGet]
        public async Task<IActionResult> getAllRoute(int page, int size, string sort)
        {
            var route = await _routeService.GetAllRouteAsync(page, size, sort);
            return Ok(route);
        }
        [HttpGet("search/arrivalName")]
        public async Task<IActionResult> getAllRouteByArrivalName(string ArrivalName,int page, int size, string sort)
        {
            var route = await _routeService.GetAllRouteByArrivalName(ArrivalName, page, size, sort);
            return Ok(route);
        }
    }
}
