using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IFeedbackService
    {
        public Task<Page<FeedbackDTO>> getListFeedbackAsync(int page, int size, int detailRouteId);
        public Task<FeedbackDTO> comment(FeedbackRequest request);

        public Task<bool> CheckBooking(int userId, int detailRouteId);
    }
}
