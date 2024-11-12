using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Departure
{
    public int DepartureId { get; set; }

    public string? DepartureName { get; set; }

    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
}
