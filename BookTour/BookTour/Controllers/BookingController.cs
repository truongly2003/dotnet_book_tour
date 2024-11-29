using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;
        
        public BookingController(IRouteService routeService, IBookingService bookingService, ILogger<BookingController> logger)
        {
            _routeService = routeService;
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailRoute(int id)
        {
            var result = await _routeService.GetDetailRouteByIdAsync(id);
            return Ok(new ApiResponse<DetailRouteResponse>
            {
                code = 200,
                message = "successfully",
                result = result
            });
        }

        // [HttpPost("handle-booking")]
        // public async Task<IActionResult> HandleBooking([FromBody] BookingRequest request)
        // {
        //     // Call service to handle booking
        //     var customerId = await _bookingService.CreateCustomerAsync(request);
        //     var passengerIds = await _bookingService.CreatePassengersAsync(request);
        //     var bookingId = await _bookingService.CreateBookingAsync(request, customerId);
        //     var status = await _bookingService.CreateTicketsAsync(passengerIds, bookingId);
        //
        //     if (status)
        //     {
        //         return Ok(new ApiResponse<BookingResponse>
        //         {
        //             Message = "Booking successful",
        //             Result = new BookingResponse() // You may return additional data here
        //         });
        //     }
        //     else
        //     {
        //         return BadRequest(new ApiResponse<BookingResponse>
        //         {
        //             Message = "Booking failed",
        //             Result = null
        //         });
        //     }
        // }
        
        [HttpPut("update-status")]
        public async Task<IActionResult> UpdateBookingStatus(int bookingId, int statusId)
        {
            var success = await _bookingService.UpdateBookingStatusAsync(bookingId, statusId);
            if (success)
            {
                return Ok(new ApiResponse<bool>
                {
                    code = 200,
                    message = "Status updated successfully",
                    result = success
                });
            }
            else
            {
                return BadRequest(new ApiResponse<string>
                {
                    code = 200,
                    message = "Status updated successfully",
                    result = "Success"
                });
            }
        }
        
        [HttpGet("check-available-quantity")]
        public async Task<IActionResult> CheckAvailability(int detailRouteId, int totalPassengers)
        {
            var available = await _bookingService.CheckAvailableQuantityAsync(detailRouteId, totalPassengers);

            return Ok(available);
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
