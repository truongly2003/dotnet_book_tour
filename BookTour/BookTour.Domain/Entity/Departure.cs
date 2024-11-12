using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Departure
{
    [Column("departure_id")]
    public int DepartureId { get; set; }
    [Column("departure_name")]
    public string DepartureName { get; set; }

    public virtual ICollection<Route> Routes { get; set; } 
}
