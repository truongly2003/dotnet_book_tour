using BookTour.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IBookingService
    {
        Task<Page<BookingResponse>> GetAllBookingByCustomerIdAsync(int CustomerId,int page,int size);
    }
}
