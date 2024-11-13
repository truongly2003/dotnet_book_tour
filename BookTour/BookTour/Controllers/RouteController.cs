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
            var arrival = await _routeService.GetAllRouteAsync(page, size, sort);
            return Ok(arrival);
        }
        //[HttpGet("test")]
        //public async Task<IActionResult> Test()
        //{
        //    var a = await _routeService.getAllRoute();
        //  return Ok(a);
        //}
    }
}
