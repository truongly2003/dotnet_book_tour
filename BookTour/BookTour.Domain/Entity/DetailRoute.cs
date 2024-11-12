using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Detailroute
{
    public int DetailRouteId { get; set; }

    public int? RouteId { get; set; }

    public double? Price { get; set; }

    public string? DetailRouteName { get; set; }

    public string? Description { get; set; }

    public DateOnly? TimeToDeparture { get; set; }

    public DateOnly? TimeToFinish { get; set; }

    public int? Stock { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<Leg> Legs { get; set; } = new List<Leg>();

    public virtual Route? Route { get; set; }
}
