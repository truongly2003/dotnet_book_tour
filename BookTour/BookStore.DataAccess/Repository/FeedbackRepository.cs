using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly BookTourDbContext _dbContext;
        public FeedbackRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Feedback>> getListFeedbackAsync(int detailRouteId)
        {
            var query = await _dbContext.Feedback
              .Include(b => b.DetailRoute)
              .Include(b => b.Booking)
              .ThenInclude(c => c.Customer)
              .Where(f =>  f.DetailRouteId == detailRouteId)
              .ToListAsync();

            return query;
        }

        public async Task<List<Feedback>> getListFeedbackAdminAsync()
        {
            var query = await _dbContext.Feedback
                .Include(b => b.DetailRoute)
                .Include(b => b.Booking)
                .ToListAsync();

            // Kiểm tra null cho từng phần tử trong query
            foreach (var feedback in query)
            {
                // Kiểm tra nếu `Booking` hoặc `DetailRoute` là null
                if (feedback.Booking == null)
                {
                    Console.WriteLine($"FeedbackId {feedback.FeedbackId} has null Booking");
                }
                if (feedback.DetailRoute == null)
                {
                    Console.WriteLine($"FeedbackId {feedback.FeedbackId} has null DetailRoute");
                }
            }

            return query;
        }

        public async Task<Feedback> saveFeedback(Feedback feedback)
        {
            await _dbContext.Feedback.AddAsync(feedback);
            await _dbContext.SaveChangesAsync();
            return feedback;

        }

      

        public async Task<bool> ExistsByUserIdAndDetailRouteId(int userId, int detailRouteId)
        {
            var exists = await _dbContext.Bookings
                .Where(b => b.Customer.UserId == userId && b.DetailRouteId == detailRouteId)
                .AnyAsync();

            return exists;
        }


        public async Task<List<Feedback>> FindFeedbackByDetailRouteNameAsync(string usernameSearch)
        {
            if (string.IsNullOrWhiteSpace(usernameSearch))
                throw new ArgumentException("DetailRouteName cannot be null or empty.", nameof(usernameSearch));

            // Tìm kiếm các phản hồi theo tên tuyến chi tiết (không phân biệt hoa thường)
            var feedbacks = await _dbContext.Feedback
                .Include(f => f.Booking)            // Bao gồm thông tin Booking nếu cần
                .Include(f => f.Booking.Customer)   // Bao gồm thông tin Customer nếu cần
                .Include(f => f.DetailRoute)        // Bao gồm thông tin DetailRoute nếu cần
                .Where(f => f.DetailRoute.DetailRouteName.ToLower().Contains(usernameSearch.ToLower()))  // Tìm kiếm theo tên tuyến chi tiết chứa từ khóa
                .ToListAsync();

            return feedbacks;
        }
    }
}

