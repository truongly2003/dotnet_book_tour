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
        public  async Task<Feedback> saveFeedback(Feedback feedback)
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
    }
}

