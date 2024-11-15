﻿using BookTour.Application.Interface;
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
    }
}
