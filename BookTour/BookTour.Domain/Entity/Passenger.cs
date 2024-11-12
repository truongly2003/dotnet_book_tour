using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public int? ObjectId { get; set; }

    public string PassengerName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly DateBirth { get; set; }

    public virtual Object? Object { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
