using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Arrival
{
    [Column("arrival_id")]
    public int ArrivalId { get; set; }
    [Column("arrival_name")]
    public string ArrivalName { get; set; }
    [Column("status_id")]
    public int StatusId { get; set; }

    public virtual ICollection<Route> Routes { get; set; } = new List<Route>();

    public virtual Statusroleoperation Status { get; set; }
}
