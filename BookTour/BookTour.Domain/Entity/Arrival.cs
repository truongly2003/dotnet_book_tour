using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Arrival
{
    public int ArrivalId { get; set; }

    public string? ArrivalName { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();

    public virtual Statusroleoperation? Status { get; set; }
}
