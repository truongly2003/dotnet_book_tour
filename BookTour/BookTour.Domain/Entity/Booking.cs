using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? CustomerId { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? TimeToOrder { get; set; }

    public int? PaymentId { get; set; }

    public int? PaymentStatusId { get; set; }

    public int StatusBooking { get; set; }

    public int? DetailRouteId { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Detailroute? DetailRoute { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual Payment? Payment { get; set; }

    public virtual Paymentstatus? PaymentStatus { get; set; }

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
