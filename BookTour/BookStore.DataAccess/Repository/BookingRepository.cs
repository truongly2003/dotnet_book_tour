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

        public async Task<Booking> UpdateBookingStatusAsync(int bookingId, int statusId)
        {
            var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found");
            }

            booking.PaymentStatusId = statusId;

            await _dbContext.SaveChangesAsync();

            return booking;
        }

        public async Task<bool> ExistsByDetailRouteIdAsync(int detailRouteId)
        {
            return await _dbContext.Bookings.AnyAsync(b => b.DetailRouteId == detailRouteId);
        }

        public async Task AddAsync(Booking booking)
        {
            await _dbContext.Bookings.AddAsync(booking);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Booking> FindByIdAsync(int id)
        {
            return await _dbContext.Bookings
                .Include(book => book.Customer)
                .Include(book => book.PaymentStatus)
                .Include(book => book.DetailRoute)
                .ThenInclude(detail => detail.Route)
                .ThenInclude(route => route.Departure)
                .Include(book => book.Ticket)
                .ThenInclude(ticket => ticket.Passenger)
                 .AsNoTracking() 
                .FirstOrDefaultAsync(book => book.BookingId == id);
        }
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _dbContext.Bookings
                .Include(book => book.PaymentStatus)
                .Include(book => book.DetailRoute)
                .ThenInclude(detail => detail.Route)
                .ThenInclude(route => route.Departure)
                .Include(book => book.Ticket)
                .ThenInclude(ticket => ticket.Passenger)
                .ToListAsync();
        }

        public async Task<Customer> GetDetailBookingResponseByCustomerIdAsync(int customerId)
        {
            var query = await _dbContext.Customers
                .Include(cus => cus.Bookings)
                .ThenInclude(book => book.DetailRoute)
                .Where(cus => cus.CustomerId == customerId)
                .FirstOrDefaultAsync();
            return query;
        }

        public async Task<List<Booking>> GetAllBookingByCustomerIdAsync(int customerId)
        {
            var query = await _dbContext.Bookings
                .Include(book => book.Customer)
                .Include(book => book.PaymentStatus)
                .Include(book => book.DetailRoute)
                .ThenInclude(detail => detail.Route)
                .ThenInclude(route => route.Departure)
                .Include(book => book.Ticket)
                .ThenInclude(ticket => ticket.Passenger)
                .Where(book => book.Customer.CustomerId == customerId)
                .ToListAsync();
            return query;
        }
    }
}
