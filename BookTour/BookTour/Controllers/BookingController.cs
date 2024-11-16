using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet("profile/user")]
        public async Task<IActionResult> GetBookingCustomerId(int UserId, int page,int size)
        {
            var booking = await _bookingService.GetAllBookingByUserIdAsync(UserId, page,size);
            return Ok(booking);
        }
        [HttpGet("profile/detail")]
        public async Task<IActionResult> GetBookingDetailByCustomerId(int UserId,int BookingId)
        {
            var booking=await _bookingService.GetDetailBookingResponseByUserIdAsync(UserId,BookingId);
            return Ok(booking);
        }
    }
}
