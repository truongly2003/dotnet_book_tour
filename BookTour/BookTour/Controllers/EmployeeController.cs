using BookTour.Application.Dto;
using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/admin/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> getListEmployee(int page, int size)
        {
            ApiResponse<Page<EmployeeDTO>> response;
            Page<EmployeeDTO> result;

            try
            {
                result = await _employeeService.GetAllUserAsync(page, size);
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Page<EmployeeDTO>>
                {
                    code = 1001,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }

            response = new ApiResponse<Page<EmployeeDTO>>
            {
                code = 1000,
                message = "Customer list fetched successfully",
                result = result
            };

            return Ok(response);
        }
    }
}
