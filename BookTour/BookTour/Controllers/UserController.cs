using BookTour.Application.Dto;
using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Đảm bảo dùng Dependency Injection cho IUserService
        private readonly IUserService _userService;

        // Constructor để Inject IUserService vào controller
        public UserController(IUserService userService)
        {
            _userService = userService;  // Sửa cách gán tham chiếu service đúng
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO request)
        {
            UserDTO result;
            try
            {
                result = await _userService.Login(request);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<UserDTO>
                {
                    code = 1001, 
                    message = ex.Message,  
                    result = null 
                };
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<UserDTO>
            {
                code = 1000, 
                message = "Login successful",  
                result = result  
            };

            // Trả về response dưới dạng OK (HTTP 200)
            return Ok(response);
        }
    }
}
