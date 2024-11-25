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
        [HttpGet("profile/customer")]
        public async Task<IActionResult> GetBookingCustomerId(int CustomerId,int page,int size)
        {
            var booking = await _bookingService.GetAllBookingByCustomerIdAsync(CustomerId,page,size);
            return Ok(booking);
        }
        [HttpGet("profile/detail")]
        public async Task<IActionResult> GetBookingDetailByCustomerId(int CustomerId)
        {
            var booking=await _bookingService.GetDetailBookingResponseByCustomerIdAsync(CustomerId);
            return Ok(booking);
        }
        [HttpGet("export")]
        public async Task<IActionResult> ExportBookings()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }
        [HttpGet("detail/{bookingId}")]
        public async Task<IActionResult> GetBookingDetailById(int bookingId)
        {
            var bookingDetail = await _bookingService.GetBookingDetailByIdAsync(bookingId);
            if (bookingDetail == null)
            {
                return NotFound();
            }
            return Ok(bookingDetail);
        }
    }
}
