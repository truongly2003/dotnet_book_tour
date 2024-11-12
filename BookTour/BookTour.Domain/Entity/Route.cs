using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Route
{
    [Column("route_id")]
    public int RouteId { get; set; }
    [Column("arrival_id")]
    public int ArrivalId { get; set; }
    [Column("departure_id")]
    public int DepartureId { get; set; }
    public virtual Arrival Arrival { get; set; }

    public virtual Departure Departure { get; set; }

    public virtual ICollection<Detailroute> Detailroutes { get; set; } = new List<Detailroute>();
}
