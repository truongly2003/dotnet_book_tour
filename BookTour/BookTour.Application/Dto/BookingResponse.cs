using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class BookingResponse
    {
        public int UserId { get; set; }
        public int BookingId { get; set; }
        public DateTime TimeToOrder { get; set; }
        public string DetailRouteName { get; set; }
        public DateOnly TimeToDeparture { get; set; }
        public DateOnly TimeToFinish { get; set; }
        public string PaymentStatusName { get; set; }
        public int TotalPassengers { get; set; }
        public string DepartureName { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
