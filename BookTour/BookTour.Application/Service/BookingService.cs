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

        public async Task<Page<BookingResponse>> GetAllBookingByUserIdAsync(int UserId, int page, int size)
        {
            var data = await _bookingRepository.GetAllBookingByUserIdAsync(UserId);
            var bookingResponse = data.Select(booking => new BookingResponse
            {
                UserId=booking.Customer.UserId,
                BookingId = booking.BookingId,
                TotalPrice=booking.TotalPrice ?? 0,
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

        public async Task<BookingDetailResponse> GetDetailBookingResponseByUserIdAsync(int UserId,int BookingId)
        {
            var data = await _bookingRepository.GetDetailBookingResponseByUserIdAsync(UserId,BookingId);
            var ticket = await _ticketRepository.GetAllTicketByUserIdAsync(UserId,BookingId);
            var groupedTickets = ticket.GroupBy(t => t.Passenger.Object.ObjectId)
                               .ToList();

            var bookingResponse = new BookingDetailResponse
            {
                CustomerEmail = data.Customers?.FirstOrDefault()?.CustomerName ?? "",
                CustomerName = data.Customers?.FirstOrDefault()?.CustomerName ?? "",
                CustomerPhone = data.Customers?.FirstOrDefault()?.CustomerPhone ?? "",

                ListTicket = groupedTickets.Select(group => 
                {
                    var ticketTypeId = group.Key;
                    var ticketPrice = group.FirstOrDefault()?.Booking.DetailRoute.Price ?? 0;
                    var adjustedPrice = ticketPrice;
                    if (ticketTypeId == 2)
                        adjustedPrice *= 0.8;
                    else if (ticketTypeId == 3)
                        adjustedPrice *= 0.5;
                    return new TicketResponse
                    {
                        ObjectName = group.FirstOrDefault()?.Passenger?.Object?.ObjectName ?? "",
                        Price = adjustedPrice,
                        Quantity = group.Count(),
                        TotalPrice = adjustedPrice * group.Count() 
                    };
                }).ToList()
            }; 
            return bookingResponse;
        }
    }
}
