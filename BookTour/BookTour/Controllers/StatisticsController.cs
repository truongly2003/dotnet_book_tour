using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess;
using System.Threading.Tasks;
using System.Collections.Generic;
using static BookStore.DataAccess.BookTourDbContext;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly BookTourDbContext _context;

        public StatisticsController(BookTourDbContext context)
        {
            _context = context;
        }

        [HttpGet("booking-status")]
        public async Task<IActionResult> GetBookingStatisticsByPaymentStatus()
        {
            List<BookingPaymentStatusStatistics> statistics;
            try
            {
                statistics = await _context.GetBookingStatisticsByPaymentStatusAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }

  
        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenueStatistics()
        {
            List<MonthlyRevenueStatistics> statistics;
            try
            {
                statistics = await _context.GetMonthlyRevenueStatisticsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }

        [HttpGet("popular-tours")]
        public async Task<IActionResult> GetPopularTourStatistics()
        {
            List<PopularTourStatistics> statistics;
            try
            {
                statistics = await _context.GetPopularTourStatisticsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }

        [HttpGet("customer-bookings")]
        public async Task<IActionResult> GetCustomerBookingStatistics()
        {
            List<CustomerBookingStatistics> statistics;
            try
            {
                statistics = await _context.GetCustomerBookingStatisticsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }

        [HttpGet("tour-ratings")]
        public async Task<IActionResult> GetTourRatingStatistics()
        {
            List<TourRatingStatistics> statistics;
            try
            {
                statistics = await _context.GetTourRatingStatisticsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }

        [HttpGet("route-tours")]
        public async Task<IActionResult> GetRouteTourStatistics()
        {
            List<RouteTourStatistics> statistics;
            try
            {
                statistics = await _context.GetRouteTourStatisticsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }

        [HttpGet("passenger-types")]
        public async Task<IActionResult> GetPassengerTypeStatistics()
        {
            List<PassengerTypeStatistics> statistics;
            try
            {
                statistics = await _context.GetPassengerTypeStatisticsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }

        [HttpGet("passenger-age-groups")]
        public async Task<IActionResult> GetPassengerAgeGroupStatistics()
        {
            List<PassengerAgeGroupStatistics> statistics;
            try
            {
                statistics = await _context.GetPassengerAgeGroupStatisticsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }
    }
}