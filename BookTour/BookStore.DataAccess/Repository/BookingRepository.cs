﻿using BookTour.Domain.Entity;
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
        public async Task<List<Booking>> GetAllBookingByCustomerIdAsync(int CustomerId)
        {
            var query = await _dbContext.Bookings
                 .Include(book => book.Customer)
                 .Include(book => book.Payment)
                 .Include(book => book.DetailRoute)
                 //.Include(book=>book.T)
                 .ToListAsync();
            return query;
        }
      
        public async Task<Booking> findById(int id)
        {
            return await _dbContext.Bookings.FirstOrDefaultAsync(b => b.BookingId == id);
        }
    }
}
