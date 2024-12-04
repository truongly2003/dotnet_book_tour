using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
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
    }
}