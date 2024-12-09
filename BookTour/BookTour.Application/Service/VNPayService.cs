using BookTour.Application.Dto.Payment;
using BookTour.Application.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BookStore.Infrastructure.VNPay;

namespace BookTour.Application.Service
{
    public class VNPayService: IVNPayService
    {
        private readonly VnPaySettings _config;
        private readonly VnPayLibrary _vnpayLibrary;
        public VNPayService(IOptions<VnPaySettings> options)
        {
            _config = options.Value;
            _vnpayLibrary = new VnPayLibrary();
        }
        public string CreatePaymentUrl(PaymentRequestDTO request)
        {
            _vnpayLibrary.AddRequestData("vnp_Version", "2.1.0"); 
            _vnpayLibrary.AddRequestData("vnp_Command", "pay"); 
            _vnpayLibrary.AddRequestData("vnp_TmnCode", "OM9LMLGS");
            _vnpayLibrary.AddRequestData("vnp_Amount", ((int)(request.Amount * 100)).ToString());
            _vnpayLibrary.AddRequestData("vnp_BankCode", "NCB");
            _vnpayLibrary.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            _vnpayLibrary.AddRequestData("vnp_CurrCode", "VND");
            _vnpayLibrary.AddRequestData("vnp_IpAddr", "127.0.0.1");
            _vnpayLibrary.AddRequestData("vnp_Locale", "vn");
            _vnpayLibrary.AddRequestData("vnp_OrderInfo", Uri.EscapeDataString($"Thanh Toan Don Hang {request.BookingId}"));
            _vnpayLibrary.AddRequestData("vnp_ReturnUrl", "https://localhost:7146/api/Payment/callback");

            string txnRef = $"{request.BookingId}";
            _vnpayLibrary.AddRequestData("vnp_TxnRef", txnRef);

            // Nếu cần thêm OrderType và ExpireDate
            _vnpayLibrary.AddRequestData("vnp_OrderType", "billpayment");
            _vnpayLibrary.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            return _vnpayLibrary.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", "OYQDPQUV6NOCS5OW6RSN8J2BPJBDUXME");
        }

        public bool ValidateSignature(string signature, string rawData)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_config.HashSecret)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                string generatedSignature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return generatedSignature.Equals(signature, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
