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
        public async Task<IActionResult> getListUser(int page, int size)
        {
            ApiResponse<Page<UserDTO>> response;
            Page<UserDTO> result;

            try
            {
                result = await _userService.GetAllUserAsync(page, size);
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Page<UserDTO>>
                {
                    code = 1001,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }

            response = new ApiResponse<Page<UserDTO>>
            {
                code = 1000,
                message = "User list fetched successfully",
                result = result
            };

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
