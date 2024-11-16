using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITicketRepository _ticketRepository;
        public BookingService(IBookingRepository bookingRepository,ITicketRepository ticketRepository)
        {
            _bookingRepository = bookingRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<Page<BookingResponse>> GetAllBookingByCustomerIdAsync(int CustomerId, int page, int size)
        {
            var data = await _bookingRepository.GetAllBookingByCustomerIdAsync(CustomerId);
            var bookingResponse = data.Select(booking => new BookingResponse
            {
                BookingId = booking.BookingId,
                DetailRouteName = booking.DetailRoute?.DetailRouteName,
                PaymentStatusName = booking.PaymentStatus.StatusName,
                TimeToDeparture = booking.DetailRoute.TimeToDeparture,
                TimeToFinish = booking.DetailRoute.TimeToFinish,
                TimeToOrder = booking.TimeToOrder,
                TotalPassengers = booking.Ticket.Select(ticket => ticket.Passenger).Count(),
                DepartureName=booking.DetailRoute.Route.Departure.DepartureName
            })
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
            var totalElement = data.Count();
            var totalPage = (int)Math.Ceiling((double)totalElement / size);
            var result = new Page<BookingResponse>
            {
                Data = bookingResponse,
                TotalElement = totalElement,
                TotalPages = totalPage
            };
            return result;
        }

        public async Task<BookingDetailResponse> GetDetailBookingResponseByCustomerIdAsync(int CustomerId)
        {
            var data = await _bookingRepository.GetDetailBookingResponseByCustomerIdAsync(CustomerId);
            var ticket = await _ticketRepository.GetAllTicketByCustomerIdAsync(CustomerId);
            var bookingResponse = new BookingDetailResponse
            {
                CustomerEmail = data.CustomerEmail,
                CustomerName = data.CustomerName,
                CustomerPhone = data.CustomerPhone,
                ListTicket = ticket.Select(t => new TicketResponse
                {
                    ObjectName = t.Passenger?.Object?.ObjectName ?? "",
                    Price = 1000,
                    Quantity = 1,
                    TotalPrice = 10
                }).ToList()
            }; 
            return bookingResponse;
        }
    }
}
