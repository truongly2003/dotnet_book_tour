using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Objects
{
    [Key]
    [Column("object_id")]
    public int ObjectId { get; set; }

    [Column("object_name")]
    public string? ObjectName { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
}
