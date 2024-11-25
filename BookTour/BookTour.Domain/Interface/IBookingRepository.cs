using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllBookingByCustomerIdAsync(int CustomerId);
        Task<Customer> GetDetailBookingResponseByCustomerIdAsync(int CustomerId);
        Task<Booking> FindByIdAsync(int id);
        Task<List<Booking>> GetAllBookingsAsync();


    }
}
