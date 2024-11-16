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
    public class TicketRepository : ITicketRepository
    {
        private readonly BookTourDbContext _dbContext;
        public TicketRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Ticket>> GetAllTicketByUserIdAsync(int UserId, int BookingId)
        {
            var query= await _dbContext.Tickets
                .Include(t=>t.Passenger)
                .ThenInclude(pas=>pas.Object)
                .Include(t=>t.Booking)
                .ThenInclude(book=>book.DetailRoute)
                .Where(book=>book.Booking.Customer.UserId == UserId && book.BookingId==BookingId)
                .ToListAsync();
            return query;
        }   
    }
}
