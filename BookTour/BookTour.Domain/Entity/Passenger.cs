using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookTour.Domain.Entity;

public partial class Passenger
{

    [Column("passenger_id")]
    public int PassengerId { get; set; }

    [Column("object_id")]
    public int? ObjectId { get; set; }

    [Column("passenger_name")]
    public string PassengerName { get; set; } = null!;

    [Column("gender")]
    public string Gender { get; set; } = null!;

    [Column("date_birth")]
    public DateOnly DateBirth { get; set; }

    public virtual Objects? Object { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } 
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    
}
