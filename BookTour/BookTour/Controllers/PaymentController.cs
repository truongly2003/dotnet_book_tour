using BookTour.Application.Dto;
using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Use PascalCase for method names
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var payments = await _paymentService.getAllPayments();

                if (payments == null || payments.Count == 0)
                {
                    return Ok(new ApiResponse<List<PaymentResponse>>
                    {
                        code = 200,
                        message = "Successfully",
                        result = payments = new List<PaymentResponse>()
                    });  
                }

                return Ok(payments);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving payments: {ex.Message}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}