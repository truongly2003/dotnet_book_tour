using System;
using System.Collections.Generic;

namespace BookTour.Domain.Entity;

public partial class Object
{
    public int ObjectId { get; set; }

    public string? ObjectName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
