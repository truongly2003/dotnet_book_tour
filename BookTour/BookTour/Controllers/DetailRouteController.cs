using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailRouteController : ControllerBase

    {

        private readonly IDetailRouteService _detailRouteService;

        public DetailRouteController(IDetailRouteService detailRouteService)
        {
            _detailRouteService = detailRouteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetailRouteDTO>>> GetAllDetailRoutes(int page, int size)
        {
            var detailRoutes = await _detailRouteService.GetAllDetailRouteAsync(page, size);
            return Ok(detailRoutes);
        }

        // GET: api/DetailRoute/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DetailRouteDTO>> GetDetailRouteById(int id)
        {
            var detailRoute = await _detailRouteService.GetDetailRouteByIdAsync(id);

            if (detailRoute == null)
            {
                return NotFound();
            }

            return Ok(detailRoute);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> CreateDetailRoute([FromBody] DetailRouteRequest detailRouteRequest)
        {

            try
            {
                // Call the repository to insert the new detail route
                var result = await _detailRouteService.InsertAsync(detailRouteRequest);

                if (result)
                {
                    return Ok(new { message = "Detail route created successfully" });
                }
                else
                {
                    return StatusCode(500, "An error occurred while creating the detail route.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (this is just a simple log)
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDetailRoute([FromRoute] int id, [FromBody] DetailRouteRequest detailRouteRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Kiểm tra tính hợp lệ của dữ liệu
            }
            try
            {
                var result = await _detailRouteService.UpdateAsync(id, detailRouteRequest);
                if (result)
                {
                    return Ok(new { message = "Detail route updated successfully" });
                }
                else
                {
                    return StatusCode(500, "An error occurred while updating the detail route.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error." });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDetailRoute([FromRoute] int id)
        {
            var result = await _detailRouteService.DeleteAsync(id);
            if (result)
            {
                return Ok(new { message = "Detail route delete successfully" });
            }
            else
            {
                return StatusCode(500, "An error occurred while deleting the detail route.");
            }
        }
    }
}
