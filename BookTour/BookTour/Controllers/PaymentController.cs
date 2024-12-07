using BookTour.Application.Dto;
using BookTour.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BookTour.Application.Dto.Payment;
using BookTour.Domain.Interface;

namespace BookTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IBookingRepository _bookingRepository;

        public PaymentController(IPaymentService paymentService, IBookingRepository bookingRepository)
        {
            _paymentService = paymentService;
            _bookingRepository = bookingRepository;
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
        
        [HttpPost("create-payment-url")]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] PaymentRequestDTO request)
        {
           
            var paymentUrl = await _paymentService.CreatePaymentUrlAsync(request);
            if (string.IsNullOrEmpty(paymentUrl))
            {
                return BadRequest("Unable to generate payment URL.");
            }

            return Ok(new { PaymentUrl = paymentUrl });
        }
        [HttpGet("callback")]
        public async Task<IActionResult> Callback()
        {
            var bookingId = HttpContext.Request.Query["vnp_TxnRef"];
            var bankTranNo = HttpContext.Request.Query["vnp_BankTranNo"].ToString();
            var orderInfo = HttpContext.Request.Query["vnp_OrderInfo"].ToString();
            var amount = HttpContext.Request.Query["vnp_Amount"].ToString();
            var transactionStatus = HttpContext.Request.Query["vnp_TransactionStatus"].ToString();
            var bankCode = HttpContext.Request.Query["vnp_BankCode"].ToString();
            var payDate = HttpContext.Request.Query["vnp_PayDate"].ToString();

            // Kiểm tra dữ liệu đầu vào hợp lệ
            if (string.IsNullOrEmpty(bankTranNo) || string.IsNullOrEmpty(orderInfo))
            {
                return BadRequest("Invalid callback data.");
            }

            // Kiểm tra và cập nhật trạng thái đơn hàng
            if (int.TryParse(bookingId, out var bookingIdInt))
            {
                var booking = await _bookingRepository.UpdateBookingStatusAsync(bookingIdInt, 2);
        
                if (booking == null)
                {
                    return BadRequest("Failed to update booking status.");
                }
            }
            else
            {
                return BadRequest("Invalid Bank Transaction Number.");
            }

            // Kiểm tra định dạng ngày giao dịch
            DateTime transactionDate;
            if (!DateTime.TryParseExact(payDate, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out transactionDate))
            {
                return BadRequest("Invalid payment date format.");
            }

            // Tạo URL để chuyển hướng
            var redirectUrl = $"http://localhost:3000/payment?bankTranNo={bankTranNo}&orderInfo={orderInfo}&amount={amount}&transactionStatus={transactionStatus}&bankCode={bankCode}&transactionDate={transactionDate:yyyy-MM-dd HH:mm:ss}";
    
            return Redirect(redirectUrl);
        }




    }
}