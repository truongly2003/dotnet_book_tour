using BookStore.DataAccess.Repository;
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


        public async Task<Page<FeedbackDTO>> getListFeedbackAdminAsync(int page, int size)
        {
            // Lấy dữ liệu từ repository
            var data = await _feedbackRepository.getListFeedbackAdminAsync();

            // Kiểm tra nếu dữ liệu là null hoặc không có bản ghi
            if (data == null || !data.Any())
            {
                return new Page<FeedbackDTO>
                {
                    Data = new List<FeedbackDTO>(),
                    TotalElement = 0,
                    TotalPages = 0
                };
            }

            // In ra số lượng bản ghi (dành cho việc debug)
            Console.WriteLine($"Data fetched from the repository: {data.Count()} records");

            // Duyệt qua dữ liệu và kiểm tra null cho các thuộc tính
            var feedback = data.Select(feedback => new FeedbackDTO
            {
                feedbackId = feedback.FeedbackId,
                bookingId = feedback.BookingId,
                detailRouteId = feedback.DetailRouteId,
                // Kiểm tra null cho Booking và Customer trước khi truy cập
                customerName = feedback.Booking.Customer?.CustomerName ?? "Unknown",  // Giá trị mặc định nếu null
                detailRouteName = feedback.DetailRoute?.DetailRouteName ?? "Unknown",  // Giá trị mặc định nếu null
                date = feedback.DateCreate,
                rating = feedback.Rating,
                text = feedback.Text
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


        public async Task<bool> CheckBooking(int userId, int detailRouteId)
        {
            try
            {
                return await _feedbackRepository.ExistsByUserIdAndDetailRouteId(userId, detailRouteId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while checking booking", ex);
            }
        }

        public async Task<FeedbackDTO> comment(FeedbackRequest request)
        {
            // Lấy đối tượng Booking và DetailRoute từ cơ sở dữ liệu
            Booking booking = await _bookRepository.FindByIdAsync(request.bookingId);
            Detailroute detailroute = await _detailRouteRepository.findById(request.detailRouteId);

            // Kiểm tra nếu không tìm thấy Booking hoặc DetailRoute
            if (booking == null)
            {
                throw new ArgumentException($"Booking with ID {request.bookingId} not found.");
            }

            if (detailroute == null)
            {
                throw new ArgumentException($"DetailRoute with ID {request.detailRouteId} not found.");
            }

            // Tạo đối tượng Feedback mới từ request
            Feedback feedback = new Feedback
            {
                Rating = request.rating,
                Text = request.text,
                Booking = booking,
                DetailRoute = detailroute,
                DateCreate = DateTime.Now
            };

            // Lưu Feedback vào cơ sở dữ liệu
            await _feedbackRepository.saveFeedback(feedback);

            // Ánh xạ từ Feedback entity sang FeedbackDTO
            FeedbackDTO feedbackDTO = new FeedbackDTO
            {
                feedbackId = feedback.FeedbackId,
                bookingId = booking.BookingId,

                detailRouteId = detailroute.DetailRouteId,
                text = feedback.Text,
                rating = feedback.Rating,
                date = feedback.DateCreate
            };

            // Trả về FeedbackDTO đã được ánh xạ
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

        public async Task<Page<FeedbackDTO>> getListUserByDetailRouteName(int page, int size, string detailRouteName)
        {
            var data = await _feedbackRepository.FindFeedbackByDetailRouteNameAsync(detailRouteName);

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
