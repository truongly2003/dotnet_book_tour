using BookTour.Application.Dto;
using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookTour.Controllers
{
    [Route("api/admin/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) { 
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> getListCustomer(int page, int size)
        {
            ApiResponse<Page<CustomerDTO>> response;
            Page<CustomerDTO> result;

            try {
                result = await _customerService.GetAllUserAsync(page, size);
            }
            catch (Exception ex)
            {
                response = new ApiResponse<Page<CustomerDTO>>
                {
                    code = 1001,
                    message = ex.Message,
                    result = null
                };
                return BadRequest(response);
            }

            response = new ApiResponse<Page<CustomerDTO>>
            {
                code = 1000,
                message = "Customer list fetched successfully",
                result = result
            };

            return Ok(response);
        }
    }
}
