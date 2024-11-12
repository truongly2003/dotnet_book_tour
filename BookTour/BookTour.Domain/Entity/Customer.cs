using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string CustomerEmail { get; set; } = null!;

    public string CustomerAddress { get; set; } = null!;

    public int? UserId { get; set; }

    public string CustomerPhone { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User? User { get; set; }
}
