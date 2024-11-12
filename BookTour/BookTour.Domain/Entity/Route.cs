using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Route
{
    public int RouteId { get; set; }

    public int? ArrivalId { get; set; }

    public int? DepartureId { get; set; }

    public virtual Arrival? Arrival { get; set; }

    public virtual Departure? Departure { get; set; }

    public virtual ICollection<Detailroute> Detailroutes { get; set; } = new List<Detailroute>();
}
