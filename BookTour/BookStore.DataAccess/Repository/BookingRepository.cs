using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookTourDbContext _dbContext;
        public BookingRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Booking>> GetAllBookingByUserIdAsync(int UserId)
        {
            var query = await _dbContext.Bookings
                 .Include(book => book.Customer)
                 .Where(cus => cus.Customer.UserId == UserId)
                 .Include(book => book.PaymentStatus)
                 .Include(book => book.DetailRoute)
                 .ThenInclude(detail => detail.Route)
                 .ThenInclude(route => route.Departure)
                 .Include(book => book.Ticket)
                 .ThenInclude(tickit => tickit.Passenger)
                 .ToListAsync();
            return query;
        }

        public async Task<Booking> findById(int id)
        {
            return await _dbContext.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        }

        public async Task<User> GetDetailBookingResponseByUserIdAsync(int UserId, int BookingId)
        {
            var query = await _dbContext.Users
                .Include(user => user.Customers)
                .ThenInclude(cus => cus.Bookings)
                .Where(user => user.UserId == UserId && user.Customers.Any(cus => cus.Bookings.Any(book => book.BookingId == BookingId)))
                .FirstOrDefaultAsync();
            return query;
        }
    }
}
