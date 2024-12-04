using BookTour.Application.Interface;
using BookTour.Application.Service;
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
        public async Task<IActionResult> GetAllRoute(int page, int size, string sort)
        {
            var route = await _routeService.GetAllRouteAsync(page, size, sort);
            return Ok(route);
        }
        [HttpGet("search/arrivalName")]
        public async Task<IActionResult> GetAllRouteByArrivalName(string ArrivalName, int page, int size, string sort)
        {
            var route = await _routeService.GetAllRouteByArrivalName(ArrivalName, page, size, sort);
            return Ok(route);
        }
        [HttpGet("search/body")]
        public async Task<IActionResult> GetAllRouteByArrivalAndDepartureAndDate(string ArrivalName, string DepartureName, DateOnly TimeToDeparture, int page, int size, string sort)
        {
            var route = await _routeService.GetAllRouteByArrivalAndDepartureAndDateAsync(ArrivalName, DepartureName, TimeToDeparture, page, size, sort);
            return Ok(route);
        }
        [HttpGet("detail/{DetailRouteId}")]
        public async Task<IActionResult> GetDetailRouteById(int DetailRouteId)
        {
            var detailRoute = await _routeService.GetDetailRouteByIdAsync(DetailRouteId);
            if (detailRoute == null)
            {
                return NotFound(new { Message = "Detail Route not found." });
            }
            return Ok(detailRoute);
        }
        [HttpGet("getRoute")]
        public async Task<IActionResult> GetRouteRoad()
        {
            var route = await _routeService.GetRoadResponsesAsync();
            return Ok(route);
        }

    }
}
