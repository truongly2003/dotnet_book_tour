using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
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
    public class FeedbackService : IFeedbackService
    {
        private readonly IDetailRouteRepository _detailRouteRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IBookingRepository _bookRepository;
        public FeedbackService(IDetailRouteRepository detailRouteRepository, IFeedbackRepository feedbackRepository, IBookingRepository bookingRepository)
        {
            this._detailRouteRepository = detailRouteRepository;
            this._feedbackRepository = feedbackRepository;
            this._bookRepository = bookingRepository;
        }
        public async Task<FeedbackDTO> comment(FeedbackRequest request)
        {
            Booking booking = await _bookRepository.findById(request.bookingId);
            Detailroute detailroute = await _detailRouteRepository.findById(request.detailRouteId);

            Feedback feedback = new Feedback
            {
                Rating = request.rating,
                Text = request.text,
                Booking = booking,
                DetailRoute = detailroute,
                DateCreate = DateTime.Now
            };

            await _feedbackRepository.saveFeedback(feedback);

            FeedbackDTO feedbackDTO = new FeedbackDTO
            {
                feedbackId = feedback.FeedbackId
            };

            return feedbackDTO;
        }

        public async Task<Page<FeedbackDTO>> getListFeedbackAsync(int page, int size, int detailRouteId)
        {
            var data = await _feedbackRepository.getListFeedbackAsync(detailRouteId);

            var feedback = data.Select(data => new FeedbackDTO
            {
                feedbackId = data.FeedbackId,
                bookingId = data.BookingId,
                detailRouteId = data.DetailRouteId,
                customerName = data.Booking.Customer.CustomerName,
                detailRouteName = data.DetailRoute.DetailRouteName,
                date = data.DateCreate,
                rating = data.Rating,
                text = data.Text
            })
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

            var totalElement = data.Count();
            var totalPage = (int)Math.Ceiling((double)totalElement / size);
            var result = new Page<FeedbackDTO>
            {
                Data = feedback,
                TotalElement = totalElement,
                TotalPages = totalPage
            };

            return result;
        }
    }
}
