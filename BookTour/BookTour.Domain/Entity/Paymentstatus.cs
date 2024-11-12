using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Paymentstatus
{
    public int PaymentStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
