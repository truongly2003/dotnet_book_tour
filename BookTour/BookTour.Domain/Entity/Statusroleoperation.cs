using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookTour.Domain.Entity;

public partial class Statusroleoperation
{
    [Key]
    public int StatusId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Arrival> Arrivals { get; set; } = new List<Arrival>();
}
