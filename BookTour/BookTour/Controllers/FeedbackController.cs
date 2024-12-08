using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using BookTour.Application.Interface;
using BookTour.Application.Service;
using BookTour.Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/admin/feedback/")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        // Constructor để Inject IUserService vào controller
        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService; 
        }


        [HttpGet("client")]
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


        [HttpGet("admin")]
        public async Task<IActionResult> getAllFeedbackAdminAsync(int page, int size)
        {
            ApiResponse<Page<FeedbackDTO>> response;
            Page<FeedbackDTO> result;

            try
            {
                result = await _feedbackService.getListFeedbackAdminAsync(page, size);
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

        [HttpPost("client/comment")]
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


        [HttpGet("client/checkBooking")]
        public async Task<IActionResult> CheckBooking([FromQuery] int userId, [FromQuery] int detailRouteId)
        {
            try
            {
                // Gọi service để kiểm tra booking (giả sử service trả về Task<bool>)
                var result = await _feedbackService.CheckBooking(userId, detailRouteId);

                return Ok(new ApiResponse<bool>
                {
                    code = 200,
                    message = "Success",
                    result = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<bool>
                {
                    code = 400,
                    message = ex.Message,
                    result = false
                });
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> getListFeedbackByUsername(int page, int size, string detailRouteName)
        {
            ApiResponse<Page<FeedbackDTO>> response;
            Page<FeedbackDTO> result;

            try
            {
                result = await _feedbackService.getListUserByDetailRouteName(page, size, detailRouteName);
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
                message = "User list fetched successfully",
                result = result
            };

            return Ok(response);
        }
    }
}
