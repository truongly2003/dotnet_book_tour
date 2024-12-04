using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;

namespace BookTour.Application.Interface;

public interface IPaymentService
{
    Task<List<PaymentResponse>> getAllPayments();
}