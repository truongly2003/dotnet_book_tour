using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto.Request
{
    public class FeedbackRequest
    {
        public float rating {  get; set; }
        public string text { get; set; }
        public int bookingId { get; set; }
        public int detailRouteId { get; set; }
    }
}
