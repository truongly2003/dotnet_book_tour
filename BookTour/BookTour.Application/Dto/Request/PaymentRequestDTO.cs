using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto.Payment
{
    public class PaymentRequestDTO
    {
        public int BookingId { get; set; }
        public int Amount { get; set; }
    }
}
