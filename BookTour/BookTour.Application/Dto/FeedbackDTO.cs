using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class FeedbackDTO
    {
        public int feedbackId {  get; set; }
        public int bookingId { get; set; }
        public string customerName { get; set; }
    
        public int detailRouteId { get; set; }
        public string detailRouteName { get; set; }

        public string text { get; set; }

        public float rating {  get; set; }

        public DateTime date { get; set; }


    
    }
}
