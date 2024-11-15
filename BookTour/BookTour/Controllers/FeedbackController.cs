using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using BookTour.Application.Interface;
using BookTour.Application.Service;
using BookTour.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        // Constructor để Inject IUserService vào controller
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService; 
        }


        [HttpGet]
        public async Task<IActionResult> getListFeedback(int page, int size, int detailRouteId)
        {
            ApiResponse<Page<FeedbackDTO>> response;
            Page<FeedbackDTO> result;

            try
            {
                result = await _feedbackService.getListFeedbackAsync(page, size, detailRouteId);
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Page<FeedbackDTO>>
                {
                    code = 1001,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }

            response = new ApiResponse<Page<FeedbackDTO>>
            {
                code = 1000,
                message = "Feedback list fetched successfully",
                result = result
            };

            return Ok(response);
        }

        [HttpPost("comment")]
        public async Task<IActionResult> createComment([FromBody] FeedbackRequest request)
        {
            FeedbackDTO result;
            try
            {
                Console.WriteLine("test");

                result = await _feedbackService.comment(request);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<FeedbackDTO>
                {
                    code = 1001,
                    message = ex.Message,
                };
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<FeedbackDTO>
            {
                code = 1000,
                message = "Feedback Create Successful",
                result = result
            };

            return Ok(response);
        }

    }
}
