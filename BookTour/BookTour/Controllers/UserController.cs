using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using BookTour.Application.Interface;
using BookTour.Application.Service;
using BookTour.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookTour.Controllers
{
    [Route("api/admin/user")]
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




      


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request, [FromRoute] int id)
        {
            ApiResponse<UserDTO> response;
            UserDTO result;

            try
            {
                // Kiểm tra nếu yêu cầu có dữ liệu hợp lệ
                if (request == null)
                {
                    throw new ArgumentNullException(nameof(request), "Request body cannot be null");
                }

                result = await _userService.UpdateUserAsync(request, id);
            }
            catch (ArgumentNullException ex)
            {
                response = new ApiResponse<UserDTO>
                {
                    code = 1002, // Error code cho dữ liệu thiếu
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response = new ApiResponse<UserDTO>
                {
                    code = 1001, // General error code
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }

            response = new ApiResponse<UserDTO>
            {
                code = 1000,
                message = "User Updated Successfully",
                result = result
            };

            return Ok(response);
        }
        [HttpPut("block/{id}")]
        public async Task<IActionResult> blockUser(int id, [FromQuery] int status)
        {
            try
            {
                // Call the service to update user status
                await _userService.blockUser(id, status);

                // Return success response
                return Ok(new ApiResponse<UserDTO>
                {
                    code = 200,
                    message = "User status updated successfully",
                    result = null
                });
            }
            catch (Exception ex)
            {
                // Handle error and return failure response
                return BadRequest(new ApiResponse<UserDTO>
                {
                    code = 400,
                    message = ex.Message,
                    result = null
                });
            }
        }
    }
}
