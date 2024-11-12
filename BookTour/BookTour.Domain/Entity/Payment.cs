using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Payment
{
    public int PaymentId { get; set; }

    public string? PaymentName { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
