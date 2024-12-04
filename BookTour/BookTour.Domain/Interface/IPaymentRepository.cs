using BookTour.Domain.Entity;

namespace BookTour.Domain.Interface;

public interface IPaymentRepository
{
    Task<List<Payment>> getAllPayments();
}