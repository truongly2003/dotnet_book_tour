using Microsoft.AspNetCore.Mvc;
using BookStore.DataAccess;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            Dictionary<string, int> statistics;
            try
            {
                statistics = await _context.GetStatisticsAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 1001, message = ex.Message });
            }

            return Ok(new { code = 1000, result = statistics });
        }
    }
}