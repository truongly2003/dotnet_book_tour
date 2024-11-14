using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Application.Service;
using BookTour.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        // Constructor để Inject IUserService vào controller
        public UserController(IUserService userService)
        {
            _userService = userService;  // Sửa cách gán tham chiếu service đúng
        }


        //
        [HttpGet]
        public async Task<IActionResult> getALlUser(int page, int size)
        {
            var users = await _userService.GetAllUserAsync(page, size);
            return Ok(users);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            LoginDTO result;
            try
            {
                result = await _userService.Login(request);
            }
            catch (Exception ex)
            {
                var errorResponse = new ApiResponse<LoginDTO>
                {
                    code = 1001,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<LoginDTO>
            {
                code = 1000,
                message = "Login successful",
                result = result
            };

            Console.WriteLine($"code: {response.code}, message: {response.message}, result: {response.result}");


            return Ok(response);
        }



        [HttpPost("createUser")]
        public async Task<IActionResult> createUser([FromBody] UserCreateRequest request)
        {
            Console.WriteLine("create user");
            User result;
            try
            {
                Console.WriteLine("test");

                result = await _userService.AddUser(request);
            }catch(Exception ex)
            {
                var errorResponse = new ApiResponse<UserDTO>
                {
                    code = 1001,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(errorResponse);
            }

            var response = new ApiResponse<User>
            {
                code = 1000,
                message = "User Create Successful",
                result = result
            };

            return Ok(response);
        }

    }
}
