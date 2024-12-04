using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class BookingDetailResponse
    {
     
        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public List<TicketResponse> ListTicket { get;set;}


    }
}
