using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
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

        [HttpPost("handle-booking")]
        public async Task<IActionResult> HandleBooking(BookingRequest request)
        {
            var customerId = await _bookingService.CreateCustomerAsync(request);
            var passengerCount = request.passengerRequestList.Count;
            var passengerIds = await _bookingService.CreatePassengersAsync(request);
            var bookingId = await _bookingService.CreateBookingAsync(request, customerId, passengerCount);
            var status = await _bookingService.CreateTicketsAsync(passengerIds, bookingId);

            if (status)
            {
                return Ok(new ApiResponse<int>
                {
                    code = 200,
                    message = "Booking successfully",
                    result = bookingId
                });
            }
            else
            {
                return BadRequest(new ApiResponse<bool>
                {
                    code = 200,
                    message = "Booking failed",
                    result = status
                });
            }
        }

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
        public async Task<IActionResult> GetBookingCustomerId(int UserId, int page, int size)
        {
            var booking = await _bookingService.GetAllBookingByUserIdAsync(UserId, page, size);
            return Ok(booking);
        }

        [HttpGet("profile/detail")]
        public async Task<IActionResult> GetBookingDetailByCustomerId(int UserId, int BookingId)
        {
            var booking = await _bookingService.GetDetailBookingResponseByUserIdAsync(UserId, BookingId);
            return Ok(booking);
        }

        [HttpGet("cancel-tour")]
        public async Task<IActionResult> CancelTour(int bookingId, int statusId)
        {
            var success = await _bookingService.UpdateBookingStatusAsync(bookingId, statusId);
            if (success)
            {
                return Ok(new ApiResponse<object>
                {
                    code = 200,
                    message = "Status updated successfully",
                    result = success
                });
            }
            else
            {
                return BadRequest(new ApiResponse<object>
                {
                    code = 400,
                    message = "Failed to update status",
                    result = "Error"
                });
            }
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
            _logger.LogInformation($"Getting booking details for ID: {bookingId}");

            var bookingDetail = await _bookingService.GetBookingDetailByIdAsync(bookingId);
            if (bookingDetail == null)
            {
                _logger.LogWarning($"Booking not found for ID: {bookingId}");
                return NotFound(new ApiResponse<string>
                {
                    code = 404,
                    message = "Booking not found",
                    result = null
                });
            }

            return Ok(new ApiResponse<BookingDetailResponse>
            {
                code = 200,
                message = "Success",
                result = bookingDetail
            });
        }

    }
}
