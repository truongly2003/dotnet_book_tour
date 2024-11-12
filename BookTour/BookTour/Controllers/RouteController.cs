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
        [HttpGet("testpage")]
        public async Task<IActionResult> getAllTestPage(int page, int size, string sort="asc")
        {
            var arrival = await _routeService.getAllRouteAsyncTestPage(page, size, sort);
            return Ok(arrival);
        }
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var a = await _routeService.getAllRoute();
          return Ok(a);
        }
    }
}
