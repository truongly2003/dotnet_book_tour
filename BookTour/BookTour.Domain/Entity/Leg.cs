using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Leg
{
    public int LegId { get; set; }

    public int? DetailRouteId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int Sequence { get; set; }

    public virtual Detailroute? DetailRoute { get; set; }
}
