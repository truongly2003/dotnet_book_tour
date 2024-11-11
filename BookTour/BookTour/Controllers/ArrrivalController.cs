using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrrivalController : ControllerBase
    {
        private readonly IArrivalService _arrivalService;
        public ArrrivalController(IArrivalService arrivalService)
        {
            _arrivalService = arrivalService;
        }   
    }
}
