using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTour.Application.Dto.Payment;
using BookTour.Domain.Entity;

namespace BookTour.Application.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IVNPayService _vNPayService;
        public PaymentService(IPaymentRepository paymentRepository, IVNPayService vNPayService)
        {
            _paymentRepository = paymentRepository;
            _vNPayService = vNPayService;
        }

        public async Task<List<PaymentResponse>> getAllPayments()
        {
            try
            {
                // Retrieve all payments from the repository
                var payments = await _paymentRepository.getAllPayments();

                // Check if the payments list is null or empty
                if (payments == null || !payments.Any())
                {
                    // Return an empty list or handle appropriately
                    return new List<PaymentResponse>();
                }

                // Map the list of Payment entities to a list of PaymentResponse DTOs
                var paymentResponses = payments.Select(payment => new PaymentResponse
                {
                    PaymentId = payment.PaymentId,
                    PaymentName = payment.PaymentName
                }).ToList();

                return paymentResponses;
            }
            catch (Exception ex)
            {
                // Log error (you can replace this with a logger)
                Console.WriteLine($"Error retrieving payments: {ex.Message}");

                // Optionally, rethrow the error or return an empty list or handle differently
                throw new InvalidOperationException("An error occurred while retrieving payments.", ex);
            }
        }
        
        public async Task<string> CreatePaymentUrlAsync(PaymentRequestDTO request)
        {
            var paymentUrl=_vNPayService.CreatePaymentUrl(request);

            return paymentUrl;
        }

        public async Task<bool> ProcessCallbackAsync(VNPayCallbackDTO callbackData)
        {
            var payment = await _paymentRepository.GetPaymentByIdAsync(int.Parse(callbackData.TransactionId));
            if (payment == null) return false;
            bool isValid = _vNPayService.ValidateSignature(callbackData.Signature, callbackData.RawData);
            if (!isValid) return false;
            //
            // payment.PaymentStatus = callbackData.ResponseCode == "00" ? PaymentStatus.Success : PaymentStatus.Failed;
            // //payment.BankCode = callbackData.BankCode;
            // //payment.Description = callbackData.Description;
            // payment.PaymentDate = callbackData.PaymentDate;
            // await _paymentRepository.UpdatePaymentAsync(payment);
            return true;
        }   
        
    }
}