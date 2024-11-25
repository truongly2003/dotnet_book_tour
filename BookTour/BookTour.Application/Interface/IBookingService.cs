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
        Task<BookingDetailResponse> GetDetailBookingResponseByCustomerIdAsync(int CustomerId);


        // Lấy tất cả các booking
        Task<List<BookingResponse>> GetAllBookingsAsync();

        // Lấy booking theo id
        Task<BookingDetailResponse> GetBookingDetailByIdAsync(int bookingId);
    }
}
