using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly BookTourDbContext _context;

    public PaymentRepository(BookTourDbContext context)
    {
        _context = context;
    }

    public async Task<List<Payment>> getAllPayments()
    {
        return await _context.Payments.ToListAsync();
    }
    
    public async Task CreatePaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }
    public async Task<Payment> GetPaymentByIdAsync(int paymentId)
    {
        return await _context.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
    }
    public async Task UpdatePaymentAsync(Payment payment)
    {
        _context.Set<Payment>().Update(payment);
        await _context.SaveChangesAsync();
    }
}