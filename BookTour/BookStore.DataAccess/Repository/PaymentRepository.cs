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
}