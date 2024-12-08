using BookTour.Domain.Entity;

namespace BookTour.Domain.Interface;

public interface IPaymentRepository
{
    Task<List<Payment>> getAllPayments();
    Task<Payment> GetPaymentByIdAsync(int paymentId);
    Task CreatePaymentAsync(Payment payment);
    Task UpdatePaymentAsync(Payment payment);
}