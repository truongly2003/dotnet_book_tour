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
        private readonly ICustomerService _customerService;
        public UserController(IUserService userService,ICustomerService customerService)
        {
            _userService = userService;  
            _customerService = customerService;
        }
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



        // phần của trưởng
        [HttpGet("profile/user")]
        public async Task<IActionResult> GetProfileByUserId(int userId)
        {
            var data=await _customerService.FindCustomerByUserIdAsync(userId);
            return Ok(data);
        }
        [HttpPut("profile/user/update")]
        public async Task<IActionResult> UpdateCustomer(int userId, [FromBody] CustomerDTO customerDTO)
        {

            var data = await _customerService.UpdateCustomerByUserIdAsync(userId, customerDTO);
            if (data)
            {
                return Ok("Customer updated successfully");
            }
            else
            {
                return NotFound("Customer not found");
            }
        }
    }
}
