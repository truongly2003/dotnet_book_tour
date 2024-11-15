using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Leg
{
    [Column("leg_id")]
    public int LegId { get; set; }
    [Column("detail_route_id")]
    public int DetailRouteId { get; set; }
    [Column("title")]
    public string Title { get; set; }
    [Column("description")]
    public string Description { get; set; }
    [Column("sequence")]
    public int Sequence { get; set; }

    public virtual Detailroute DetailRoute { get; set; }
}
