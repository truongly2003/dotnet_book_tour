using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookTour.Application.Dto.Request
{
    public class BookingRequest
    {
        public int detailRouteId { get; set; }
        public decimal total_price { get; set; }

        public string customerName { get; set; }

        public string customerEmail { get; set; }

        public string customerAddress { get; set; }
        
        public string customerPhone { get; set; }
        
        public int paymentMethod { get; set; }

        public int? userId { get; set; }
        
        public List<PassengerRequestList> passengerRequestList { get; set; }
    }

}