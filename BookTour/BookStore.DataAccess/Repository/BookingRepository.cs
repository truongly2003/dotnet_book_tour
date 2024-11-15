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
        private readonly BookTourDbContext _context;

        public BookingRepository(BookTourDbContext context)
        {
            _context = context;
        }
        public async Task<Booking> findById(int id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        }
    }
}
