using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> getListFeedbackAsync(int detailRouteId);
        Task<Feedback> saveFeedback(Feedback feedback);

        public Task<bool> ExistsByUserIdAndDetailRouteId(int userId, int detailRouteId);
    }
}
