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
        Task<List<Booking>> GetAllBookingByUserIdAsync(int UserId);
        Task<User> GetDetailBookingResponseByUserIdAsync(int UserId, int BookingId);
        Task<Booking> findById(int id);
    }
}
