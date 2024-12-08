using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BookTour.Controllers
{
    [Route("api/admin/decentralization")]
    [ApiController]
    public class DecentralizationController : ControllerBase
    {

        private readonly IDecentralizationService _decentralizationService;

        // Constructor để inject service
        public DecentralizationController(IDecentralizationService decentralizationService)
        {
            _decentralizationService = decentralizationService;
        }

        [HttpGet("getRoleAndPermissionsByUserId")]
        public async Task<IActionResult> GetRoleAndPermissionsByUserId([FromQuery] int userId)
        {
            ApiResponse<DecentralizationDTO> response;

            try
            {
                Console.WriteLine("Received userId: " + userId);

                // Lấy thông tin vai trò của người dùng từ service
                var result = await _decentralizationService.GetRoleAndPermissionsByUserIdAsync(userId);

                if (result == null || !result.Permissions.Any())
                {
                    response = new ApiResponse<DecentralizationDTO>
                    {
                        code = 1001,
                        message = "User not found or no permissions available",
                        result = null
                    };
                    return NotFound(response);
                }

                response = new ApiResponse<DecentralizationDTO>
                {
                    code = 1000,
                    message = "Role and permissions fetched successfully",
                    result = result
                };

                Console.WriteLine($"code: {response.code}, message: {response.message}, result: {response.result}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                response = new ApiResponse<DecentralizationDTO>
                {
                    code = 1002,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }
        }

        [HttpGet("getPermissionsByRoleId")]
        public async Task<IActionResult> GetPermissionsByRoleId([FromQuery] int roleId)
        {
            ApiResponse<object> response;

            try
            {
                // Kiểm tra nếu roleId không hợp lệ
                if (roleId <= 0)
                {
                    response = new ApiResponse<object>
                    {
                        code = 1001,
                        message = "Invalid roleId",
                        result = null
                    };
                    return BadRequest(response);
                }

                // Lấy danh sách permissions từ service
                var result = await _decentralizationService.GetPermissionsByRoleIdAsync(roleId);

                // Nếu không tìm thấy permissions cho roleId
                if (result == null || !result.Any())
                {
                    response = new ApiResponse<object>
                    {
                        code = 1002,
                        message = "Permissions not found for the given roleId",
                        result = null
                    };
                    return NotFound(response);
                }

                // Trả về kết quả thành công
                response = new ApiResponse<object>
                {
                    code = 1000,
                    message = "Permissions fetched successfully",
                    result = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Trường hợp có lỗi xảy ra trong quá trình xử lý
                response = new ApiResponse<object>
                {
                    code = 1003,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }
        }

        [HttpPost("updatePermissions")]
        public async Task<IActionResult> UpdatePermissions([FromBody] RolePermissionRequest request)
        {
            try
            {
                // Gọi service để cập nhật permissions
                await _decentralizationService.UpdatePermissionsAsync(request.roleId, request.permissions);

                // Trả về phản hồi thành công
                var response = new ApiResponse<DecentralizationDTO>
                {
                    message = "Permissions updated successfully!"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                var errorResponse = new ApiResponse<DecentralizationDTO>
                {
                    code = 1002,
                    message = ex.Message,
                    result = null
                };

                return BadRequest(errorResponse);
            }
        }

        [HttpGet("getPermission")]
        public async Task<ActionResult<ApiResponse<List<PermissionDTO>>>> GetListPermission()
        {
            try
            {
                // Gọi service để lấy danh sách permissions (chờ kết quả trả về)
                var permissions = await _decentralizationService.GetListPermission();

                // Trả về phản hồi thành công với danh sách permissions
                var response = new ApiResponse<List<PermissionDTO>>
                {
                    result = permissions,
                    message = "Permissions fetched successfully!",
                    code = 1000
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                var errorResponse = new ApiResponse<List<PermissionDTO>>
                {
                    code = 1002,
                    message = ex.Message,
                    result = null
                };

                return BadRequest(errorResponse);
            }
        }

        [HttpPost("addPermissions")]
        public async Task<ActionResult<ApiResponse<object>>> AddPermissions([FromBody] RolePermissionRequest request)
        {
            try
            {
                Console.WriteLine($"Received request: {JsonConvert.SerializeObject(request)}");
                // Gọi service để thêm permissions cho role
                await _decentralizationService.AddPermissionsForRoleAsync(request.roleId, request.permissions);

                // Trả về phản hồi thành công
                var response = new ApiResponse<object>
                {
                    message = "Permissions added successfully!",
                    code = 1000,
                    result = null // Không cần trả về kết quả cụ thể, có thể là null
                };

                return Ok(response); // Trả về kết quả thành công
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                var errorResponse = new ApiResponse<object>
                {
                    code = 1002, // Mã lỗi
                    message = $"Error: {ex.Message}",
                    result = null // Không có kết quả trả về trong trường hợp lỗi
                };

                return BadRequest(errorResponse); // Trả về phản hồi lỗi
            }
        }

    }
}
