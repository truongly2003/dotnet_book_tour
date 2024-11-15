using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Application.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> getAllRole()
        {
            ApiResponse<List<RoleDTO>> response;  // Đổi kiểu thành List<RoleDTO>
            List<RoleDTO> result;

            try
            {
                result = await _roleService.getAllRole();  // Giả sử trả về List<RoleDTO>
            }
            catch (Exception ex)
            {
                response = new ApiResponse<List<RoleDTO>>
                {
                    code = 1001,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }

            response = new ApiResponse<List<RoleDTO>>
            {
                code = 1000,
                message = "Role list fetched successfully",
                result = result
            };

            return Ok(response);
        }

    }
}
