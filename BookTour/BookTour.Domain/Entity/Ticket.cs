using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Entity
{
    public partial class Ticket
    {
        [Column("booking_id")]
        public int BookingId { get; set; }
        [Column("passenger_id")]
        public int PassengerId { get; set; }

        public virtual Booking Booking { get; set; }

        public virtual Passenger Passenger { get; set; }
        //public string ObjectName { get; set; }
        //public int Quantity { get; set; }
        //public double TotalPrice { get; set; }
        //public double Price { get; set; }
    }
}
